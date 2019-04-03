using Discord;
using Discord.WebSocket;
using KatanaBot.Data;
using System.Threading.Tasks;

namespace KatanaBot.Utilities
{
	public class EmoteManager
	{
		public static class TextEmoji
		{
			public const string Nsfw = @"🔞";
			public const string CheckMark = @"✅";
			public const string CrossMark = @"❎";
			public const string Skull = @"💀";
			public const string Peach = @"🍑";
			public const string Smirk = @"😏";
			public const string Flip = @"(╯°□°）╯︵ ┻━┻";
			public const string Unflip = @"┬─┬﻿ ノ( ゜-゜ノ)";
			public const string Lenny = @"( ͡° ͜ʖ ͡°)";
			public const string InvalidEmote = @"❌";
			public const string One = "\u0031\u20e3";
			public const string Two = "\u0032\u20e3";
			public const string Three = "\u0033\u20e3";
			public const string Four = "\u0034\u20e3";
			public const string Five = "\u0035\u20e3";
			public const string Six = "\u0036\u20e3";
			public const string Seven = "\u0037\u20e3";
			public const string Eight = "\u0038\u20e3";
			public const string Nine = "\u0039\u20e3";
		}

		public static IEmote Nsfw { get; } = new Emoji(TextEmoji.Nsfw);
		public static IEmote CheckMark { get; } = new Emoji(TextEmoji.CheckMark);
		public static IEmote CrossMark { get; } = new Emoji(TextEmoji.CrossMark);
		public static IEmote Skull { get; } = new Emoji(TextEmoji.Skull);
		public static IEmote Peach { get; } = new Emoji(TextEmoji.Peach);
		public static IEmote Smirk { get; } = new Emoji(TextEmoji.Smirk);
		public static IEmote InvalidEmote { get; } = new Emoji(TextEmoji.InvalidEmote);
		public static IEmote One { get; } = new Emoji(TextEmoji.One);
		public static IEmote Two { get; } = new Emoji(TextEmoji.Two);
		public static IEmote Three { get; } = new Emoji(TextEmoji.Three);
		public static IEmote Four { get; } = new Emoji(TextEmoji.Four);
		public static IEmote Five { get; } = new Emoji(TextEmoji.Five);
		public static IEmote Six { get; } = new Emoji(TextEmoji.Six);
		public static IEmote Seven { get; } = new Emoji(TextEmoji.Seven);
		public static IEmote Eight { get; } = new Emoji(TextEmoji.Eight);
		public static IEmote Nine { get; } = new Emoji(TextEmoji.Nine);

		public struct Guilds
		{
			public static class Zawarudo
			{
				private static readonly SocketGuild guild = DataManager.Client.GetGuild(309407896070782976);

				public static IEmote Pepe => GetEmote(329281047730585601, guild).Result ?? InvalidEmote;
				public static IEmote Aret => GetEmote(452977127722188811, guild).Result ?? InvalidEmote;
				public static IEmote Mickey => GetEmote(452977414440615976, guild).Result ?? InvalidEmote;
			}

			public static class Prod
			{
				private static readonly SocketGuild guild = DataManager.Client.GetGuild(456443419896709123);

				public static IEmote Ban => GetEmote(553719355322531851, guild).Result ?? InvalidEmote;
				public static IEmote Minus => GetEmote(553716950979575819, guild).Result ?? InvalidEmote;
				public static IEmote Plus => GetEmote(553716932826890250, guild).Result ?? InvalidEmote;
				public static IEmote Edit => GetEmote(553716553502163070, guild).Result ?? InvalidEmote;
			}

			public static class Tests
			{
				private static readonly SocketGuild guild = DataManager.Client.GetGuild(543925483008426016);

				public static IEmote Taric => GetEmote(553721390260289662, guild).Result ?? InvalidEmote;
			}
		}

		private static async Task<GuildEmote> GetEmote(ulong idEmoji, IGuild guild)
		{
			try {
				return await guild.GetEmoteAsync(idEmoji);
			}
			catch (System.Exception) {
				return null;
			}
		}
	}
}
