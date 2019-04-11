namespace KatanaGame {
	public sealed class Character : KatanaCardModel {
		private short max_life;
		public short MaxLife { get { return this.max_life; } }

		public Character(string name, string description, string picture, short maxLife) : base(name:name, description:description, picture:picture) {
			this.max_life = maxLife;
		}

		public override string ToString() {
			return base.ToString() + $" / [MaxLife] : {max_life.ToString()}";
		}
	}
}
