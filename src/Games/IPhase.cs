namespace Games {
	public interface IPhase {
		
	}
	public interface IPhase<in TGameState> {
		void Apply(TGameState game_state);
	}
}
