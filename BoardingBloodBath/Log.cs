using ModLoader;

namespace BoardingBloodbath
{
	internal static class Log
	{
		// Token: 0x04000001 RID: 1
		public static readonly Logger logger = new Logger("[BoardingBattle]", ModLoader.ModLoader.LogPath + "\\BoardingBattle.txt");

		public static void log(string message)
        {
			logger.Log(message);
        }
	}
}
