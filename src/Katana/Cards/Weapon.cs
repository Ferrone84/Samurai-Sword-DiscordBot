namespace KatanaGame {
	public sealed class Weapon : KatanaPlayingCardModel {
		public short Range { get; }
		public short Damage { get; }
		private static string GenerateDescription(short range, short damage) {
			return $"Arme qui inflige {damage} blessure{((damage <= 1) ? "" : "s")} et a portée de {range}.";
		}
		public Weapon(string name, string picture, short range, short damage) : base(name:name, description:Weapon.GenerateDescription(range, damage), picture:picture) {
			this.Range = range;
			this.Damage = damage;
		}

		public override string ToString() {
			return base.ToString() + $" / [Range] : {Range.ToString()} / [Damage] : {Damage.ToString()}";
		}
	}
}
