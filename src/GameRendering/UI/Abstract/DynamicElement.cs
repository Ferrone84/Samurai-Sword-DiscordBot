using System;
using System.Drawing;

namespace GameRendering.UI {
	public abstract class DynamicElement<TReceived, TUsed> : GameRendering.UI.Element, GameRendering.UI.IDynamicElement<TReceived> {
		private Func<TReceived, TUsed> converter;
		public DynamicElement(RelativeRectangle origin, Func<TReceived, TUsed> converter) : base(origin) {
			this.converter = converter;
		}
		public void Render(Graphics graphics, Rectangle origin, TReceived data) => this.OnRender(graphics,  origin.Compose(this.Origin), this.converter(data));
		protected virtual void OnRender(Graphics graphics, Rectangle dedicated_rectangle, TUsed data) {}
	}
}
