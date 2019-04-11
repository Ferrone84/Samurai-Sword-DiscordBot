namespace KatanaGame {
	public sealed class Role : KatanaCardModel {
		public enum StarRank : short {
			None = 0,
			One = 1,
			Two = 2,
			Three = 3
		}
		private StarRank stars;
		public StarRank Stars { get { return this.stars; } }

		public Role(string name, string description, string picture, StarRank stars) : base(name:name, description:description, picture:picture) {
			this.stars = stars;
		}

		public override string ToString() {
			return base.ToString() + $" / [Stars] : {this.stars.ToString()}";
		}
	}
}