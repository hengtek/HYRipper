using AssetRipper.Core.Logging;
using System;

namespace AssetRipper.Core.Utils
{
	[Flags]
	public enum GameFlags
	{
		ZZZ,
		SR,
		BH3
	}

	public static class GameChoice
	{
		public static void SetGame(int game)
		{
			game1 = (GameFlags)game;
			if (game1 == GameFlags.SR)
			{
				Logger.Info(LogCategory.System, $"已选择游戏 Honkai Impact: Star Rail | 崩坏:星穹铁道");
			}
			else if (game1 == GameFlags.ZZZ)
			{
				Logger.Info(LogCategory.System, $"已选择游戏 Zenless Zone Zero | 绝区零");
			}
			else if (game1 == GameFlags.BH3)
			{
				Logger.Info(LogCategory.System, $"已选择游戏 Honkai Impact 3 | 崩坏3");
			}
		}

		public static GameFlags GetGame()
		{
			return game1;
		}

		public static GameFlags game1;
	}
}
