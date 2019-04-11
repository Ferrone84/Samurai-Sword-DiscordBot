using System.Collections.Generic;

namespace Games.Cards {
	public interface IDeckedPlayer : IPlayer {
		IEnumerable<ICardCopy> Deck {get;}
	}
	public interface IDeckedPlayer<out TCardCopy> : IDeckedPlayer where TCardCopy : ICardCopy {
		new IEnumerable<TCardCopy> Deck {get;}
	}
}
