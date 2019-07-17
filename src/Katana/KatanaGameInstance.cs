using System;
using System.Threading.Tasks;
using Games;
using Games.Cards;
using KatanaGame.Cards;
namespace KatanaGame {
	internal sealed class KatanaGameInstanceState {
		public bool GameOver { get; set; }
		public KatanaPlayer CurrentPlayer { get; set; }
		public bool IsFirstPlayerTurn { get => (this.CurrentPlayer == this.Players[0]); }
		public bool IsLastPlayerTurn { get => (this.CurrentPlayer == this.Players[this.Players.Length-1]); }
		public KatanaPlayer[] Players { get; set; }
		public Deck<ICardCopy<AKatanaPlayingCardModel>, AKatanaPlayingCardModel> DrawDeck { get; }
		public Deck<ICardCopy<AKatanaPlayingCardModel>, AKatanaPlayingCardModel> DiscardPile { get; }
	}
	public class KatanaGameInstance : IGameInstance<KatanaGame, KatanaGameEvent> {
		private sealed class GamePhases : APhasedPhase<KatanaGameInstanceState, KatanaGameEvent> {
			public GamePhases(KatanaGameInstanceState game_state) : base(game_state,
				new LobbyPhase(game_state),
				new GamePlayPhase(game_state),
				new GameEndPhase(game_state)
			) { }
		}
		IGame IGameInstance.Game { get => this.Game; }
		public KatanaGame Game { get; }
		private KatanaGameInstanceState state;
		private GamePhases game_flow;
		public KatanaGameInstance(KatanaGame game) {
			this.Game = game;
			this.state = new KatanaGameInstanceState( );
			this.state.DrawDeck.Set(game.MakeDefaultDeck( ));

			this.game_flow = new GamePhases(this.state);
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