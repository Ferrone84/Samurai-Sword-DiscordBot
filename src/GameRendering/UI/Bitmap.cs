using System;
using System.Drawing;

namespace GameRendering.UI {
	public class FixedBitmap : GameRendering.UI.FixedElement {
		private readonly System.Drawing.Bitmap bitmap;
		public FixedBitmap(RelativeRectangle origin, System.Drawing.Bitmap bitmap) : base(origin) {
			this.bitmap = bitmap;
		}
		protected override void OnRender(Graphics graphics, Rectangle dedicated_rectangle) {
			graphics.Compose(this.bitmap, dedicated_rectangle);
			/* %TODO% Render */
		}
	}
	public class DynamicBitmap<TReceived> : GameRendering.UI.DynamicElement<TReceived, System.Drawing.Bitmap> {
		public DynamicBitmap(RelativeRectangle origin, Func<TReceived, System.Drawing.Bitmap> bitmap_getter) : base(origin, bitmap_getter) {}
		protected override void OnRender(Graphics graphics, Rectangle dedicated_rectangle, System.Drawing.Bitmap bitmap) {
			graphics.Compose(bitmap, dedicated_rectangle);
			/* %TODO% Render */
		}
	}
}
