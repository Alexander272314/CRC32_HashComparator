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
		private List<UnitOfFile> _folder1File;
		private List<UnitOfFile> _folder2File;

		private int _numberOfGood;
		private int _numberOfBad;
		private int _numberOfMissed;
		private int _numberOfCalculationErrors;

		private StringBuilder _errorSB;

		public Form1()
		{
			InitializeComponent();

			_numberOfGood = 0;
			_numberOfBad = 0;
			_numberOfMissed = 0;
			_numberOfCalculationErrors = 0;

			label1.Visible = false;
			button1_Folder1.Enabled = true;
			button2_Folder2.Enabled = false;
			button3_GetAnswer.Enabled = false;
			textBox1_Folder1.Enabled = false;
			textBox2_Folder2.Enabled = false;
			textBox3_AnswerArea.Enabled = true;
			checkBox1_CompareAll.Enabled = true;
			checkBox1_CompareAll.Checked = true;
		}

		private List<Exception> AddFileToList(DirectoryInfo directory, List<UnitOfFile> list, string generalPath, bool compareAll)
		{
			FileInfo[] fileInfo = null;
			DirectoryInfo[] directoryInfo = null;
			List<Exception> tempExceptionList = new List<Exception>();

			try
			{
				fileInfo = directory.GetFiles();
			}
			catch (Exception ex)
			{
				tempExceptionList.Add(ex);
			}

			if (compareAll)
			{
				try
				{
					directoryInfo = directory.GetDirectories();
				}
				catch (Exception ex)
				{
					tempExceptionList.Add(ex);
				}
			}

			if (compareAll && directoryInfo != null)
				foreach (DirectoryInfo dir in directoryInfo)
				{
					List<Exception> exceptionList = AddFileToList(dir, list, generalPath, true);

					if (exceptionList != null)
						foreach (Exception ex in exceptionList)
							_errorSB.AppendLine(directory.FullName + Environment.NewLine + ex.Message + Environment.NewLine);
				}

			if (fileInfo != null)
			{
				List<UnitOfFile> tempList = new List<UnitOfFile>(fileInfo.Length);

				foreach (FileInfo file in fileInfo)
				{
					try
					{
						tempList.Add(new UnitOfFile(file, generalPath));
					}
					catch (Exception ex)
					{
						_errorSB.AppendLine(directory.FullName + Environment.NewLine + ex.Message + Environment.NewLine);
						continue;
					}

					tempList[tempList.Count - 1].CalcCRC32();
				}

				list.AddRange(tempList.OrderBy(x => x.RelativePath));
			}

			if (tempExceptionList.Count > 0)
				return tempExceptionList;
			else
				return null;
		}

		private void button1_Folder1_Click(object sender, EventArgs e)
		{
			textBox3_AnswerArea.Text = "";
			DirectoryInfo directoryInfo;
			_folder1File = new List<UnitOfFile>();
			_errorSB = new StringBuilder();

			using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
			{
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
					textBox1_Folder1.Text = folderBrowserDialog.SelectedPath;
			}

			try
			{
				directoryInfo = new DirectoryInfo(textBox1_Folder1.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				textBox1_Folder1.Text = "";
				_folder1File = null;
				_errorSB = null;
				GC.Collect();
				return;
			}

			List<Exception> exceptionList = AddFileToList(directoryInfo, _folder1File, directoryInfo.FullName, checkBox1_CompareAll.Checked);

			if (exceptionList != null)
				foreach (Exception ex in exceptionList)
					_errorSB.AppendLine(directoryInfo.FullName + Environment.NewLine + ex.Message + Environment.NewLine);

			if (_errorSB.Length > 0)
				textBox3_AnswerArea.Text = Environment.NewLine
										 + "=== ERRORS (folder 1) ================================================================" + Environment.NewLine
										 + Environment.NewLine
										 + _errorSB.ToString();

			button1_Folder1.Enabled = false;
			button2_Folder2.Enabled = true;
			checkBox1_CompareAll.Enabled = false;
			textBox3_AnswerArea.Select(0, 0);
		}

		private void button2_Folder2_Click(object sender, EventArgs e)
		{
			textBox3_AnswerArea.Text = "";
			DirectoryInfo directoryInfo;
			_folder2File = new List<UnitOfFile>();
			_errorSB = new StringBuilder();

			using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
			{
				if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
					textBox2_Folder2.Text = folderBrowserDialog.SelectedPath;
			}

			try
			{
				if (textBox1_Folder1.Text == textBox2_Folder2.Text)
					throw new Exception("Выбрана та же самая папка! Выберите другую папку.");

				directoryInfo = new DirectoryInfo(textBox2_Folder2.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				textBox2_Folder2.Text = "";
				_folder2File = null;
				_errorSB = null;
				GC.Collect();
				return;
			}

			List<Exception> exceptionList = AddFileToList(directoryInfo, _folder2File, directoryInfo.FullName, checkBox1_CompareAll.Checked);

			if (exceptionList != null)
				foreach (Exception ex in exceptionList)
					_errorSB.AppendLine(directoryInfo.FullName + Environment.NewLine + ex.Message + Environment.NewLine);

			if (_errorSB.Length > 0)
				textBox3_AnswerArea.Text = Environment.NewLine
										 + "=== ERRORS (folder 2) ================================================================" + Environment.NewLine
										 + Environment.NewLine
										 + _errorSB.ToString();

			button2_Folder2.Enabled = false;
			button3_GetAnswer.Enabled = true;
			textBox3_AnswerArea.Select(0, 0);
		}

		private void button3_GetAnswer_Click(object sender, EventArgs e)
		{
			label1.Visible = true;
			textBox3_AnswerArea.Text = "";
			StringBuilder Answer = new StringBuilder(Environment.NewLine);

			for (int i = 0; i < _folder1File.Count; i++)
			{
				StringBuilder sb = new StringBuilder();
				string name = _folder1File[i].RelativePath;

				if (name.Length < 38)
				{
					sb.Append(name);
					int s = 38 - sb.Length;
					sb.Append(' ', s);
				}
				else
				{
					if (name.Length < 97)
					{
						sb.Append(name);
						sb.Append(Environment.NewLine);
						sb.Append("                                      ");
					}
					else
					{
						int div = name.Length / 97;
						int mod = name.Length % 97;

						for (int x = 0; x < div; x++)
						{
							sb.Append(name.Substring(0, 97));
							sb.Append(Environment.NewLine);
							name = name.Remove(0, 97);
						}

						if (mod < 38)
						{
							sb.Append(name);
							int s = 38 - mod;
							sb.Append(' ', s);
						}
					}
				}

				sb.Append("| " + _folder1File[i].CRC32 + "        | ");

				if (_folder1File[i].CRC32 == "error   ")
					_numberOfCalculationErrors++;

				bool miss = true;
				for (int j = 0; j < _folder2File.Count; j++)
				{
					if (_folder2File[j].IsAlreadyCompared)
					{
						continue;
					}
					else if (_folder1File[i].RelativePath == _folder2File[j].RelativePath)
					{
						if (_folder2File[j].CRC32 == "error   ")
						{
							sb.Append(_folder2File[j].CRC32 + "           | false");
							_numberOfBad++;
							_numberOfCalculationErrors++;
							miss = false;
							break;
						}
						else if (_folder1File[i].CRC32 == _folder2File[j].CRC32)
						{
							sb.Append(_folder2File[j].CRC32 + "           | true");
							_numberOfGood++;
							miss = false;
							break;
						}
						else
						{
							sb.Append(_folder2File[j].CRC32 + "           | false");
							_numberOfBad++;
							miss = false;
							break;
						}
					}
				}
				if (miss)
				{
					sb.Append("                   | файл отсутствует");
					_numberOfMissed++;
				}

				Answer.AppendLine(sb.ToString());
			}

			textBox3_AnswerArea.Text = Answer.ToString();

			textBox3_AnswerArea.Text += Environment.NewLine
									 + "=== ИТОГО ============================================================================" + Environment.NewLine
									 + Environment.NewLine
									 + "Всего исходных файлов: " + _folder1File.Count + Environment.NewLine
									 + Environment.NewLine
									 + "Хеш-сумма соответствует: " + _numberOfGood + Environment.NewLine
									 + Environment.NewLine
									 + "Хеш-сумма не соответствует: " + _numberOfBad + Environment.NewLine
									 + Environment.NewLine
									 + "Файлов отсутствует: " + _numberOfMissed + Environment.NewLine
									 + Environment.NewLine;

			if (_numberOfCalculationErrors > 0)
				textBox3_AnswerArea.Text += "Ошибок вычисления контрольной суммы: " + _numberOfCalculationErrors + Environment.NewLine + Environment.NewLine;

			textBox3_AnswerArea.Select(0, 0);
			button3_GetAnswer.Enabled = false;

			_folder1File = null;
			_folder2File = null;
			GC.Collect();
		}
	}
}
