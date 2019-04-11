namespace Games {
	public interface IGameInstance {
		
	}
	public interface IGameInstance<out TGame, TGameState> where TGame : IGame {
		TGame Game();
	}
}
