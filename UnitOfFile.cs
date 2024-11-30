using System.IO;

namespace CRC32_HashComparator
{
	public class UnitOfFile
	{
		public FileInfo Info { get; }
		public string RelativePath { get; }
		public string CRC32 { get; private set; }
		public bool IsAlreadyCompared { get; set; }

		public UnitOfFile(FileInfo fileInfo, string generalPath)
		{
			Info = fileInfo;
			RelativePath = fileInfo.FullName.Replace(generalPath + "\\", "");
			CRC32 = null;
			IsAlreadyCompared = false;
		}

		public void CalcCRC32()
		{
			byte[] byte_crc32;
			Crc32 crc32 = new Crc32();

			try
			{
				using (FileStream fs = File.Open(@Info.FullName, FileMode.Open))
				{
					byte_crc32 = crc32.ComputeHash(fs);
				}
			}
			catch
			{
				CRC32 = "error   ";
				return;
			}

			string hash = "";
			for (int i = 0; i < byte_crc32.Length; i++)
				hash += byte_crc32[i].ToString("X2").ToUpper();

			CRC32 = hash;
		}
	}
}
