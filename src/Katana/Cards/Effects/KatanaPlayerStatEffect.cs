using Games;
using Games.Cards;

namespace KatanaGame.Cards.Effects {
	internal abstract class AKatanaPlayerStatEffect : IEffect<KatanaPlayer> {
		protected int Amount { get; }
		public AKatanaPlayerStatEffect(int amount) {
			this.Amount = amount;
		}
		public abstract void Apply(KatanaPlayer player);
	}
}
