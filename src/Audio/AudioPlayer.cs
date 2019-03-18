using Discord;
using Discord.Audio;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
/*
namespace KatanaBot.Audio {
	internal class Artist {
		internal readonly string Name;
		public Artist(string name) {
			this.Name = name;
		}
	}
	internal class Song {
		internal readonly Artist Author;
		internal readonly string Title;
		internal readonly string Filepath;
		internal Song(string title, Artist author, string filepath) {
			this.Title = title;
			this.Author = author;
			this.Filepath = filepath;
		}
	}
	internal class AudioPlayer {
		internal IVoiceChannel VoiceChannel = null;
		internal ITextChannel TextChannel = null;
		private TaskCompletionSource<bool> _tcs;
		private CancellationTokenSource _disposeToken;
		public IAudioClient AudioPipe;
		private const string ImABot = "";
		private string prefix = "::";
		private readonly string[] _commands = {
			"::help",
			"::queue",
			"::add",
			"::pause",
			"::resume",
			"::clear",
			"::skip"
		};
		private Queue<Song> _queue;
		private bool _internalPause;
		private bool Pause {
			get => _internalPause;
			set {
				new Thread(() => _tcs.TrySetResult(value)).Start();
				_internalPause = value;
			}
		}
		private bool _internalSkip;
		private bool Skip {
			get {
				bool ret = _internalSkip;
				_internalSkip = false;
				return ret;
			}
			set => _internalSkip = value;
		}
		public AudioPlayer() { Initialize(); }
		public async void Initialize() {
			_queue = new Queue<Song>();
			_tcs = new TaskCompletionSource<bool>();
			_disposeToken = new CancellationTokenSource();
			InitThread();
		}
		internal async Task JoinChannel(IVoiceChannel voice_channel, SocketTextChannel text_channel) {
			try {
				if (voice_channel != null) {
					this.VoiceChannel = voice_channel;
					this.AudioPipe = await voice_channel.ConnectAsync();
				}
				if (text_channel != null) {this.TextChannel = text_channel;}
			}
			catch (Exception e) {
				Program.Print("Could not join Voice/Text Channel (" + e.Message + ")", ConsoleColor.Red);
			}
		}

		internal async void MessageReceived(SocketMessage message) {
			try {
				string msg = message.Content;
				bool wanna_be_command = msg.StartsWith(this.prefix);
				bool known_command = _commands.Any(c => msg.StartsWith(c + " "));

				if (known_command) {}
				else if (wanna_be_command) {await message.DeleteAsync(); return;}
				else {return;}

				try { await message.DeleteAsync(); }
				catch {}

				if (msg.StartsWith(this.prefix + "help")) {
					await message.Channel.SendMessageAsync("Ce contenu a pour vocation d'être utile. Un jour.");
					return;
				} else if (msg.StartsWith(this.prefix + "queue")) {
					await SendQueue(TextChannel);
					return;
				}

				string[] split = msg.Split(' ');
				string command = split[0].Substring(this.prefix.Length);

				switch (command) {
					case "help":
						await SendMessage(
							$"Use these *Commands* by sending me a **private Message**, or writing in **#{message.Channel.Name}**!" + ImABot,
							embed: GetHelp(message.Author.ToString()));
						break;
					case "add":
						// Resolve song
						// Add it to queue
						break;

					case "pause":
						Pause = true;
						break;

					case "play":
						if (split.Length != 1) {
							// Resolve song
							// if currently playing, nothing
							// if in list, jump to
						}
						Pause = false;
						break;
					
					case "resume":
						Pause = false;
						break;

					case "clear":
						Pause = true;
						_queue.Clear();
						break;
					
					case "come":
						AudioPipe?.Dispose();
						await JoinChannel((message.Author as IGuildUser)?.VoiceChannel, null);
						break;
					
					case "skip":
						await SendMessage("Skipped song.");
						Skip = true;
						Pause = false;
						break;

					default:
						break;
				}
			}
			catch (Exception) {}
		}
		public void QueueSong(Song song, bool alone=false, bool play=true) {
			if (alone) {_queue.Clear();}
			_queue.Enqueue(song);
			if (play) {Pause = false;}
		}
		public async Task SendMessage(string message, IMessageChannel channel=null, Embed embed=null) {
			channel = channel ?? this.TextChannel;
			if (channel != null) { await TextChannel.SendMessageAsync(message, embed: embed); }
		}

		//Send Song queue in channel
		private async Task SendQueue(IMessageChannel channel) {
			// EmbedBuilder builder = new EmbedBuilder() {
			// 	Author = new EmbedAuthorBuilder { Name = "Music Bot Song Queue" },
			// 	Footer = new EmbedFooterBuilder() { Text = "(I don't actually sing)" },
			// 	Color = Pause ? new Color(244, 67, 54) /*Red : new Color(00, 99, 33) //Green
			// };
			//builder.ThumbnailUrl = "some cool url";

			if (_queue.Count == 0) { await SendMessage("Liste de lecture vide."); }
			else {
				string body = "Liste de lecture :";
				foreach (Song song in _queue) {
					body += $" - **{song.Title}** (de **{song.Author}**)";
				}
				await SendMessage(body, channel);
				//await channel.SendMessageAsync("", embed: builder.Build());
			}
		}
		//Return Bot Help
		public Embed GetHelp(string user) {
			EmbedBuilder builder = new EmbedBuilder() {
				Title = "-- Aide --",
				Description = "La gentille description",
				Color = new Color(102, 153, 255)
			};
			//builder.ThumbnailUrl = "https://raw.githubusercontent.com/mrousavy/DiscordMusicBot/master/DiscordMusicBot/disc.png"; //Music Bot Icon
			//builder.Url = "http://github.com/mrousavy/DiscordMusicBot";

			builder.AddField("`!help`", "Prints available Commands and usage");
			builder.AddField("`!queue`", "Prints all queued Songs & their User");

			builder.AddField("`!add [url]`", "Adds a single Song to Music-queue");
			builder.AddField("`!addPlaylist [url]`", "Adds whole playlist to Music-queue");
			builder.AddField("`!pause`", "Pause the queue and current Song");
			builder.AddField("`!play`", "Resume the queue and current Song");
			builder.AddField("`!clear`", "Clear queue and current Song");
			builder.AddField("`!come`", "Let Bot join your Channel");
			builder.AddField("`!update`", "Updates Permitted Clients from File");
			return builder.Build();
		}

		//Init Player Thread
		public void InitThread() { //TODO: Main Thread or New Thread?
			//MusicPlay();
			new Thread(MusicPlay).Start();
		}

		#region Audio
		//Audio: PCM | 48000hz | mp3

		//Get ffmpeg Audio Procecss
		private static Process GetFfmpeg(string path) {
			ProcessStartInfo ffmpeg = new ProcessStartInfo {
				FileName = "ffmpeg",
				Arguments = $"-xerror -i \"{path}\" -ac 2 -ar 48000 -f s16le pipe:1",
				//UseShellExecute = false,    //TODO: true or false?
				RedirectStandardOutput = true
			};
			return Process.Start(ffmpeg);
		}

		//Get ffplay Audio Procecss
		private static Process GetFfplay(string path) {
			ProcessStartInfo ffplay = new ProcessStartInfo {
				FileName = "ffplay",
				Arguments = $"-i \"{path}\" -ac 2 -f s16le -ar 48000 pipe:1 -autoexit",
				//UseShellExecute = false,    //TODO: true or false?
				RedirectStandardOutput = true
			};
			return new Process { StartInfo = ffplay };
		}

		//Send Audio with ffmpeg
		private async Task SendAudio(string path) {
			//FFmpeg.exe
			Process ffmpeg = GetFfmpeg(path);
			//Read FFmpeg output
			using (Stream output = ffmpeg.StandardOutput.BaseStream) {
				using (AudioOutStream discord = AudioPipe.CreatePCMStream(AudioApplication.Music, 128*1024)) {
					//Adjust?
					int bufferSize = 2048;
					int bytesSent = 0;
					bool fail = false;
					bool exit = false;
					byte[] buffer = new byte[bufferSize];

					while (
						!Skip &&                                    // If Skip is set to true, stop sending and set back to false (with getter)
						!fail &&                                    // After a failed attempt, stop sending
						!_disposeToken.IsCancellationRequested &&   // On Cancel/Dispose requested, stop sending
						!exit                                       // Audio Playback has ended (No more data from FFmpeg.exe)
							) {
						try {
							int read = await output.ReadAsync(buffer, 0, bufferSize, _disposeToken.Token);
							if (read == 0) { exit = true; break; } //No more data available, EOF
							await discord.WriteAsync(buffer, 0, read, _disposeToken.Token);
							if (Pause) {
								bool pauseAgain;
								do {
									pauseAgain = await _tcs.Task;
									_tcs = new TaskCompletionSource<bool>();
								} while (pauseAgain);
							}
							bytesSent += read;
						}
						catch (TaskCanceledException) { exit = true; }
						catch { fail = true; }
					}
					await discord.FlushAsync();
				}
			}
		}

		//Looped Music Play
		private async void MusicPlay() {
			bool next = false;
			while (true) {
				bool pause = false;
				//Next song if current is over
				if (!next) {
					pause = await _tcs.Task;
					_tcs = new TaskCompletionSource<bool>();
				}
				else { next = false; }

				try {
					if (_queue.Count == 0) {} // PLaylist End
					else {
						if (!pause) {
							Song song = _queue.Peek();
							await SendMessage($"Morceau actuel : **{song.Title}** ({song.Author})");
							await SendAudio(song.Filepath);
							_queue.Dequeue();
							next = true;
						}
					}
				}
				catch {} //audio can't be played
			}
		}

		#endregion
	}
}
*/