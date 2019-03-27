using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using KatanaBot.Data;

namespace KatanaBot {
	public static class DrawingExtensions {
		/* Fucking Microsoft figured out how to fuck it even more *STARTING TO HATE CSHARP* */
		public static Graphics Compose(this Graphics graphics, Bitmap bmp, int x, int y) {
			graphics.DrawImage(bmp, x, y, bmp.Width, bmp.Height);
			return graphics;
		}
		public static Graphics Compose(this Graphics graphics, Bitmap bmp, Rectangle source_rect, Rectangle target_rect) {
			graphics.DrawImage(bmp, target_rect, source_rect, GraphicsUnit.Pixel);
			return graphics;
		}
		public static Graphics Compose(this Graphics graphics, Bitmap bmp, RectangleF source_rect, RectangleF target_rect) {
			graphics.DrawImage(bmp, target_rect, source_rect, GraphicsUnit.Pixel);
			return graphics;
		}
		public static void DrawNumber(this Graphics g, Bitmap digits_bmp, int number, int height, int x, int y, int x_align=1, int y_align=1, bool plus=false, bool dashzero=false) {
			var number_bmp = GameRender.MakeNumber(digits_bmp, number, plus:plus, dashzero:dashzero);
			var src = new RectangleF(0, 0, number_bmp.Width, number_bmp.Height);
			float fx = height / 89.0F;
			float f_x = x, f_w = number_bmp.Width * fx;
			float f_y = y, f_h = number_bmp.Height * fx;
			
			if (x_align < 0) {f_x -= f_w;}
			else if (x_align == 0) {f_x -= f_w * 0.5F;}
			if (y_align < 0) {f_y -= f_h;}
			else if (y_align == 0) {f_y -= f_h * 0.5F;}
			var trg = new RectangleF(f_x, f_y, f_w, f_h);
			g.Compose(number_bmp, src, trg);
		}
	}
	public class GameRender {
		private struct FontShape {
			public const int DigitOuterWidth = 70;
			public const int DigitInnerWidth = 54;
			public const int Margin = 2;
		}
		private static int[] DIGIT_MARGIN = new int[] { /* %FIXME% */
				/* 0 */  2,
				/* 1 */ 14,
				/* 2 */  1,
				/* 3 */  3,
				/* 4 */  0,
				/* 5 */  1,
				/* 6 */  5,
				/* 7 */  2,
				/* 8 */  5,
				/* 9 */  5
		};
		private const int PROFILE_WIDTH = 176;
		private const int PROFILE_HEIGHT = 384;
		private static string[] POINT_TYPES = new string[] {"honor", "resilience"};
		private const int POINTS_SIZE = 64;
		private const int POINTS_Y = PROFILE_WIDTH - POINTS_SIZE/2;
		private const int POINTS_VALUE_HEIGHT = 56;
		private static string[] STAT_TYPES = new string[] {"armor", "weapons", "damage"};
		private struct Stats {
			public const int SIZE = 48;
			public const int BASE_X = 24, BASE_Y = POINTS_Y + POINTS_SIZE;
			public const int BASE_VALUE_YOFFSET = 12;
			public const int BASE_VALUE_HEIGHT = 36, BONUS_VALUE_HEIGHT = 40;
		}
		private static Bitmap BLANK_PROFILE = new Bitmap(PROFILE_WIDTH, PROFILE_HEIGHT);
		public GameRender() {
			this.InitializeBlankProfile();
			this.RenderPlayer(this.InitializePlayer(null, 0));
		}
		private void InitializeBlankProfile() {
			var g = Graphics.FromImage(BLANK_PROFILE);
			g.CompositingMode = CompositingMode.SourceOver;

			g.Compose(Resources.Assets.UI("back"), 16, 158)
				.Compose(Resources.Assets.UI("profile_with_shadow"), 0, 0);

			Func<string, string> val = (string type) => type;
			Action<(int X, int Y, string Value)> act = (t) => g.Compose(Resources.Assets.UI(t.Value), t.X, t.Y);
			Iter(POINT_TYPES, val:val, act:act,
				x:(i) => i * (PROFILE_WIDTH - POINTS_SIZE),
				y:(i) => POINTS_Y
			);
			Iter(STAT_TYPES, val:val, act:act,
				x:(i) => Stats.BASE_X,
				y:(i) => Stats.BASE_Y + i * Stats.SIZE
			);
			BLANK_PROFILE.Save("blank.png", ImageFormat.Png);
		}
		private Bitmap InitializePlayer(object game, ulong id) {
			string lowercase_character = "chiyome"; /* %FIXME% dynamic argument */
			var base_stats = new Dictionary<string, int>() {{"armor", 27}, {"weapons", 0}, {"damage", 1}}; /* %FIXME% dynamic argument */

			var bmp = new Bitmap(PROFILE_WIDTH, PROFILE_HEIGHT);
			var g = Graphics.FromImage(bmp);
			g.CompositingMode = CompositingMode.SourceOver;
			g.Compose(Resources.Assets.UICharacter(lowercase_character), 0, 0);
			g.Compose(BLANK_PROFILE, 0, 0);
			Bitmap digits = Resources.Assets.UI("DigitsBlueInWhite");

			Iter(STAT_TYPES,
				x:(i) => 64,
				y:(i) => Stats.BASE_Y + i * Stats.SIZE + Stats.BASE_VALUE_YOFFSET,
				val:(stat_type) => base_stats[stat_type],
				act:(t) => g.DrawNumber(digits, t.Value, Stats.BASE_VALUE_HEIGHT, t.X, t.Y, x_align:0, dashzero:true)
			);
			bmp.Save("chiyome_profile.png", ImageFormat.Png);
			return bmp;
		}
		public void RenderPlayer(Bitmap base_bmp) {
			var points = new Dictionary<string, int>() {{"honor", 3}, {"resilience", 5}}; /* %FIXME% dynamic argument */
			var bonus_stats = new Dictionary<string, int>() {{"armor", 1}, {"weapons", 0}, {"damage", 18}}; /* %FIXME% dynamic argument */

			var bmp = new Bitmap(base_bmp);
			var g = Graphics.FromImage(bmp);
			g.CompositingMode = CompositingMode.SourceOver;
			Bitmap digits = Resources.Assets.UI("DigitsBlackInWhite");

			Iter(POINT_TYPES,
				x:(i) => POINTS_SIZE/2 + (PROFILE_WIDTH - POINTS_SIZE)*i,
				y:(i) => POINTS_Y + POINTS_SIZE/2,
				val:(point_type) => points[point_type],
				act:(t) => g.DrawNumber(digits, t.Value, POINTS_VALUE_HEIGHT, t.X, t.Y, x_align:0, y_align:0)
			);
			Iter(STAT_TYPES,
				x:(i) => 16 + ((PROFILE_WIDTH-32)*3)/4,
				y:(i) => Stats.BASE_Y + i * Stats.SIZE + 4,
				val:(stat_type) => bonus_stats[stat_type],
				act:(t) => g.DrawNumber(digits, t.Value, Stats.BONUS_VALUE_HEIGHT, t.X, t.Y, x_align:0, plus:true, dashzero:true)
			);

			bmp.Save("chiyome_state.png", ImageFormat.Png);
		}
		private void Iter<TIn, TOut>(TIn[] types, Func<int, int> x, Func<int, int> y, Func<TIn, TOut> val, Action<(int X, int Y, TOut Value)> act) {
			int i = 0;
			foreach (var type in types) {
				act((x(i), y(i),val(type)));
				i++;
			}
		}





























