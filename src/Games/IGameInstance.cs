namespace Games {
	public interface IGameInstance {
		IGame Game { get; }
	}
	public interface IGameInstance<out TGame, TGameEvent> : IGameInstance where TGame : IGame {
		new TGame Game { get; }
		void Event(TGameEvent game_event);
	}
}
