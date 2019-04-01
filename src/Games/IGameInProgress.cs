namespace Games {
	public interface IGameInProgress {
		
	}
	public interface IGameInProgress<out TGame, TGameState> where TGame : IGame {
		TGame Game();
	}
}
