using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CRC32_HashComparator
{
	public partial class Form1 : Form
	{
		public List<UnitOfFile> Folder1File;
		public List<UnitOfFile> Folder2File;

		public int NumberOfGood;
		public int NumberOfBad;
		public int NumberOfMissed;

		public Form1()
		{
			InitializeComponent();

			NumberOfGood = 0;
			NumberOfBad = 0;
			NumberOfMissed = 0;

			button2_Folder2.Enabled = false;
			button3_GetAnswer.Enabled = false;
			textBox3_AnswerArea.Enabled = false;
		}

		private void button1_Folder1_Click(object sender, EventArgs e)
		{
			DirectoryInfo directoryInfo;
			FileInfo[] fileInfo;

			using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
			{
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
					textBox1_Folder1.Text = folderBrowserDialog.SelectedPath;
			}

			try
			{
				directoryInfo = new DirectoryInfo(textBox1_Folder1.Text);
				fileInfo = directoryInfo.GetFiles();
			}
			catch
			{
				MessageBox.Show("Ошибка при попытке открыть файл! \n \nПроверьте указанный путь к файлу или проверьте целостность файла.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			fileInfo.OrderBy(x => x.Name);

			Folder1File = new List<UnitOfFile>(fileInfo.Length);

			Error error;
			for (int i = 0; i < fileInfo.Length; i++)
			{
				Folder1File.Add(new UnitOfFile(fileInfo[i]));
				error = Folder1File[i].CalcCRC32();

				switch (error)
				{
					case Error.NOERROR:
						button1_Folder1.Enabled = false;
						textBox1_Folder1.Enabled = false;
						button2_Folder2.Enabled = true;
						textBox2_Folder2.Enabled = true;
						break;

					case Error.ReadFile:
						MessageBox.Show("Ошибка при попытке открыть файл! \n \nПроверьте указанный путь к файлу или проверьте целостность файла.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
				}
			}
		}

		private void button2_Folder2_Click(object sender, EventArgs e)
		{
			DirectoryInfo directoryInfo;
			FileInfo[] fileInfo;

			using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
			{
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
					textBox2_Folder2.Text = folderBrowserDialog.SelectedPath;
			}

			try
			{
				directoryInfo = new DirectoryInfo(textBox2_Folder2.Text);
				fileInfo = directoryInfo.GetFiles();
			}
			catch
			{
				MessageBox.Show("Ошибка при попытке открыть файл! \n \nПроверьте указанный путь к файлу или проверьте целостность файла.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			fileInfo.OrderBy(x => x.Name);

			Folder2File = new List<UnitOfFile>(fileInfo.Length);

			Error error;
			for (int i = 0; i < fileInfo.Length; i++)
			{
				Folder2File.Add(new UnitOfFile(fileInfo[i]));
				error = Folder2File[i].CalcCRC32();

				switch (error)
				{
					case Error.NOERROR:
						button2_Folder2.Enabled = false;
						textBox2_Folder2.Enabled = false;
						button3_GetAnswer.Enabled = true;
						textBox3_AnswerArea.Enabled = true;
						break;
						
					case Error.ReadFile:
						MessageBox.Show("Ошибка при попытке открыть файл! \n \nПроверьте указанный путь к файлу или проверьте целостность файла.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
				}
			}
		}

		private void button3_GetAnswer_Click(object sender, EventArgs e)
		{
			StringBuilder Answer = new StringBuilder(Environment.NewLine);

			for (int i = 0; i < Folder1File.Count; i++)
			{
				StringBuilder sb = new StringBuilder(Folder1File[i].Info.Name);

				if (sb.Length < 38)
				{
					int s = 38 - sb.Length;
					while (s > 0)
					{
						sb.Append(' ');
						s--;
					}
				}

				sb.Append("| " + Folder1File[i].CRC32 + "        | ");

				bool miss = true;
				for (int j = 0; j < Folder2File.Count; j++)
				{
					if (Folder1File[i].Info.Name == Folder2File[j].Info.Name)
					{
						if (Folder1File[i].CRC32 == Folder2File[j].CRC32)
						{
							sb.Append(Folder2File[j].CRC32 + "           | true");
							NumberOfGood++;
							miss = false;
							break;
						}
						else
						{
							sb.Append(Folder2File[j].CRC32 + "           | false");
							NumberOfBad++;
							miss = false;
							break;
						}
					}
				}
				if (miss)
				{
					sb.Append("                   | файл отсутствует");
					NumberOfMissed++;
				}

				Answer.AppendLine(sb.ToString());
			}

			textBox3_AnswerArea.Text = Answer.ToString();

			textBox3_AnswerArea.Text += Environment.NewLine
									 + "=== ИТОГО ============================================================================" + Environment.NewLine
									 + Environment.NewLine
									 + "Всего исходных файлов: " + Folder1File.Count + Environment.NewLine
									 + Environment.NewLine
									 + "Хеш-сумма соответствует: " + NumberOfGood + Environment.NewLine
									 + Environment.NewLine
									 + "Хеш-сумма не соответствует: " + NumberOfBad + Environment.NewLine
									 + Environment.NewLine
									 + "Файлов отсутствует: " + NumberOfMissed + Environment.NewLine
									 + Environment.NewLine;

			textBox3_AnswerArea.Select(0, 0);
			button3_GetAnswer.Enabled = false;
			
			Answer = null;
			Folder1File = null;
			Folder2File = null;
			GC.Collect();
		}
	}
}
