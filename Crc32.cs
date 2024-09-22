using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CRC32_HashComparator
{
	public class Crc32 : HashAlgorithm
	{
		public const UInt32 DefaultPolynomial = 0xedb88320u;
		public const UInt32 DefaultSeed = 0xffffffffu;

		private readonly UInt32 _seed;
		private readonly UInt32[] _table;
		private UInt32 _hash;

		private static UInt32[] _defaultTable;


		public Crc32(UInt32 polynomial = DefaultPolynomial, UInt32 seed = DefaultSeed)
		{
			if (!BitConverter.IsLittleEndian)
				throw new PlatformNotSupportedException("Not supported on Big Endian processors");

			HashSize = 32;

			_table = InitializeTable(polynomial);
			_seed = _hash = seed;
		}


		public override int HashSize { get; }


		static UInt32[] InitializeTable(UInt32 polynomial)
		{
			if (polynomial == DefaultPolynomial && _defaultTable != null)
				return _defaultTable;

			UInt32[] createTable = new UInt32[256];
			for (int i = 0; i < 256; i++)
			{
				UInt32 entry = (UInt32)i;
				for (int j = 0; j < 8; j++)
					if ((entry & 1) == 1)
						entry = (entry >> 1) ^ polynomial;
					else
						entry >>= 1;
				createTable[i] = entry;
			}

			if (polynomial == DefaultPolynomial)
				_defaultTable = createTable;

			return createTable;
		}

		static byte[] UInt32ToBigEndianBytes(UInt32 uint32)
		{
			byte[] result = BitConverter.GetBytes(uint32);

			if (BitConverter.IsLittleEndian)
				Array.Reverse(result);

			return result;
		}

		// Calculate a bit-inverted CRC32 for a given buffer using a polynomial-derived table.
		static UInt32 CalculateHash(UInt32[] _table, UInt32 _seed, IList<byte> buffer, int start, int size)
		{
			UInt32 _hash = _seed;
			for (int i = start; i < start + size; i++)
				_hash = (_hash >> 8) ^ _table[buffer[i] ^ _hash & 0xff];

			return _hash;
		}


		public override void Initialize()
		{
			_hash = _seed;
		}

		protected override void HashCore(byte[] array, int ibStart, int cbSize)
		{
			_hash = CalculateHash(_table, _hash, array, ibStart, cbSize);
		}

		protected override byte[] HashFinal()
		{
			byte[] hashBuffer = UInt32ToBigEndianBytes(~_hash);
			HashValue = hashBuffer;
			return hashBuffer;
		}
	}
}
