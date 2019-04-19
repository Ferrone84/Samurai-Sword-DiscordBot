using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using GameRendering.UI;
using KatanaBot.Data;

using NumericExtensions;
using CollectionExtensions;

namespace KatanaGame {
	public class KatanaUI {
		private Dictionary<string, BitmapFont> fonts = new Dictionary<string, BitmapFont>();
		public KatanaUI() {
			var font_chars = new Dictionary<char, (Rectangle Outer, Rectangle Inner)>() {
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
			
			var d = new DynamicDisposition<KatanaPlayer>(
				origin:profile_top_rectangle,
				order:new string[] {"back_banner", "character", "shadow", "over", "loop", "points", "stats"},
				elements:new Dictionary<string, IElement>() {
					{"back_banner", new FixedBitmap(
						( Offset:(0, 158, ContentAlignment.TopCenter), Size:(144, 226, ContentAlignment.TopCenter) ),
						Resources.Assets.UI("back")
					)},
					{"character", new DynamicBitmap<KatanaPlayer>(profile_top_rectangle, (player) => Resources.Assets.UI("characters/" + player.Character.Picture))},
					{"mask", new DynamicBitmap<KatanaPlayer>(profile_top_rectangle, (player)=>(player.IsDead ? Resources.Assets.UI("mask") : null))},
					{"shadow", new FixedBitmap(profile_top_rectangle, Resources.Assets.UI("shadow"))},
					{"over", new FixedBitmap(profile_top_rectangle, Resources.Assets.UI("red_over"))},
					{"loop", new DynamicBitmap<KatanaPlayer>(profile_top_rectangle, (player) => Resources.Assets.UI("loop-" + LoopColor(player)))},

					{"points", PointsUI(this.fonts["stats_bonus"])},
					{"stats", StatsUI(this.fonts["stats_base"], this.fonts["stats_bonus"])}
				}
			);
			/*
				Organisation optimale de l'UI (réutilisation des resources):
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
						Valeurs de points
						Stats augmentées
						icone d'intouchable
				
				En l'occurence notre ui c'est : ( 1 => Modèle, 2 => nature du joueur, 3 => état du joueur )
					(1)Bannière de fond
					(2)Image de personnage
					(3)Masque de mort
					(3)icone d'intouchable
					(1)Ombre
					(1)Encart rouge
					(1)Boucle (dorée, argentée...)
					Points
						Honneur
							(1)Icone de points
							(3)Valeur de points
						Résilience
							(1)Icone de points
							(3)Valeur de points
					Stats
						Armures
							(1)Icones de stats
							(2)Stats de base
							(3)Stats augmentées
						Armes
							(1)Icones de stats
							(2)Stats de base
							(3)Stats augmentées
						Domages
							(1)Icones de stats
							(2)Stats de base
							(3)Stats augmentées
					

			*/
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
		private DynamicDisposition<KatanaPlayer> PointsUI(BitmapFont font) {
			var ordered_points = new string[] {"honor", "resilience"};
			var point_ui_ordered_elements = new string[] {"icon", "value"};
			return new DynamicDisposition<KatanaPlayer>(
				origin:(Offset:(0, 144, ContentAlignment.TopCenter), Size:(176, 64, ContentAlignment.TopCenter)),
				order:ordered_points,
				elements:new Dictionary<string, IElement>() {
					{"honor", PointUI(
						"honor", point_ui_ordered_elements,
						(Offset:( 32, 0, ContentAlignment.MiddleLeft), Size:(64, 64, ContentAlignment.MiddleCenter)),
						(player)=>player.Honor.Format(dashzero:false, enforce_plus:false),
						font
					)},
					{"resilience", PointUI(
						"resilience", point_ui_ordered_elements,
						(Offset:( -32, 0, ContentAlignment.MiddleRight), Size:(64, 64, ContentAlignment.MiddleCenter)),
						(player)=>player.Resilience.Format(dashzero:false, enforce_plus:false),
						font
					)},
				}
			);
		}
		private DynamicDisposition<KatanaPlayer> PointUI(
			string stat_icon,
			string[] ordered_elements,
			RelativeRectangle origin,
			Func<KatanaPlayer, string> text_getter,
			BitmapFont font) {
			return new DynamicDisposition<KatanaPlayer>(
				origin:origin,
				order:ordered_elements,
				elements:new Dictionary<string, IElement>() {
					{"icon", new FixedBitmap(
						origin:(Offset:(0, 0, ContentAlignment.MiddleCenter), Size:(64, 64, ContentAlignment.MiddleCenter)),
						Resources.Assets.UI(stat_icon)
					)},
					{"value", new DynamicText<KatanaPlayer>(
						origin:(Offset:(0, 0, ContentAlignment.MiddleCenter), Size:(0, 56, ContentAlignment.MiddleCenter)),
						text_getter:text_getter,
						font:font
					)}
				}
			);
		}
		private DynamicSeries<KatanaPlayer, KatanaPlayer.StatsValues, KatanaPlayer.StatValues> StatsUI(BitmapFont base_font, BitmapFont bonus_font) {
			var ordered_stats = new string[] {"armor", "weapons", "damage"};
			var stat_ui_ordered_elements = new string[] {"icon", "base", "bonus"};
			RelativeRectangle icon_origin = ((0, 0, ContentAlignment.MiddleLeft), (0, 0, ContentAlignment.MiddleCenter)); /* %TODO% */
			DynamicText<KatanaPlayer.StatValues> stat_base = new DynamicText<KatanaPlayer.StatValues>(
				origin:((16, 12, ContentAlignment.MiddleLeft), (0,36, ContentAlignment.MiddleCenter)), /* %TODO% */
				text_getter:(stat) => stat.Base.Format(dashzero:true, enforce_plus:false),
				font:base_font
			);
			DynamicText<KatanaPlayer.StatValues> stat_bonus = new DynamicText<KatanaPlayer.StatValues>(
				origin:((0, 0, ContentAlignment.MiddleRight), (0, 40, ContentAlignment.MiddleCenter)), /* %TODO% */
				text_getter:(stat) => stat.Bonus.Format(dashzero:true, enforce_plus:true),
				font:bonus_font
			);
			return new DynamicSeries<KatanaPlayer, KatanaPlayer.StatsValues, KatanaPlayer.StatValues>(
				origin: ((0, 232, ContentAlignment.TopCenter), (0, 0, ContentAlignment.TopCenter)), /* %TODO% */
				converter:(player) => player.Stats,
				selector:(stats, index, name) => {
					switch (name) {
						case "armor": { return stats.Armor;}
						case "weapons": { return stats.Weapons;}
						case "damage": { return stats.Damage;}
						default: return default(KatanaPlayer.StatValues);
					}
				},
				order:ordered_stats,
				elements:new Dictionary<string, IElement>(
					ordered_stats.Map((stat_name) => KeyValuePair.Create<string, IElement>(
						stat_name,
						StatUI(stat_name, stat_ui_ordered_elements, icon_origin, stat_base, stat_bonus)
					))
				),
				dynamic_offset:((i)=>0, (i)=>i*48)
			);
		}
		private DynamicDisposition<KatanaPlayer.StatValues> StatUI(
				string stat_icon,
				string[] ordered_elements,
				RelativeRectangle icon_origin,
				DynamicText<KatanaPlayer.StatValues> stat_base,
				DynamicText<KatanaPlayer.StatValues> stat_bonus) {
			return new DynamicDisposition<KatanaPlayer.StatValues>(
				origin: ((0, 0, ContentAlignment.TopCenter), (80, 0, ContentAlignment.TopCenter)),
				order:ordered_elements,
				elements: new Dictionary<string, IElement>() {
					{"icon", new FixedBitmap( origin:icon_origin, Resources.Assets.UI(stat_icon) )},
					{"base", stat_base},
					{"bonus", stat_bonus}
				}
			);
		}
	}
}
