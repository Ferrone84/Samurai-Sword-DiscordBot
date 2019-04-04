using System.Drawing;
using GameRendering.UI;

namespace GameRendering {
	public static class DrawingExtensions {
		public static Point Compose(this Rectangle rectangle, RelativePoint relative_point) {
			int x, y;
			x = rectangle.X + relative_point.X;
			var h_align = relative_point.Alignment.Horizontal();
			if (h_align == HorizontalAlignment.Center) {x += rectangle.Width/2;}
			else if (h_align == HorizontalAlignment.Right) {x += rectangle.Width;}

			y = rectangle.Y + relative_point.Y;
			var v_align = relative_point.Alignment.Vertical();
			if (v_align == VerticalAlignment.Middle) {y += rectangle.Height/2;}
			else if (v_align == VerticalAlignment.Bottom) {y += rectangle.Height;}

			return new Point(x, y);
		}
		public static Rectangle Compose(this Rectangle rectangle, RelativeRectangle relative_rectangle) {
			var point = rectangle.Compose(relative_rectangle.Offset);
			int x = point.X, y = point.Y;
			x = rectangle.X + relative_rectangle.Offset.X;
			var h_align = relative_rectangle.Size.Alignment.Horizontal();
			if (h_align == HorizontalAlignment.Center) {x -= relative_rectangle.Width/2;}
			else if (h_align == HorizontalAlignment.Right) {x -= relative_rectangle.Width;}

			y = rectangle.Y + relative_rectangle.Offset.Y;
			var v_align = relative_rectangle.Size.Alignment.Vertical();
			if (v_align == VerticalAlignment.Middle) {y -= relative_rectangle.Height/2;}
			else if (v_align == VerticalAlignment.Bottom) {y -= relative_rectangle.Height;}

			return new Rectangle(x, y, relative_rectangle.Width, relative_rectangle.Height);
		}
		/* Fucking Microsoft figured out how to fuck it even more *STARTING TO HATE CSHARP* */
		public static Graphics Compose(this Graphics graphics, System.Drawing.Bitmap bmp, int x, int y) {
			graphics.DrawImage(bmp, x, y, bmp.Width, bmp.Height);
			return graphics;
		}
		public static Graphics Compose(this Graphics graphics, System.Drawing.Bitmap bmp, Rectangle dest_rect) {
			graphics.DrawImage(bmp, dest_rect);
			return graphics;
		}
		public static Graphics Compose(this Graphics graphics, System.Drawing.Bitmap bmp, Rectangle source_rect, Rectangle dest_rect) {
			graphics.DrawImage(bmp, dest_rect, source_rect, GraphicsUnit.Pixel);
			return graphics;
		}
		public static Graphics Compose(this Graphics graphics, System.Drawing.Bitmap bmp, RectangleF source_rect, RectangleF dest_rect) {
			graphics.DrawImage(bmp, dest_rect, source_rect, GraphicsUnit.Pixel);
			return graphics;
		}
		public static HorizontalAlignment Horizontal(this ContentAlignment alignment) {
			if (alignment == ContentAlignment.BottomCenter) {return HorizontalAlignment.Center;}
			if (alignment == ContentAlignment.BottomLeft  ) {return HorizontalAlignment.Left;  }
			if (alignment == ContentAlignment.BottomRight ) {return HorizontalAlignment.Right; }
			if (alignment == ContentAlignment.MiddleCenter) {return HorizontalAlignment.Center;}
			if (alignment == ContentAlignment.MiddleLeft  ) {return HorizontalAlignment.Left;  }
			if (alignment == ContentAlignment.MiddleRight ) {return HorizontalAlignment.Right; }
			if (alignment == ContentAlignment.TopCenter   ) {return HorizontalAlignment.Center;}
			if (alignment == ContentAlignment.TopLeft     ) {return HorizontalAlignment.Left;  }
			if (alignment == ContentAlignment.TopRight    ) {return HorizontalAlignment.Right; }
			return HorizontalAlignment.Left;
		}
		public static VerticalAlignment Vertical(this ContentAlignment alignment) {
			if (alignment == ContentAlignment.BottomCenter) {return VerticalAlignment.Bottom;}
			if (alignment == ContentAlignment.BottomLeft  ) {return VerticalAlignment.Bottom;}
			if (alignment == ContentAlignment.BottomRight ) {return VerticalAlignment.Bottom;}
			if (alignment == ContentAlignment.MiddleCenter) {return VerticalAlignment.Middle;}
			if (alignment == ContentAlignment.MiddleLeft  ) {return VerticalAlignment.Middle;}
			if (alignment == ContentAlignment.MiddleRight ) {return VerticalAlignment.Middle;}
			if (alignment == ContentAlignment.TopCenter   ) {return VerticalAlignment.Top;   }
			if (alignment == ContentAlignment.TopLeft     ) {return VerticalAlignment.Top;   }
			if (alignment == ContentAlignment.TopRight    ) {return VerticalAlignment.Top;   }
			return VerticalAlignment.Top;
		}
		public static void DrawNumber(this Graphics g, BitmapFont font, int number, int x, int y, RelativeSize size, bool enforce_plus=false, bool dashzero=false) {
			string str = ((number == 0) && dashzero ? "-" : ((enforce_plus && (number >= 0)) ? "+" : "") + number.ToString());
			g.DrawText(font, str, x, y, size);
		}
		public static void DrawText(this Graphics g, BitmapFont font, string str, int x, int y, RelativeSize size) {
			var text_bmp = font.MakeText(str);
			var src = new RectangleF(0, 0, text_bmp.Width, text_bmp.Height);
			float fx = size.Height * 1.0F / text_bmp.Height;
			float f_x = x, f_w = text_bmp.Width * fx;
			float f_y = y, f_h = text_bmp.Height * fx;

			var h_align = size.Alignment.Horizontal();
			var v_align = size.Alignment.Vertical();

			if (h_align == HorizontalAlignment.Right) {f_x -= f_w;}
			else if (h_align == HorizontalAlignment.Center) {f_x -= f_w * 0.5F;}
			if (v_align == VerticalAlignment.Bottom) {f_y -= f_h;}
			else if (v_align == VerticalAlignment.Middle) {f_y -= f_h * 0.5F;}
			var trg = new RectangleF(f_x, f_y, f_w, f_h);
			g.Compose(text_bmp, src, trg);
		}
	}
}
