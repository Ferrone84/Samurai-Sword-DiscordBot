namespace KatanaGame {
	public sealed class Action : KatanaPlayingCardModel {
		public Action(string name, string description, string picture, object effects=null) : base(name:name, description:description, picture:picture) {
			/* %TODO% Handle effects */
		}
	}
}
