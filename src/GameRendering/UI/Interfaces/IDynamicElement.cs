using System.Drawing;

namespace GameRendering.UI {
	public interface IDynamicElement<in TReceived> : GameRendering.UI.IElement {
		void Render(Graphics graphics, Rectangle origin, TReceived data);
	}
}
