using System;
using System.Drawing;

namespace GameRendering.UI {
	public class Bitmap : GameRendering.UI.FixedElement {
		private readonly System.Drawing.Bitmap bitmap;
		public Bitmap(RelativeRectangle origin, System.Drawing.Bitmap bitmap) : base(origin) {
			this.bitmap = bitmap;
		}
		protected override void OnRender(Graphics graphics, Rectangle dedicated_rectangle) {
			graphics.Compose(this.bitmap, dedicated_rectangle);
			/* %TODO% Render */
		}
	}
}
