using Games.Cards;

namespace KatanaGame.Cards.Effects {
	internal class BaseArmorBoost : AKatanaPlayerStatEffect {
		private int amount;
		public BaseArmorBoost(int amount) : base(amount) { }
		public override void Apply(KatanaPlayer player) {
			player.Stats.Armor.Base += this.Amount;
		}
	}
}
