using System.Collections.Generic;

namespace Games.Cards {
	public interface ICardModel {
		string Name {get;}
		string Description {get;}
	}
	public interface ICardModel<out TEffect> : ICardModel {
		IEnumerable<TEffect> Effects {get;}
	}
}
