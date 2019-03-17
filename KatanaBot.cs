using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.Commands;
using Discord.WebSocket;

using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace DiscordBot {
	class KatanaBot {
		[Flags]
		private enum RunType {
			DO_NOT_RUN = 0,
			TESTS = 1,
			CONFIG = 2,
			RUN = 4
		}
		private static readonly RunType __RunType__ = RunType.RUN;
		internal static Random Random = new Random();
		internal readonly static DiscordSocketClient Client = new DiscordSocketClient();
		internal readonly static AudioPlayer AudioPlayer = new AudioPlayer();
		// internal readonly static EventHandlersManager EventHandlersManager = new EventHandlersManager(Client);
		internal readonly static CancellationTokenSource LicenceToLive = new CancellationTokenSource();

		static void Main(string[] args) {
			if (__RunType__ == RunType.DO_NOT_RUN) {Console.WriteLine("ABOOORT"); return;}
			if ((__RunType__ & RunType.TESTS) == RunType.TESTS) {BotTests.Run();}
			if ((__RunType__ & RunType.CONFIG) == RunType.CONFIG) {BotConfig.Run();}
			if ((__RunType__ & RunType.RUN) == RunType.RUN) {new KatanaBot().MainAsync().GetAwaiter().GetResult();}
		}
		public async Task MainAsync() {
			Client.Log += Log;
			/* Startup */
			await Client.LoginAsync(TokenType.Bot, BotConfig.Token);
			await Client.StartAsync();
			try {await Task.Delay(-1, LicenceToLive.Token);}
			catch (TaskCanceledException) {}
			/* Shutdown */
			await Client.LogoutAsync();
			await Client.StopAsync();
			Client.Dispose();
		}
		public static void Print(string message, ConsoleColor color) {
			Console.ForegroundColor = color;
			Console.WriteLine(message);
			Console.ResetColor();
		}
		private static Task Log(LogMessage arg) {
			switch (arg.Severity) {
				case LogSeverity.Critical:
				case LogSeverity.Error:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				case LogSeverity.Debug:
				case LogSeverity.Verbose:
					Console.ForegroundColor = ConsoleColor.Gray;
					break;
				case LogSeverity.Warning:
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
				case LogSeverity.Info:
					Console.ForegroundColor = ConsoleColor.Green;
					break;
				default:
					break;
			}
			Console.WriteLine($"[{arg.Severity}] [{arg.Source}] [{arg.Message}]");
			if (arg.Exception != null) {
				Console.WriteLine($"Error : {arg.Exception.Message}");
				if (arg.Exception.Data != null) {
					foreach (var key in arg.Exception.Data.Keys) {
						Console.WriteLine($" :: {key} -> {arg.Exception.Data[key]}");
					}
				}
			}
			if ((arg.Severity == LogSeverity.Warning) && arg.Message.EndsWith("Channel=451118099207421992).")) {LicenceToLive.Cancel();}
			
			Console.ResetColor();
			return Task.CompletedTask;
		}
	}
}
