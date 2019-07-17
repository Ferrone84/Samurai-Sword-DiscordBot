using Games.Cards;

namespace KatanaGame.Cards {
	public abstract class AKatanaCardModel : ICardModel {
		public string Name { get; }
		public string Picture { get; }
		public string Description { get; }

		protected AKatanaCardModel(string name, string description, string picture) {
			this.Name = name;
			this.Description = description;
			this.Picture = picture;
		}

		public override string ToString() {
			return $"[CardType] : {this.GetType().Name} / [Name] : {this.Name} / [Picture] : {this.Picture} / [Desc] : {this.Description}";
		}
	}
}
