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
		public static class TextEmojis
		{
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
			public static Emoji GetEmoji(string emoji_name)
			{
				TryGetEmoji(emoji_name, out Emoji emoji);
				return emoji;
			}
			public static Emoji[] GetEmojis(params string[] emoji_names) {
				Emoji[] emoji_objects = new Emoji[emoji_names.Length];
				for (int i = 0 ; i < emoji_names.Length ; ++i) {
					TryGetEmoji(emoji_names[i], out emoji_objects[i]);
				}
				return emoji_objects;
			}
			public static bool TryGetEmoji(string emoji_name, out Emoji emoji)
			{
				if (emojis.TryGetValue(emoji_name, out string emoji_str))
				{
					emoji = new Emoji(emoji_str);
					return true;
				}
				emoji = null;
				return false;
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
			public static GuildEmote GetEmote(string emote_name)
			{
				GuildEmote emote = null;
				guilds_emotes.AsParallel().FirstOrDefault(guild_entry => guild_entry.Value.TryGetEmote(emote_name, out emote));
				return emote;
			}
		}
	}
}
