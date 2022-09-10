using AssetRipper.Core.IO.Asset;
using AssetRipper.Core.Utils;

namespace AssetRipper.Core.Classes.AnimationClip.Clip
{
	public sealed class AclClip : IAssetReadable
	{
		public uint[] m_ClipData;

		public uint m_CurveCount;
		public uint m_ConstCurveCount;
		public void Read(AssetReader reader)
		{
			m_ClipData = reader.ReadUInt32Array();

			m_CurveCount = reader.ReadUInt32();

			if (GameChoice.GetGame() == GameFlags.SR)
			{
				m_ConstCurveCount = reader.ReadUInt32();
			}
		}
	}
}
