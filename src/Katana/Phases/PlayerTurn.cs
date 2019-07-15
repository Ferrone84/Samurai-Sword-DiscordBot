using System.Threading.Tasks;
using Games;
using KatanaGame.Events;

namespace KatanaGame {
	internal class PlayerTurn : APhasedPhase<KatanaGameInstance, KatanaGameEvent> {
		public PlayerTurn(KatanaGameInstance game_instance) : base(game_instance,
			new PlayerRecoveryPhase(game_instance),
			new PlayerBushidoPhase(game_instance),
			new PlayerDrawPhase(game_instance),
			new PlayerPlayPhase(game_instance),
			new PlayerDiscardPhase(game_instance)
		) { }
		protected override async Task<bool> CheckForPrematureTermination( ) {
			return this.GameInstance.GameOver;
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
