namespace KatanaGame {
	public sealed class Weapon : KatanaPlayingCardModel {
		private short range;
		private short damage;

		public short Range { get { return this.range; } }
		public short Damage { get { return this.damage; } }
		private static string GenerateDescription(short range, short damage) {
			return $"Arme qui inflige {damage} blessure{((damage <= 1) ? "" : "s")} et a portée de {range}.";
		}
		public Weapon(string name, string picture, short range, short damage) : base(name:name, description:Weapon.GenerateDescription(range, damage), picture:picture) {
			this.range = range;
			this.damage = damage;
		}

		public override string ToString() {
			return base.ToString() + $" / [Range] : {Range.ToString()} / [Damage] : {Damage.ToString()}";
		}
	}
}
