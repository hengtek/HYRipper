using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.IO.Extensions;
using AssetRipper.Core.Utils;

namespace AssetRipper.Core.Classes.AnimationClip.Clip
{
	public sealed class AclClip : IAssetReadable
	{
		public byte[] m_ClipData;
		public uint[] m_ClipDataUint;

		public uint m_CurveCount;
		public uint m_ConstCurveCount;
		public void Read(AssetReader reader)
		{
			if (GameChoice.GetGame() == GameFlags.SR)
			{
				m_ClipDataUint = reader.ReadUInt32Array();
			}
			else
			{
				m_ClipData = reader.ReadUInt8Array();
				reader.AlignStream();
			}

			m_CurveCount = reader.ReadUInt32();

			if (GameChoice.GetGame() == GameFlags.SR)
			{
				m_ConstCurveCount = reader.ReadUInt32();
			}	
		}
		public bool IsSet => m_ClipDataUint != null && m_ClipDataUint.Length > 0 || m_ClipData != null && m_ClipData.Length > 0;
	}
}
