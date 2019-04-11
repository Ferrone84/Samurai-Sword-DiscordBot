using Games.Cards;

namespace KatanaGame {
	public abstract class KatanaCardModel : ICardModel {
		protected string name;
		protected string picture;
		protected string description;

		public string Name { get { return this.name; } }
		public string Picture { get { return this.picture; } }
		public string Description { get { return this.description; } }

		protected KatanaCardModel(string name, string description, string picture) {
			this.name = name;
			this.description = description;
			this.picture = picture;
		}

		public override string ToString() {
			return $"[CardType] : {this.GetType().Name} / [Name] : {Name} / [Picture] : {Picture} / [Desc] : {Description}";
		}
	}
	public abstract class KatanaPlayingCardModel : KatanaCardModel {
		protected KatanaPlayingCardModel(string name, string description, string picture) : base(name:name, description:description, picture:picture) {}
	}
}
