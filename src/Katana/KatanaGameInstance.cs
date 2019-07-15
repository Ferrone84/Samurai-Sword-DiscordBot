using Games;

namespace KatanaGame {
	public class KatanaGameInstance : IGameInstance<KatanaGame, KatanaGameEvent> {
		IGame IGameInstance.Game { get => this.Game; }
		public KatanaGame Game { get; }
		public bool GameOver { get; set; }
		public KatanaGameInstance(KatanaGame game) {
			this.Game = game;
		}
		public async Task Run( ) {

		}
		public void Event(KatanaGameEvent game_event) {
			/* Forward event to current phase */
			throw new System.NotImplementedException();
		}
	}
}