		public static Bitmap MakeNumber(Bitmap digits_bmp, int number, bool plus=false, bool dashzero=false) {
			/* Full of font-specific values %FIXME% MOCHE MOCHE MOCHE (mais ça marche)*/
			/*
				89 -> Fullframe height
				70/54 -> Digit Outer/Inner frame width
				51/35 -> 'Plus' Outer/Inner frame width
				50/34 -> 'Minus/Dash' Outer/Inner frame width
				8-2 -> Frame margin - blur margin
			 */
			int remaining = number;
			var digits = new List<int>();
			int width = 4;
			while ((remaining != 0) || (digits.Count == 0)) {
				int digit = remaining % 10;
				digits.Add(digit);
				remaining -= digit;
				remaining /= 10;
				width += 54 - 2*DIGIT_MARGIN[digit];
			}
			digits.Reverse();

			if (plus && (!dashzero || (number > 0))) {width += 35;}
			else if (number == 0 && dashzero) {width = 34+2; digits.Clear();}

			var bmp = new Bitmap(width, 89);
			var g = Graphics.FromImage(bmp);
			g.CompositingMode = CompositingMode.SourceOver;

			int x = 2;
			if (plus && (!dashzero || (number > 0))) {
				g.Compose(digits_bmp, new Rectangle(70*10+8-2, 0, 35+4, 89), new Rectangle(x-2, 0, 35+4, 89));
				x += 35;
			}
			else if ((number == 0) && dashzero) {
				g.Compose(digits_bmp, new Rectangle(70*10+51+8-2, 0, 34+4, 89), new Rectangle(x-2, 0, 34+4, 89));
				x += 34;
			}
			foreach (var digit in digits) {
				int margin = DIGIT_MARGIN[digit];
				int digit_width = 54 - 2*margin;
				var src = new Rectangle(70*digit+8+margin-2, 0, digit_width+4, 89);
				var trg = new Rectangle(x-2, 0, digit_width+4, 89);
				g.Compose(digits_bmp, src, trg);
				x += digit_width;
			}
			return bmp;
		}
	}
}
