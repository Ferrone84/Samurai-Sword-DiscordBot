namespace Games {
	public interface IGame : IPhased {
		string Name {get;}
		string Description {get;}
		string Rules {get;}
		/*
			Title string
			Content string[]
			Subs []
		 */
		IGameInProgress New();
	}
}
