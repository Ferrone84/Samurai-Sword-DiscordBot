namespace Cards
{
	public class Character : Card
	{
		protected short maxLife;

		public short MaxLife { get { return maxLife; } protected set { maxLife = value; } }

		public Character(string name, string picture, string description, short maxLife) : base(name, picture, description)
		{
			MaxLife = maxLife;
		}

		public override string ToString()
		{
			return base.ToString() + $" / [MaxLife] : {maxLife.ToString()}";
		}
	}
}
