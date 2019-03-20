namespace Cards
{
	public class Weapon : Card
	{
		protected short range;
		protected short damage;

		public short Range { get { return range; } protected set { range = value; } }
		public short Damage { get { return damage; } protected set { damage = value; } }

		public Weapon(string name, string picture, short range, short damage) : base(name, picture, string.Empty)
		{
			Range = range;
			Damage = damage;
			Description = "Arme qui possède " + Damage.ToString() + " point" + ((Damage == 1) ? "" : "s") + " de dégats et " + Range.ToString() + " point" + ((Range == 1) ? "" : "s") + " de portée.";
		}

		public override string ToString()
		{
			return base.ToString() + $" / [Range] : {Range.ToString()} / [Damage] : {Damage.ToString()}";
		}
	}
}
