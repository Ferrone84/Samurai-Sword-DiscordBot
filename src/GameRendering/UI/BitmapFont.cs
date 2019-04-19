using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace GameRendering.UI {
	public class BitmapFont {
		private readonly System.Drawing.Bitmap charset;
		private readonly ConcurrentDictionary<char, (Rectangle Outer, Rectangle Inner)> chars_rects;
		public BitmapFont(System.Drawing.Bitmap charset, IDictionary<char, (Rectangle Outer, Rectangle Inner)> rects) {
			this.charset = charset;
			this.chars_rects = new ConcurrentDictionary<char, (Rectangle, Rectangle)>(rects);
		}
		public Rectangle TextSize(string str) {
			int width = 0;
			int height = 0;
			int right_margin = 0;
			foreach (char c in str) {
				var rects = this.chars_rects.GetValueOrDefault(c);
				int left_margin = rects.Inner.X - rects.Outer.X;
				width += Math.Max(left_margin, right_margin);
				width += rects.Inner.Width;
				right_margin = rects.Outer.X + rects.Outer.Width - rects.Inner.X - rects.Inner.Width;
				height = Math.Max(height, rects.Outer.Height);
			}
			width += right_margin;
			return new Rectangle(0, 0, width, height);
		}
		public System.Drawing.Bitmap RenderText(string str) {
			var rect = this.TextSize(str);
			var bmp = new System.Drawing.Bitmap(rect.Width, rect.Height);
			var g = Graphics.FromImage(bmp);
			g.CompositingMode = CompositingMode.SourceOver;
			int x = 0;
			int right_margin = 0;
			foreach (char c in str) {
				var rects = this.chars_rects.GetValueOrDefault(c);
				int left_margin = rects.Inner.X - rects.Outer.X;
				x += Math.Max(left_margin, right_margin);
				g.Compose(this.charset, rects.Outer, new Rectangle(x - left_margin, 0, rects.Outer.Width, rects.Outer.Height));
				x += rects.Inner.Width;
				right_margin = rects.Outer.X + rects.Outer.Width - rects.Inner.X - rects.Inner.Width;
			}
			return bmp;
		}
	}
}
