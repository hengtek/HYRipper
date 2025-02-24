﻿using AssetRipper.Core.HoYo;
using AssetRipper.Core.IO.Endian;
using AssetRipper.Core.IO.Extensions;
using AssetRipper.Core.Parser.Files.BundleFile.Parser;
using AssetRipper.Core.Utils;

namespace AssetRipper.Core.Parser.Files.BundleFile.Header
{
	public sealed class BundleFileStreamHeader
	{
		public BundleFileStreamHeader(EndianReader reader)
		{
			if (GameChoice.GetGame() == GameFlags.BH3)
			{
				var key = reader.ReadInt32();
				Flags = (BundleFlags)reader.ReadUInt32();
				Size = reader.ReadInt64();
				UncompressedBlocksInfoSize_BH3 = reader.ReadUInt32();
				CompressedBlocksInfoSize_BH3 = reader.ReadUInt32();
				DecryptHeader(key);
				var encUnityVersion = reader.ReadStringToNull();
				var encUnityRevision = reader.ReadStringToNull();

				CompressedBlocksInfoSize = (int)CompressedBlocksInfoSize_BH3;
				UncompressedBlocksInfoSize = (int)UncompressedBlocksInfoSize_BH3;
			}
			else
			{
				Size = reader.ReadInt64();
				CompressedBlocksInfoSize = reader.ReadInt32();
				UncompressedBlocksInfoSize = reader.ReadInt32();
				Flags = (BundleFlags)reader.ReadInt32();
			}
		}

		private void DecryptHeader(int key)
		{
			var rand = new XORShift128();
			rand.InitSeed(key);
			Flags ^= (BundleFlags)rand.NextDecryptInt();
			Size ^= rand.NextDecryptLong();
			UncompressedBlocksInfoSize_BH3 ^= rand.NextDecryptUInt();
			CompressedBlocksInfoSize_BH3 ^= rand.NextDecryptUInt();
		}

		/// <summary>
		/// Equal to file size, sometimes equal to uncompressed data size without the header
		/// </summary>
		public long Size { get; set; }
		/// <summary>
		/// UnityFS length of the possibly-compressed (LZMA, LZ4) bundle data header
		/// </summary>
		public int CompressedBlocksInfoSize { get; set; }
		public int UncompressedBlocksInfoSize { get; set; }
		public uint CompressedBlocksInfoSize_BH3 { get; set; }
		public uint UncompressedBlocksInfoSize_BH3 { get; set; }
		public BundleFlags Flags { get; set; }
	}
}
