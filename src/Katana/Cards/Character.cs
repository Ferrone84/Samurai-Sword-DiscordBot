namespace KatanaGame {
	public sealed class Character : KatanaCardModel {
		public short MaxLife { get; }

		public Character(string name, string description, string picture, short max_life) : base(name:name, description:description, picture:picture) {
			this.MaxLife = max_life;
		}

		public override string ToString() {
			return base.ToString() + $" / [MaxLife] : {this.MaxLife.ToString()}";
		}
	}
}
