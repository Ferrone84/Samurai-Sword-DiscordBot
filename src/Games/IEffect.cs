namespace Games {
	public interface IEffect<in TGameInstance> where TGameInstance : IGameInstance {
		
	}
	public interface IEffect<in TGameInstance, in TGameContext> : IEffect<TGameInstance> where TGameInstance : IGameInstance {
		
	}
}