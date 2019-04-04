using System.Drawing;

namespace GameRendering.UI {
	public interface IFixedElement : GameRendering.UI.IElement {
		void Render(Graphics graphics, Rectangle origin);
	}
}
