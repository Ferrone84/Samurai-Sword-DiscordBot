using Games.Cards;

namespace KatanaGame {
	public abstract class KatanaPlayingCardModel : KatanaCardModel {
		protected KatanaPlayingCardModel(string name, string description, string picture) : base(name:name, description:description, picture:picture) {}
	}
}
