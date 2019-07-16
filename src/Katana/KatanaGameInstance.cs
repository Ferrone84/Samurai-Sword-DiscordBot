using System;
using System.Threading.Tasks;
using Games;

namespace KatanaGame {
	public class KatanaGameInstance : IGameInstance<KatanaGame, KatanaGameEvent> {
		private sealed class GamePhases : APhasedPhase<KatanaGameInstance, KatanaGameEvent> {
			public GamePhases(KatanaGameInstance game_instance) : base(game_instance,
				new LobbyPhase(game_instance),
				new GamePlayPhase(game_instance),
				new GameEndPhase(game_instance)
			) { }
		}
		IGame IGameInstance.Game { get => this.Game; }
		public KatanaGame Game { get; }
		public bool GameOver { get; set; }
		private GamePhases game_flow;
		public KatanaGameInstance(KatanaGame game) {
			this.Game = game;
			this.game_flow = new GamePhases(this);
			this.GameOver = false;
		}
		public async Task Run( ) {
			await this.game_flow.Run( );
		}
		public void Event(KatanaGameEvent game_event) {
			/* Forward event to current phase */
			throw new System.NotImplementedException();
		}
	}
}