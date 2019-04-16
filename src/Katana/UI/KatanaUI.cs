using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using GameRendering.UI;
using KatanaBot.Data;

namespace KatanaGame {
	public class MockPlayer : KatanaPlayer {

	}
	public class KatanaUI {
		private Dictionary<string, BitmapFont> fonts = new Dictionary<string, BitmapFont>();
		public KatanaUI() {
			var font_chars = new Dictionary<char, (Rectangle, Rectangle)>() {
					{'0', (new Rectangle(  8, 0, 54, 89), new Rectangle( 10, 8, 50, 73))},
					{'1', (new Rectangle( 90, 0, 30, 89), new Rectangle( 92, 8, 26, 73))},
					{'2', (new Rectangle(147, 0, 56, 89), new Rectangle(149, 8, 52, 73))},
					{'3', (new Rectangle(219, 0, 52, 89), new Rectangle(221, 8, 48, 73))},
					{'4', (new Rectangle(286, 0, 58, 89), new Rectangle(288, 8, 54, 73))},
					{'5', (new Rectangle(357, 0, 56, 89), new Rectangle(359, 8, 52, 73))},
					{'6', (new Rectangle(431, 0, 48, 89), new Rectangle(433, 8, 44, 73))},
					{'7', (new Rectangle(498, 0, 54, 89), new Rectangle(500, 8, 50, 73))},
					{'8', (new Rectangle(571, 0, 48, 89), new Rectangle(573, 8, 44, 73))},
					{'9', (new Rectangle(641, 0, 48, 89), new Rectangle(643, 8, 44, 73))},
					{'+', (new Rectangle(706, 0, 39, 89), new Rectangle(708, 8, 35, 73))},
					{'-', (new Rectangle(757, 0, 38, 89), new Rectangle(759, 8, 34, 73))}
			};
			this.fonts["stats_base"] = new BitmapFont(Resources.Assets.UI("DigitsBlueInWhite"), font_chars);
			this.fonts["stats_bonus"] = new BitmapFont(Resources.Assets.UI("DigitsBlackInWhite"), font_chars);
			
			var profile_top_rectangle = new RelativeRectangle(
				offset: (0, 0, ContentAlignment.TopCenter),
				size: (176, 215, ContentAlignment.TopCenter)
			);
			
			var d = new DynamicDisposition<KatanaPlayer>(profile_top_rectangle,
				new string[] {"back_banner", "character", "shadow", "over", "loop"},
				new Dictionary<string, IElement>() {
					{"character", new DynamicBitmap<KatanaPlayer>(profile_top_rectangle, (player) => Resources.Assets.UI("characters/" + player.Character.Picture))},
					{"shadow", new FixedBitmap(profile_top_rectangle, Resources.Assets.UI("shadow"))},
					{"over", new FixedBitmap(profile_top_rectangle, Resources.Assets.UI("red_over"))},
					{"loop", new DynamicBitmap<KatanaPlayer>(profile_top_rectangle, (player) => Resources.Assets.UI("loop-" + LoopColor(player)))},
					{"back_banner", new FixedBitmap(
						new RelativeRectangle(
							offset: (0, 158, ContentAlignment.TopCenter),
							size: (144, 226, ContentAlignment.TopCenter)
						),
						Resources.Assets.UI("back")
					)}
				}
			);
			var p = new KatanaPlayer();
			Bitmap b = new Bitmap(176, 384);
			var g = Graphics.FromImage(b);
			d.Render(g, new Rectangle(0, 0, 176, 384), p);
			b.Save("RefactoredChiyome.png", System.Drawing.Imaging.ImageFormat.Png);
		}
		public string LoopColor(KatanaPlayer player) {
			switch (player.Role.Name) {
				case "Shogun" : return "gold";
				default: return "silver";
			}
		}
	}
}
