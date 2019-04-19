using System;
using System.Drawing;

namespace GameRendering.UI {
	public class FixedText : GameRendering.UI.FixedElement {
		private readonly string text;
		private readonly BitmapFont font;
		public FixedText(RelativeRectangle origin, string text, BitmapFont font) : base(origin) {
			this.text = text;
			this.font = font;
		}
		protected override void OnRender(Graphics graphics, Rectangle dedicated_rectangle) {
			graphics.DrawText(this.font, this.text, dedicated_rectangle.X, dedicated_rectangle.Y + dedicated_rectangle.Height/2, this.Origin.Size);
			/* %TODO% Render */
		}
	}
	public class DynamicText<TReceived> : GameRendering.UI.DynamicElement<TReceived, string> {
		private readonly BitmapFont font;
		public DynamicText(RelativeRectangle origin, Func<TReceived, string> text_getter, BitmapFont font) : base(origin, text_getter) {
			this.font = font;
		}
		protected override void OnRender(Graphics graphics, Rectangle dedicated_rectangle, string text) {
			graphics.DrawText(this.font, text, dedicated_rectangle.X + dedicated_rectangle.Width/2, dedicated_rectangle.Y + dedicated_rectangle.Height/2, this.Origin.Size);
			/* %TODO% Render */
		}
	}
}
