namespace KatanaGame.Cards {
	public sealed class Character : AKatanaCardModel {
		public short Resilience { get; }

		public Character(string name, string description, string picture, short resilience) : base(name:name, description:description, picture:picture) {
			this.Resilience = resilience;
		}

		public override string ToString() {
			return base.ToString() + $" / [Resilience] : {this.Resilience.ToString()}";
		}
	}
}
