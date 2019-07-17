using System.Threading.Tasks;
using Games;
using KatanaGame.Events;

namespace KatanaGame {
	internal class PlayerRecoveryPhase : APhase<KatanaGameInstanceState, KatanaGameEvent> {
		public PlayerRecoveryPhase(KatanaGameInstanceState game_state) : base(game_state) { }
		protected override async Task Proceed( ) {
			/* Revive player if dead */
			/* ** Report player resurection ** */
			await this.Terminate( );
		}
	}
}
