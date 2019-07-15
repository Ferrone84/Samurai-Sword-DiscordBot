using System;
using System.Threading.Tasks;
using Games;
using KatanaGame.Events;

namespace KatanaGame {
	internal class GamePlayPhase : APhase<KatanaGameInstance, KatanaGameEvent> {
		private readonly PlayerTurn player_turn;
		public GamePlayPhase(KatanaGameInstance game_instance) : base(game_instance) {
			this.player_turn = new PlayerTurn(game_instance);
		}
		protected override async Task Setup( ) {
			/* %TODO% */
			/*
			 * Insérer ici la création des chanels :
			 *  la catégorie
			 *  1 Chan par player
			 *  
			 *  Créer la pioche, la défausse... bref init du plateau
			 *  Init de chaque joueur
			 *  et distribution des cartes
			 */
			/* Create Category */
			/* Create one channel per player */
			Console.WriteLine("Discord OK"); // -> Should just tell interface ar the end of setup. Game's not supposed to manage this
			/* Assign characters (Shuffle them, then pick one per player) */
			Console.WriteLine("Characters assigned");
			/* Place players in the loop */
			Console.WriteLine("Players placed");
			/* Give cards (Shuffle, then pick amount 4-5-5-6-6-7-7) */
			Console.WriteLine("Deck shuffled");
		}
		protected override async Task Proceed( ) {
			while (!this.Terminated) {
				Console.WriteLine("Running player turn");
				await this.player_turn.Run();
				/* Change player, and let repeat */
				Console.WriteLine("Next player");
			}
		}
		public override async Task Event(KatanaGameEvent katana_event) {
			if (katana_event is CharacterDiedEvent character_died) { }
			else if (katana_event is CharacterRevivedEvent character_revived) { }
			else if (katana_event is CharacterLostHonorPointsEvent character_lost_honor) { }
			else if (katana_event is CharacterGainedHonorPointsEvent character_gained_honor) { }
			else if (katana_event is CharacterLostResiliencePointsEvent	character_lost_resilience) { }
			else if (katana_event is CharacterGainedResiliencePointsEvent character_gained_resilience) { }
			else if (katana_event is CharacterRanoutOfCardsEvent character_ranout_of_cards) { }

		}
		protected override async Task ClearUp( ) {
			/* Count scores */
			Console.WriteLine("Scores are set : We have a winner! And losers!");
			/* Report last events (no deaths) */
			Console.WriteLine("Last events reported. (events relative to game ending)");
		}
		public override sealed async Task Terminate( ) {
			await base.Terminate( );
			Console.WriteLine("The GamePlay phase has now ended.");
		}
	}
}
