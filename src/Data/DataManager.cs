using Discord.WebSocket;
using System.Threading;

namespace KatanaBot.Data
{
	public class DataManager
	{
		public static string RESOURCES_DIR = @"resources/";
		public static string BIN_DIR = $@"{RESOURCES_DIR}binaries/";
		public static string TEXT_DIR = $@"{RESOURCES_DIR}text/";

		public static string CARDS_DIR = "resources/cards/";
		public static string WEAPONS_DIR = $"{CARDS_DIR}weapons/";
		public static string GENDERS_DIR = $"{CARDS_DIR}genders/";
		public static string CHARACTERS_DIR = $"{CARDS_DIR}characters/";
		public static string BUFFS_DIR = $"{CARDS_DIR}buffs/";
		public static string SPELLS_DIR = $"{CARDS_DIR}spells/";

		public struct Text
		{
			public static string TOKEN_FILE = $@"{TEXT_DIR}token.txt";
		}

		public static DiscordSocketClient _client;
		public static CancellationTokenSource LicenceToLive;
	}
}
