using System;
using System.Threading.Tasks;
using Games;
using KatanaGame.Events;

namespace KatanaGame {
	internal class LobbyPhase : APhase<KatanaGameInstance, KatanaGameEvent> {
		public LobbyPhase(KatanaGameInstance game_instance) : base(game_instance) { }
		protected override async Task Setup( ) {
			/*
			 * Initialize lobby
			 * Void players list (mabe not) -> if replay option
			 * add creator as master (maybe not) -> if replay option
			 * reset gamemode
			 * 
			 * Option to select character?
			 */
			Console.WriteLine("Initialised Lobby.");
			Console.WriteLine("Game Master has been set.");
			await this.Terminate( ); /* %DEBUG% */
		}
		public override async Task Event(KatanaGameEvent katana_event) {
			if (this.Terminated) { return; }
			if (katana_event is PlayerJoinsEvent player_joins) { /* Add player */ }
			else if (katana_event is PlayerLeavesEvent player_leaves) { /* Remove player */ }
			else if (katana_event is GameLaunchEvent game_launch) { }
		}
		public override async Task Terminate( ) {
			await base.Terminate( );
			Console.WriteLine("Lobby is now closed.");
		}
		protected override async Task ClearUp( ) {
			Console.WriteLine("Lobby Ended.");
		}
	}
}
