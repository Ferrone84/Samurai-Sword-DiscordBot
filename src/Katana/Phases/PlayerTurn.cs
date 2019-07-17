using System.Threading.Tasks;
using Games;
using KatanaGame.Events;

namespace KatanaGame {
	internal class PlayerTurn : APhasedPhase<KatanaGameInstanceState, KatanaGameEvent> {
		public PlayerTurn(KatanaGameInstanceState game_state) : base(game_state,
			new PlayerRecoveryPhase(game_state),
			new PlayerBushidoPhase(game_state),
			new PlayerDrawPhase(game_state),
			new PlayerPlayPhase(game_state),
			new PlayerDiscardPhase(game_state)
		) { }
		protected override async Task<bool> CheckForPrematureTermination( ) {
			return this.GameState.GameOver;
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
	}
}
