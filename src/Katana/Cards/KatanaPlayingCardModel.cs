using Games.Cards;

namespace KatanaGame.Cards {
	public abstract class AKatanaPlayingCardModel : AKatanaCardModel {
		protected AKatanaPlayingCardModel(string name, string description, string picture) : base(name:name, description:description, picture:picture) {}
	}
}
