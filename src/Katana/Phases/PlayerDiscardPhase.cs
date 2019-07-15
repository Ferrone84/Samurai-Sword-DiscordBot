using System;
using System.Threading.Tasks;
using Games;
using KatanaGame.Events;

namespace KatanaGame {
	internal class PlayerDiscardPhase : APhase<KatanaGameInstance, KatanaGameEvent> {
		public PlayerDiscardPhase(KatanaGameInstance game_instance) : base(game_instance) { }
		protected override async Task Setup( ) {
			/* I player has more than 7 cards, warn it has to discard some */
			Console.WriteLine("Do you have more than 7 cards in hand?");
		}
		protected override async Task Proceed( ) {
			/* Gently ask to sacrifice a card while the player has more than 7 cards in hand */
			Console.WriteLine("Sacrificing cards to have at most 7 in hand.");
			await this.Terminate( ); /* %DEBUG% */
		}
		public override async Task Event(KatanaGameEvent katana_event) {
			/* handle card choice */
		}
	}
}
