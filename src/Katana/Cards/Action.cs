namespace KatanaGame.Cards {
	public sealed class Action : AKatanaPlayingCardModel {
		public Action(string name, string description, string picture, object effects=null) : base(name:name, description:description, picture:picture) {
			/* %TODO% Handle effects */
		}
	}
}
