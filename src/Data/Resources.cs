using System;
using System.IO;
using System.Collections.Concurrent;
using System.Drawing;
using System.Threading;
using Discord.WebSocket;

namespace KatanaBot.Data {
	public class Resources {
		public static string RESOURCES_PATH = @"resources/";
		public static class Assets {
			// private static ConcurrentDictionary<string, Bitmap> Bitmaps = new ConcurrentDictionary<string, Bitmap>();
			private const string DEFAULT_FILEEXT = "png";
			private static string IMG_PATH = $@"{RESOURCES_PATH}img/";
			private static string UI_PATH = $@"{IMG_PATH}ui/";
			private static string UICHARACTER_PATH = $@"{UI_PATH}characters/";
			private static Bitmap Load(string id, string path) {
				Bitmap bmp;
				// if (Bitmaps.TryGetValue(id, out bmp)) {return Bitmaps[id];}
				bmp = new Bitmap(path);
				// Bitmaps[id] = bmp;
				return bmp;	
			}
			public static Bitmap UI(string resource, string extension=DEFAULT_FILEEXT) {
				string id = $"ui/{resource}";
				string path = $"{Assets.UI_PATH}{resource}.{extension}";
				return Load(id, path);
			}
			public static Bitmap UICharacter(string resource, string extension=DEFAULT_FILEEXT) {
				string id = $"ch/{resource}";
				string path = $"{Assets.UICHARACTER_PATH}{resource}.{extension}";
				return Load(id, path);
			}
		}
		
		public static string BIN_DIR = $@"{RESOURCES_PATH}binaries/";
		public static string TEXT_DIR = $@"{RESOURCES_PATH}text/";

		public static string CARDS_DIR = "resources/cards/";
		public static string WEAPONS_DIR = $"{CARDS_DIR}weapons/";
		public static string ROLES_DIR = $"{CARDS_DIR}genders/";
		public static string CHARACTERS_DIR = $"{CARDS_DIR}characters/";
		public static string BUFFS_DIR = $"{CARDS_DIR}buffs/";
		public static string SPELLS_DIR = $"{CARDS_DIR}spells/";

		public struct Text {
			public static string TOKEN_FILE = $@"{TEXT_DIR}token.txt";
		}
	}
}
