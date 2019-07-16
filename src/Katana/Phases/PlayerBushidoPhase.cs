using System;
using System.Threading.Tasks;
using Games;
using KatanaGame.Events;

namespace KatanaGame {
	internal class PlayerBushidoPhase : APhase<KatanaGameInstance, KatanaGameEvent> {
		public PlayerBushidoPhase(KatanaGameInstance game_instance) : base(game_instance) { }
		protected override async Task Proceed( ) {
			/* if (no bushido) */
			if (new Random( ).Next(16) != 0) {
				/* return */
				await this.Terminate( );
				return;
			}
			/* Draw a card */
			int i = new Random( ).Next(16);
			/* if (card is a weapon) */
			if (i < 6) {
				/* if (player has one or more wapons) */
				if (i < 4) {
					/* choose a weapon card to sacrifice */
					/* discard selected card */
					/* pass on bushido card */
				}
				/* else */
				else {
					/* Lose an honor point */
					Console.WriteLine("Bushido victim!");
					/* discard bushido card */
					/* ** Report honor point lost, report bushido victim ** */
					/* ** If bushido causes loss of last honor point, report game end because of bushido ** */
					this.GameInstance.GameOver |= (new Random( ).Next(16) == 0);
					if (this.GameInstance.GameOver) { Console.WriteLine("It was its last point, how sad."); }
				}
			}
			/* else */
			else {
				/* pass on bushido card */
				/* ** Report bushido dodger ** */
			}
			await this.Terminate( );
		}
	}
}
