using System.Collections.Generic;
using System.Threading;

using Discord;
using Discord.WebSocket;

namespace KatanaBot.Data
{
	public class DataManager
	{
		public static string RESOURCES_PATH = @"resources/";
		
		public static string BIN_DIR = $@"{RESOURCES_PATH}binaries/";
		public static string TEXT_DIR = $@"{RESOURCES_PATH}text/";

		public static string CARDS_DIR = "resources/cards/";
		public static string WEAPONS_DIR = $"{CARDS_DIR}weapons/";
		public static string ROLES_DIR = $"{CARDS_DIR}genders/";
		public static string CHARACTERS_DIR = $"{CARDS_DIR}characters/";
		public static string BUFFS_DIR = $"{CARDS_DIR}buffs/";
		public static string SPELLS_DIR = $"{CARDS_DIR}spells/";

		public struct Text {
			public static string TOKEN_FILE = $@"{TEXT_DIR}token.txt";
		}

		public static DiscordSocketClient Client;
		public static CancellationTokenSource LicenceToLive;

		public static List<IDeletable> ElementsToDelete = new List<IDeletable>();
	}
}
