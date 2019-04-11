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

		public struct Text {
			public static string TOKEN_FILE = $@"{TEXT_DIR}token.txt";
		}

		public static DiscordSocketClient Client;
		public static CancellationTokenSource LicenceToLive;

		public static List<IDeletable> ElementsToDelete = new List<IDeletable>();
	}
}
