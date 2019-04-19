using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

using GameRendering.UI;
using KatanaBot.Data;
using Discord;
using Discord.WebSocket;
using Discord.Webhook;

namespace GameRendering {
	public class Engine {
		private System.Drawing.Bitmap BLANK_PROFILE = new System.Drawing.Bitmap(176, 384); /* %FIXME% */
		public Engine() {
			this.InitializeBlankProfile();
			this.RenderPlayer(this.InitializePlayer(null, 0));

			/* Toutes les variables ci-dessous sont là pour ne pas perdre les valeurs
				elles seront stokées dans d'autres classes
			 */
			var stats_base_font = new BitmapFont(
				Resources.Assets.UI("DigitsBlueInWhite"),
				new Dictionary<char, (Rectangle, Rectangle)>() {
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
			});
			
			var ProfileSize = new Size(width:176, height:384);
			var psize = new RelativeSize(64, 64);
			// var Points = new ATypeWeWillNameProperly(
			// 	types:new string[] {"honor", "resilience"},
			// 	rect:new RelativeRectangle(offset:(0, ProfileSize.Width - psize.Height/2), size:psize),
			// 	dynamic_offset:(i=>i * (ProfileSize.Width - psize.Width), i=>0),
			// 	valuerect:new RelativeRectangle(offset:(0, 0, ContentAlignment.MiddleCenter), size:(0, 56, ContentAlignment.MiddleCenter))
			// );
			// var Stats = new ATypeWeWillNameProperly(
			// 	types:new string[] {"armor", "weapons", "damage"},
			// 	rect:new RelativeRectangle(offset:(24, Points.Offset.Y + Points.Size.Height), size:(48, 48)),
			// 	dynamic_offset:(i=>0, i=>i*48),
			// 	valuerect:new RelativeRectangle(offset:(40, 12, ContentAlignment.TopLeft), size:(0, 36, ContentAlignment.TopCenter))
			// );
			/*
				On référence les séries d'icones par nom
				Et on demande à l'ui de render telle série avec telles valeurs
				Ou sans valeurs.
				Sans valeurs => fond uniquement
				Avec valeurs => valeurs uniquement
				Les StatsDeBase sont une série d'icône
					dans l'ordre "armor", "weapons", "damage"
					La série commence au point PointRelatif offset relatif au profil général
						-> l'alignement fait référence au rect global
					La série progresse selon un PointRelatif offset x:i=>x(i),y:i=>y(i)
						-> l'alignement du point n'affecte rien vu qu'il n'y a pas de rect de référence.
						-> on peut merge les offsets fixe et dynamique en 1 suel, dynamique (sera fait)
					Les icones sont tracées avec une taille S
						-> le rect est aligné par rapport au point obtenu
					Les valeurs sont décalées par rapport aux icones d'un offset fixe
						-> offset relatif au rect de l'icône

				A faire :
					Element composite d'UI
					On lui dit quoi placer où
					Quels layers combiner
					Et hop ça a la gueule d'un makefile

					En l'occurence notre ui c'est :
						BlankProfile (Modèle pour tout le monde)
							Ombre
							Bannière de fond
							Encart rouge
							Boucle (dorée, argentée...)
							Icones de points
							Icones de stats

						BaseProfile (Modèle pour un joueur)
							Image de personnage
							BlankProfile
							Stats de base
						
						RenderedProfile (Fiche remplie pour un joueur)
							BaseProfile
							Masque de mort
							Stats augmentées
							icone d'intouchable

			 */
		}
		private void InitializeBlankProfile() {
			var g = Graphics.FromImage(BLANK_PROFILE);
			g.CompositingMode = CompositingMode.SourceOver;

			g.Compose(Resources.Assets.UI("back"), 16, 158)
				.Compose(Resources.Assets.UI("shadow"), 0, 0)
				.Compose(Resources.Assets.UI("red_over"), 0, 0)
				.Compose(Resources.Assets.UI("loop-gold"), 0, 0)
				// .Compose(Resources.Assets.UI("mask"), 0, 0)
			;

			Func<string, string> val = (string type) => type;
			Action<(int X, int Y, string Value)> act = (t) => g.Compose(Resources.Assets.UI(t.Value), t.X, t.Y);
			// Iter(POINT_TYPES, val:val, act:act,
			// 	x:(i) => i * (PROFILE_WIDTH - POINTS_SIZE),
			// 	y:(i) => POINTS_Y
			// );
			// Iter(STAT_TYPES, val:val, act:act,
			// 	x:(i) => Stats.BASE_X,
			// 	y:(i) => Stats.BASE_Y + i * Stats.SIZE
			// );
			BLANK_PROFILE.Save("blank.png", System.Drawing.Imaging.ImageFormat.Png);
		}
		private System.Drawing.Bitmap InitializePlayer(object game, ulong id) {
			string lowercase_character = "chiyome"; /* %FIXME% dynamic argument */
			var base_stats = new Dictionary<string, int>() {{"armor", 27}, {"weapons", 0}, {"damage", 1}}; /* %FIXME% dynamic argument */

			var bmp = new System.Drawing.Bitmap(176, 384);
			var g = Graphics.FromImage(bmp);
			g.CompositingMode = CompositingMode.SourceOver;
			// g.Compose(Resources.Assets.UICharacter(lowercase_character), 0, 0);
			g.Compose(BLANK_PROFILE, 0, 0);
			System.Drawing.Bitmap digits = Resources.Assets.UI("DigitsBlueInWhite");

			// Iter(STAT_TYPES,
			// 	x:(i) => 64,
			// 	y:(i) => Stats.BASE_Y + i * Stats.SIZE + Stats.BASE_VALUE_YOFFSET,
			// 	val:(stat_type) => base_stats[stat_type],
			// 	act:(t) => g.DrawNumber(digits, t.Value, Stats.BASE_VALUE_HEIGHT, t.X, t.Y, x_align:0, dashzero:true)
			// );
			bmp.Save("chiyome_profile.png", System.Drawing.Imaging.ImageFormat.Png);
			return bmp;
		}
		public void RenderPlayer(System.Drawing.Bitmap base_bmp) {
			var points = new Dictionary<string, int>() {{"honor", 3}, {"resilience", 5}}; /* %FIXME% dynamic argument */
			var bonus_stats = new Dictionary<string, int>() {{"armor", 1}, {"weapons", 0}, {"damage", 18}}; /* %FIXME% dynamic argument */

			var bmp = new System.Drawing.Bitmap(base_bmp);
			var g = Graphics.FromImage(bmp);
			g.CompositingMode = CompositingMode.SourceOver;
			System.Drawing.Bitmap digits = Resources.Assets.UI("DigitsBlackInWhite");

			// Iter(POINT_TYPES,
			// 	x:(i) => POINTS_SIZE/2 + (PROFILE_WIDTH - POINTS_SIZE)*i,
			// 	y:(i) => POINTS_Y + POINTS_SIZE/2,
			// 	val:(point_type) => points[point_type],
			// 	act:(t) => g.DrawNumber(digits, t.Value, POINTS_VALUE_HEIGHT, t.X, t.Y, x_align:0, y_align:0)
			// );
			// Iter(STAT_TYPES,
			// 	x:(i) => 16 + ((PROFILE_WIDTH-32)*3)/4,
			// 	y:(i) => Stats.BASE_Y + i * Stats.SIZE + 4,
			// 	val:(stat_type) => bonus_stats[stat_type],
			// 	act:(t) => g.DrawNumber(digits, t.Value, Stats.BONUS_VALUE_HEIGHT, t.X, t.Y, x_align:0, plus:true, dashzero:true)
			// );

			bmp.Save("chiyome_state.png", System.Drawing.Imaging.ImageFormat.Png);
		}
		private void Iter<TIn, TOut>(TIn[] types, Func<int, int> x, Func<int, int> y, Func<TIn, TOut> val, Action<(int X, int Y, TOut Value)> act) {
			/* sera rempalcé par une méthode plus propre de render UI */
			int i = 0;
			foreach (var type in types) {
				act((x(i), y(i),val(type)));
				i++;
			}
		}




























		public static System.Drawing.Bitmap MakeNumber(System.Drawing.Bitmap digits_bmp, int number, bool plus=false, bool dashzero=false) {
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
				width += 54 - 2;//*DIGIT_MARGIN[digit];
			}
			digits.Reverse();

			if (plus && (!dashzero || (number > 0))) {width += 35;}
			else if (number == 0 && dashzero) {width = 34+2; digits.Clear();}

			System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(width, 89);
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
				int margin = 0;//DIGIT_MARGIN[digit];
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
