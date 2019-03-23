namespace Cards
{
	public abstract class Card
	{
		protected string name;
		protected string picture;
		protected string description;

		public string Name { get { return name; } protected set { name = value; } }
		public string Picture { get { return picture; } protected set { picture = value; } }
		public string Description { get { return description; } protected set { description = value; } }

		protected Card(string name, string picture, string description)
		{
			Name = name;
			Picture = picture;
			Description = description;
		}

		public override string ToString()
		{
			return $"[CardType] : {this.GetType().Name} / [Name] : {Name} / [Picture] : {Picture} / [Desc] : {Description}";
		}
	}
}
