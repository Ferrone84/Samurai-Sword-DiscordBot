namespace Cards
{
	public class Role : Card
	{
		public enum MultiplierType : short
		{
			one = 1,
			two = 2,
			three = 3
		}

		protected short honor;
		protected MultiplierType multiplier;

		public short Honor { get { return honor; } protected set { honor = value; } }
		public MultiplierType Multiplier { get { return multiplier; } protected set { multiplier = value; } }

		public Role(string name, string picture, string description, short honor, MultiplierType multiplier) : base(name, picture, description)
		{
			Honor = honor;
			Multiplier = multiplier;
		}

		public override string ToString()
		{
			return base.ToString() + $" / [Honor] : {Honor.ToString()} / [Multiplier] : {Multiplier.ToString()}";
		}
	}
}
