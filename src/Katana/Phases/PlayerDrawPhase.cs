using System;
using System.Threading.Tasks;
using Games;
using KatanaGame.Events;

namespace KatanaGame {
	internal class PlayerDrawPhase : APhase<KatanaGameInstance, KatanaGameEvent> {
		public PlayerDrawPhase(KatanaGameInstance game_instance) : base(game_instance) { }
		protected override async Task Setup( ) {
			/* if player can choose, let it choose, else, force */
			await this.Terminate( );
		}
		protected override async Task Proceed( ) {
			/* No many things to do...
			 * The main action is in the setup, telling the player
			 * what he/she can do.
			 * everything is handled via events, so let base handle this for us
			 */
			await this.Terminate( ); /* %DEBUG% */
			await base.Proceed( );
		}
		public override async Task Event(KatanaGameEvent katana_event) {
			/*
			 * Handle all cases
			 * -> player wants to draw from default pile
			 * -> player wants to draw from discard pile
			 */
		}
		protected override async Task ClearUp( ) {
			Console.WriteLine("Drawn cards");
		}
	}
}
