using System;
using System.Drawing;

namespace GameRendering.UI {
	public class FixedBitmap : GameRendering.UI.FixedElement {
		private readonly ContentAlignment alignement;
		private readonly System.Drawing.Bitmap bitmap;
		public FixedBitmap(RelativeRectangle origin, System.Drawing.Bitmap bitmap, ContentAlignment alignement=ContentAlignment.MiddleCenter) : base(origin) {
			this.bitmap = bitmap;
			this.alignement = alignement;
		}
		protected override void OnRender(Graphics graphics, Rectangle dedicated_rectangle) {
			dedicated_rectangle = dedicated_rectangle.Compose(((0, 0, this.alignement), (this.bitmap.Width, this.bitmap.Height, this.alignement)));
			graphics.Compose(this.bitmap, dedicated_rectangle);
			/* %TODO% Render */
		}
	}
	public class DynamicBitmap<TReceived> : GameRendering.UI.DynamicElement<TReceived, System.Drawing.Bitmap> {
		private readonly ContentAlignment alignement;
		public DynamicBitmap(RelativeRectangle origin, Func<TReceived, System.Drawing.Bitmap> bitmap_getter, ContentAlignment alignement=ContentAlignment.MiddleCenter) : base(origin, bitmap_getter) {
			this.alignement = alignement;
		}
		protected override void OnRender(Graphics graphics, Rectangle dedicated_rectangle, System.Drawing.Bitmap bitmap) {
			dedicated_rectangle = dedicated_rectangle.Compose(((0, 0, this.alignement), (bitmap.Width, bitmap.Height, this.alignement)));
			graphics.Compose(bitmap, dedicated_rectangle);
			/* %TODO% Render */
		}
	}
}
