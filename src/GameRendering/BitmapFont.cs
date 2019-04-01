using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace KatanaBot.GameRendering {
	public class BitmapFont {
		private readonly Bitmap charset;
		private readonly ConcurrentDictionary<char, (Rectangle Outer, Rectangle Inner)> chars_rects;
		public BitmapFont(Bitmap charset, IDictionary<char, (Rectangle, Rectangle)> rects) {
			this.charset = charset;
			this.chars_rects = new ConcurrentDictionary<char, (Rectangle, Rectangle)>(rects);
		}
		public Rectangle TextSize(string str) {
			int width = 0;
			int height = 0;
			int right_margin = 0;
			foreach (char c in str) {
				var (inner, outer) = this.chars_rects.GetValueOrDefault(c);
				int left_margin = inner.X - outer.X;
				width += Math.Max(left_margin, right_margin);
				width += inner.Width;
				right_margin = outer.X + outer.Width - inner.X - inner.Width;
				height = Math.Max(height, outer.Height);
			}
			width += right_margin;
			return new Rectangle(0, 0, width, height);
		}
		public Bitmap MakeText(string str) {
			var rect = this.TextSize(str);
			var bmp = new Bitmap(rect.Width, rect.Height);
			var g = Graphics.FromImage(bmp);
			g.CompositingMode = CompositingMode.SourceOver;
			int x = 0;
			int right_margin = 0;
			foreach (char c in str) {
				var (inner, outer) = this.chars_rects.GetValueOrDefault(c);
				int left_margin = inner.X - outer.X;
				x += Math.Max(left_margin, right_margin);
				g.Compose(this.charset, outer, new Rectangle(x - left_margin, 0, outer.Width, outer.Height));
				x += inner.Width;
				right_margin = outer.X + outer.Width - inner.X - inner.Width;
			}
			return bmp;
		}
	}
}
