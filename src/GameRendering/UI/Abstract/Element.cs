namespace GameRendering.UI {
	public abstract class Element : GameRendering.UI.IElement {
		private readonly RelativeRectangle origin;
		public RelativeRectangle Origin {get => this.origin;}
		public Element(RelativeRectangle origin) {
			this.origin = origin;
		}
	}
}
