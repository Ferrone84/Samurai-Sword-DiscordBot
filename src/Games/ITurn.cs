namespace Games {
	public interface ITurn<in TGameState, TPhase> : IPhased<TPhase>, IPhase<TGameState> where TPhase : IPhase {
		
	}
}