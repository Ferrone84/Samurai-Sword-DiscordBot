using System.Drawing;

namespace GameRendering.UI {
	public abstract class FixedElement : GameRendering.UI.Element, GameRendering.UI.IFixedElement {
		public FixedElement(RelativeRectangle origin) : base(origin) {}
		public void Render(Graphics graphics, Rectangle origin) => this.OnRender(graphics, origin.Compose(this.Origin));
		protected virtual void OnRender(Graphics graphics, Rectangle dedicated_rectangle) {}
	}
}
