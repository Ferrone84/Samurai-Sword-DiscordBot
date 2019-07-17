using System.Collections.Generic;
using CollectionExtensions;

namespace Games.Cards {
	public interface IDeck {
		IEnumerable<ICardCopy> Cards {get;}
	}
	public interface IDeck<out TCardModel> : IDeck where TCardModel : ICardModel {
		new IEnumerable<ICardCopy<TCardModel>> Cards {get;}
	}
	public interface IDeck<out TCardCopy, out TCardModel> : IDeck<TCardModel> where TCardModel : ICardModel where TCardCopy : class, ICardCopy<TCardModel> {
		new IEnumerable<TCardCopy> Cards {get;}
	}
	public class Deck<TCardCopy, TCardModel> : IDeck<TCardCopy, TCardModel> where TCardModel : ICardModel where TCardCopy : class, ICardCopy<TCardModel> {
		private LinkedList<TCardCopy> pile;
		public Deck(IEnumerable<TCardCopy> initial_cards) {
			this.pile = new LinkedList<TCardCopy>(initial_cards);
		}
		public Deck() { }
		IEnumerable<ICardCopy> IDeck.Cards { get {
				foreach (TCardCopy card in this.pile) { yield return card; }
				yield break;
			}
		}
		IEnumerable<ICardCopy<TCardModel>> IDeck<TCardModel>.Cards {
			get {
				foreach (TCardCopy card in this.pile) { yield return card; }
				yield break;
			}
		}
		public IEnumerable<TCardCopy> Cards {
			get {
				foreach (TCardCopy card in this.pile) { yield return card; }
				yield break;
			}
		}
		public bool IsEmpty( ) { return this.pile.IsEmpty( ); }
		public TCardCopy Draw( ) { return this.pile.Shift( ); }
		public bool TryDraw(out TCardCopy drawn_card) { return this.pile.TryShift(out drawn_card); }
		public TCardCopy TopCard { get => ((this.pile.Count == 0) ? null : this.pile.First.Value); }
		public void PutOnTop(TCardCopy card) { this.pile.AddFirst(card); }
		public void PutUnder(TCardCopy card) { this.pile.AddLast(card); }
		public void Set(IEnumerable<TCardCopy> cards) {
			this.pile = new LinkedList<TCardCopy>(cards);
		}
		public void Shuffle( ) {
			this.pile = new LinkedList<TCardCopy>(this.pile.Shuffled( ));
		}
	}
}
