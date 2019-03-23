namespace Cards
{
	public class Buff : Card
	{
		public enum Type
		{
			Armor,
			Damage,
			WeaponNumber
		}

		protected Type buffType;

		public Type BuffType { get { return buffType; } protected set { buffType = value; } }

		public Buff(string name, string picture, string description, Type type) : base(name, picture, description)
		{
			BuffType = type;
		}

		public override string ToString()
		{
			return base.ToString() + $" / [Type] : {BuffType.ToString()}";
		}
	}
}
