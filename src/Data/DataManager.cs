using System.Collections.Generic;

using Discord;
using Discord.WebSocket;
using System.Threading;

namespace KatanaBot.Data
{
	public class DataManager
	{
		public static string RESOURCES_DIR = @"resources/";
		public static string BIN_DIR = $@"{RESOURCES_DIR}binaries/";
		public static string TEXT_DIR = $@"{RESOURCES_DIR}text/";

		public struct Text
		{
			public static string TOKEN_FILE = $@"{TEXT_DIR}token.txt";
		}

		public static DiscordSocketClient _client;
		public static CancellationTokenSource LicenceToLive;
	}
}
