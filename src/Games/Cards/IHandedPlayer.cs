using System.Collections.Generic;

namespace Games.Cards {
	public interface IHandedPlayer : IPlayer {
		IEnumerable<ICardCopy> Hand {get;}
	}
	public interface IHandedPlayer<out TCardCopy> : IHandedPlayer where TCardCopy : ICardCopy  {
		new IEnumerable<TCardCopy> Hand {get;}
	}
}
