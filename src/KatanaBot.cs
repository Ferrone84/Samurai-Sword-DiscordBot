using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using Events.EventsHandling;
using KatanaGame;
using KatanaBot.Data;

namespace KatanaBot {
	public class KatanaBot {
		private EventHandlersManager eventHandlersManager;

		public async Task MainAsync() {
			new KatanaUI();
			return;
			DiscordSocketConfig discordSocketConfig = new DiscordSocketConfig()
			{
				MessageCacheSize = 100
			};

			DataManager.Client = new DiscordSocketClient(discordSocketConfig);
			eventHandlersManager = new EventHandlersManager(DataManager.Client);
			DataManager.Client.Log += Log;
			try {
				eventHandlersManager.AddHandlers(GetAllEventsHandlers("Events.EventsHandlers").ToArray());
			} catch (Exception e) { e.Display("MainAsync() => EventHandlersManager.AddHandlers"); }

			DataManager.LicenceToLive = new CancellationTokenSource();
			await DataManager.Client.LoginAsync(TokenType.Bot, Utils.Token);
			await DataManager.Client.StartAsync();

			Console.CancelKeyPress += async delegate (object sender, ConsoleCancelEventArgs e) {
				e.Cancel = true;
				await Deconnection();
			};

			// Block this task until the program is closed.
			try {
				await Task.Delay(-1, DataManager.LicenceToLive.Token);
			} catch (TaskCanceledException) {
				await Deconnection();
			}
		}

		private async Task Deconnection() {
			try {
				Console.WriteLine("Le bot a bien été coupé.");
				eventHandlersManager.Unbind(DataManager.Client);
				DataManager.Client.Log -= Log;
				await DataManager.Client.LogoutAsync();
				await DataManager.Client.StopAsync();
				DataManager.Client.Dispose();
				Environment.Exit(0);
			} catch (Exception e) {
				e.Display(MethodBase.GetCurrentMethod().ToString());
			}
		}

		private Task Log(LogMessage msg) {
			Console.WriteLine(msg.ToString());

			return Task.CompletedTask;
		}

		private List<IEventHandler> GetAllEventsHandlers(string nameSpace) {
			List<IEventHandler> eventsHandlers = new List<IEventHandler>();

			try {
				var types = Assembly.GetExecutingAssembly().GetTypes();
				var filtered_types = types.Where(
					t => ((t.Namespace != null) && t.Namespace.StartsWith(nameSpace))
				);
				foreach (var t in filtered_types) {
					if (!t.Name.Contains("d_")) {
						eventsHandlers.Add((t.GetConstructor(Type.EmptyTypes).Invoke(Type.EmptyTypes) as IEventHandler));
					}
				}
			} catch (Exception e) {
				e.Display(MethodBase.GetCurrentMethod().ToString());
			}
			return eventsHandlers;
		}
	}
}
