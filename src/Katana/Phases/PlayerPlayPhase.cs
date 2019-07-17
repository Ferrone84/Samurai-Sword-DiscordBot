using System;
using System.Threading.Tasks;
using Games;
using KatanaGame.Events;

namespace KatanaGame {
	internal class PlayerPlayPhase : APhase<KatanaGameInstanceState, KatanaGameEvent> {
		public PlayerPlayPhase(KatanaGameInstanceState game_state) : base(game_state) { }
		protected override async Task Setup( ) {
			Console.WriteLine("Now play cards as you wish");
		}
		protected override async Task Proceed( ) {
			/* No many things to do...
			 * The main action is in the setup, telling the player
			 * what he/she can do.
			 * everything is handled via events, so let base handle this for us
			 */
			if (new Random( ).Next(128) == 0) {
				Console.WriteLine("Someone has no more honor points, ending game.");
				this.GameState.GameOver = true;
			}
			await this.Terminate( ); /* %DEBUG% */
			await base.Proceed( );
		}
		public override async Task Event(KatanaGameEvent katana_event) {
			/*
			 * Handle all cases
			 * -> player wants to play some card
			 * -> player wants to end its turn
			 * -> player wants to use a special ability (
			 */
		}
	}
}
