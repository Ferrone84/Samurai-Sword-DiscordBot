using System.Collections.Generic;

namespace Games.Cards {
	public interface IDeck {
		IEnumerable<ICardCopy> Cards {get;}
	}
	public interface IDeck<TCardModel> : IDeck where TCardModel : ICardModel {
		new IEnumerable<ICardCopy<TCardModel>> Cards {get;}
	}
	public interface IDeck<TCardCopy, TCardModel> : IDeck<TCardModel> where TCardModel : ICardModel where TCardCopy : CardCopy<TCardModel> {
		new IEnumerable<TCardCopy> Cards {get;}
	}
	public class Deck<TCardCopy, TCardModel> : IDeck<TCardCopy, TCardModel> where TCardModel : ICardModel where TCardCopy : CardCopy<TCardModel> {
		private List<TCardCopy> cards = new List<TCardCopy>();
		public Deck(IEnumerable<TCardCopy> initial_cards) {
			this.cards.AddRange(initial_cards);
		}
		public Deck() { }
		IEnumerable<ICardCopy> IDeck.Cards {get => this.cards;}
		IEnumerable<ICardCopy<TCardModel>> IDeck<TCardModel>.Cards {get => this.cards;}
		public IEnumerable<TCardCopy> Cards {get => this.cards;}
	}
}
