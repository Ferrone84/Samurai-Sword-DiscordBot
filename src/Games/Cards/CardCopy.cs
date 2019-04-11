namespace Games.Cards {
	public interface ICardCopy {
		ICardModel Model {get;}
	}
	public interface ICardCopy<out TCardModel> : ICardCopy where TCardModel : ICardModel {
		new TCardModel Model {get;}
	}
	public class CardCopy<TCardModel> : ICardCopy<TCardModel> where TCardModel : ICardModel {
		private readonly TCardModel model;
		public CardCopy(TCardModel model) {
			this.model = model;
		}
		ICardModel ICardCopy.Model {get => this.Model;}
		public TCardModel Model {get => this.model;}
	}
}
