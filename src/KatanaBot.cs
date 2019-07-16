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
		private EventHandlersManager event_handlers_manager;

		public async Task MainAsync( ) {
			await new KatanaGame.KatanaGame( ).NewGame( ).Run( );
			return;
			await this.Setup( );
			await this.Run();
			await this.CleanUp();
		}
		private async Task Setup( ) {
			DiscordSocketConfig discord_socket_config = new DiscordSocketConfig() {
				MessageCacheSize = 100
			};

			DataManager.Client = new DiscordSocketClient(discord_socket_config);
			this.event_handlers_manager = new EventHandlersManager(DataManager.Client);
			DataManager.Client.Log += this.Log;
			try {
				this.event_handlers_manager.AddHandlers(this.GetAllEventsHandlers("Events.EventsHandlers").ToArray());
			}
			catch (Exception e) { e.Display("MainAsync() => EventHandlersManager.AddHandlers"); }

			DataManager.LicenceToLive = new CancellationTokenSource();
			await DataManager.Client.LoginAsync(TokenType.Bot, Utils.Token);
			await DataManager.Client.StartAsync( );

			Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e) {
				e.Cancel = true;
				DataManager.LicenceToLive.Cancel();
				//await Deconnection();
			};
		}
		private async Task Run( ) {
			try { await Task.Delay(-1, DataManager.LicenceToLive.Token); }
			catch (TaskCanceledException) { }
		}
		private async Task CleanUp( ) {
			await this.Deconnection();
		}
		private async Task Deconnection( ) {
			try {
				this.event_handlers_manager.Unbind(DataManager.Client);
				DataManager.Client.Log -= this.Log;
				await DataManager.Client.LogoutAsync();
				await DataManager.Client.StopAsync();
				Console.WriteLine("Le bot a bien été coupé.");
				DataManager.Client.Dispose();
				Environment.Exit(0);
			}
			catch (Exception e) {
				e.Display(MethodBase.GetCurrentMethod().ToString());
			}
		}

		private Task Log(LogMessage msg) {
			Console.WriteLine(msg.ToString());

			return Task.CompletedTask;
		}

		private List<IEventHandler> GetAllEventsHandlers(string name_space) {
			List<IEventHandler> events_handlers = new List<IEventHandler>();

			try {
				var types = Assembly.GetExecutingAssembly().GetTypes();
				var filtered_types = types.Where(
					t => ((t.Namespace != null) && t.Namespace.StartsWith(name_space))
				);
				foreach (var t in filtered_types) {
					if (!t.Name.Contains("d_")) {
						events_handlers.Add((t.GetConstructor(Type.EmptyTypes).Invoke(Type.EmptyTypes) as IEventHandler));
					}
				}
			}
			catch (Exception e) {
				e.Display(MethodBase.GetCurrentMethod().ToString());
			}
			return events_handlers;
		}
	}
}
