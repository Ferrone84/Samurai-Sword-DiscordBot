using System.Threading.Tasks;
using Games;
using KatanaGame.Events;

namespace KatanaGame {
	internal class PlayerRecoveryPhase : APhase<KatanaGameInstance, KatanaGameEvent> {
		public PlayerRecoveryPhase(KatanaGameInstance game_instance) : base(game_instance) { }
		protected override async Task Proceed( ) {
			/* Revive player if dead */
			/* ** Report player resurection ** */
			await this.Terminate( );
		}
	}
}
