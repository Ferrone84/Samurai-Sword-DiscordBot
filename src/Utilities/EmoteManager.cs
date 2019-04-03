using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using KatanaBot.Data;

namespace KatanaBot.Utilities
{
	public class EmoteManager
	{
		public struct TextEmoji
		{
			public static string Nsfw = "🔞";
			public static string CheckMark = "✅";
			public static string CrossMark = "❎";
			public static string Skull = "💀";
			public static string Peach = "🍑";
			public static string Smirk = "😏";
			public static string Flip = "(╯°□°）╯︵ ┻━┻";
			public static string Unflip = "┬─┬﻿ ノ( ゜-゜ノ)";
			public static string Lenny = "( ͡° ͜ʖ ͡°)";
			public static string InvalidEmote = "❌";
			public static string RedCross = "❌";
			public static string One = "\u0031\u20e3";
			public static string Two = "\u0032\u20e3";
			public static string Three = "\u0033\u20e3";
			public static string Four = "\u0034\u20e3";
			public static string Five = "\u0035\u20e3";
			public static string Six = "\u0036\u20e3";
			public static string Seven = "\u0037\u20e3";
			public static string Eight = "\u0038\u20e3";
			public static string Nine = "\u0039\u20e3";
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








		public static class TextEmojis
		{
			public static string InvalidEmote = "❌";
			private static ConcurrentDictionary<string, string> emojis = new ConcurrentDictionary<string, string>(
				new Dictionary<string, string>() {
					{"Nsfw", "🔞"},
					{"CheckMark", "✅"},
					{"CrossMark", "❎"},
					{"Skull", "💀"},
					{"Peach", "🍑"},
					{"Smirk", "😏"},
					{"Flip", "(╯°□°）╯︵ ┻━┻"},
					{"Unflip", "┬─┬﻿ ノ( ゜-゜ノ)"},
					{"Lenny", "( ͡° ͜ʖ ͡°)"},
					{"RedCross", "❌"},
					{"0", "\u0030\u20e3"},
					{"1", "\u0031\u20e3"},
					{"2", "\u0032\u20e3"},
					{"3", "\u0033\u20e3"},
					{"4", "\u0034\u20e3"},
					{"5", "\u0035\u20e3"},
					{"6", "\u0036\u20e3"},
					{"7", "\u0037\u20e3"},
					{"8", "\u0038\u20e3"},
					{"9", "\u0039\u20e3"}
				}
			);

			public static string Which(Emoji emoji)
			{
				var emoji_str = emoji.ToString();
				var emoji_meta = emojis.AsParallel().FirstOrDefault(pair => (emoji_str == pair.Value));
				return emoji_meta.Key;
			}

			public static string WhichOf(Emoji emoji, params string[] names)
			{
				var name = Which(emoji);
				return (names.Contains(name) ? name : null);
			}
		}

		private static async Task<GuildEmote> GetEmote(ulong idEmoji, IGuild guild)
		{
			try {
				return await guild.GetEmoteAsync(idEmoji);
			}
			catch (Exception) {
				return null;
			}
		}

		private class GuildEmotes
		{
			private readonly ulong guild_id;
			private readonly ConcurrentDictionary<string, ulong> emotes;

			public GuildEmotes(ulong guild_id, IDictionary<string, ulong> emotes)
			{
				this.guild_id = guild_id;
				this.emotes = new ConcurrentDictionary<string, ulong>(emotes);
			}

			public GuildEmotes(ulong guild_id, params (string Name, ulong Id)[] emotes)
			{
				this.guild_id = guild_id;
				var dictionary = new Dictionary<string, ulong>();
				foreach (var (Name, Id) in emotes) {
					dictionary.Add(Name, Id);
				}
				this.emotes = new ConcurrentDictionary<string, ulong>(dictionary);
			}

			public string Which(GuildEmote emote)
			{
				var emote_meta = emotes.AsParallel().FirstOrDefault(pair => (emote.Id == pair.Value));
				return ((emote_meta.Value == emote.Id) ? emote_meta.Key : null);
			}

			public GuildEmote GetEmote(string name)
			{
				this.TryGetEmote(name, out GuildEmote emote);
				return emote;
			}

			public bool TryGetEmote(string name, out GuildEmote emote)
			{
				SocketGuild guild;
				try {
					if (this.emotes.TryGetValue(name, out ulong id)) {
						guild = DataManager.Client.GetGuild(this.guild_id);
						emote = guild.GetEmoteAsync(id).Result;
						return true;
					}
				}
				catch (Exception) { }
				emote = null;
				return false;
			}
		}

		public static class GuildsEmotes
		{
			private static ConcurrentDictionary<string, GuildEmotes> guilds_emotes = new ConcurrentDictionary<string, GuildEmotes>(
				new Dictionary<string, GuildEmotes>() {
					{"ZaWarudo", new GuildEmotes(309407896070782976,
						("Pepe", 329281047730585601),
						("Aret", 452977127722188811),
						("Mickey", 452977414440615976)
					)},
					{"Prod", new GuildEmotes(456443419896709123,
						("Ban", 553719355322531851),
						("Minus", 553716950979575819),
						("Plus", 553716932826890250),
						("Edit", 553716553502163070)
					)},
					{"Tests", new GuildEmotes(543925483008426016,
						("Taric", 553721390260289662)
					)}
				}
			);

			public static (string GuildName, string EmoteName) Which(GuildEmote emote)
			{
				string guild_name = null;
				string emote_name = null;
				guilds_emotes.AsParallel().FirstOrDefault(
					guild => {
						string local_name = guild.Value.Which(emote);
						if (local_name != null) {
							emote_name = local_name;
							guild_name = guild.Key;
							return true;
						}
						return false;
					}
				);
				return (guild_name, emote_name);
			}

			public static GuildEmote GetEmote(string guild_name, string emote_name)
			{
				if (guilds_emotes.TryGetValue(guild_name, out GuildEmotes guild) && guild.TryGetEmote(emote_name, out GuildEmote emote)) {
					return emote;
				}
				return null;
			}
		}
	}
}
