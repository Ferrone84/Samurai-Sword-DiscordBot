
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

using Events.EventsHandling;
using KatanaBot;
using KatanaBot.Data;
using KatanaBot.Utilities;

namespace Events.EventsHandlers
{
	public class ActionsMessageReceived : IGuildMessageReceivedEventHandler, IGuildMessageReactionAddedEventHandler
	{
		public async Task Guild_Message_Received(SocketUserMessage message)
		{
			if (message.Channel.Id != 559060763864596495) { return; } //tests-channel
			int pos = 0;
			if (!message.HasMentionPrefix(DataManager.Client.CurrentUser, ref pos)) {}
			try {
				//bon foudra ajouter ton système de commande pour rendre ça automatique
				//ce bloc sera remplacé par ton truc qui vérifie la mention
				bool noice = false;
				foreach (SocketUser user in message.MentionedUsers) {
					if (user.Id == 556976755294994487) {
						noice = true;
						break;
					}
				}

				//ce bloc sera remplacé par l'Action qui correspond
				if (noice) {
					Regex regex = new Regex(@"k+a+t+a+n+a+");
					if (regex.Match(message.Content.ToLower()).Success) {
						IUserMessage messageSent = await message.Channel.SendMessageAsync("Lancement du jeu Katana ! Sélectionne un nombre de joueurs.");
						await messageSent.AddReactionsAsync(EmoteManager.TextEmojis.GetEmojis("3","4","5","6","7"));
							// new IEmote[] { EmoteManager.Three, EmoteManager.Four, EmoteManager.Five, EmoteManager.Six, EmoteManager.Seven });
						//plus qu'à stocker le message où il faut pour save l'objet
						//et le user
						//pour l'instant je le fais en sale
					}
				}
			}
			catch (System.Exception e) {
				e.Display(MethodBase.GetCurrentMethod().ToString());
			}

			//ça c'est juste du caca pour tester les créations/destructions automatiques des channels/roles
			try {
				if (message.Content.ToLower() != "!c") { goto Bonsouar; }

				var guild = (message.Channel as SocketGuildChannel).Guild;

				RestCategoryChannel categorieChannel = await guild.CreateCategoryChannelAsync("Katana");
				RestTextChannel gameChannel = await guild.CreateTextChannelAsync("Game", x =>
				{
					x.CategoryId = categorieChannel.Id;
					x.Topic = "Affiche le jeu avec les joueurs.";
				});

				List<IUser> users = new List<IUser>() {message.Author, message.Author, message.Author}; //en admettant qu'on le sache déjà
				foreach ((IUser user, int i) in users.Select((value, i) => (value, i))) {
					RestTextChannel channel = await guild.CreateTextChannelAsync($"cards-player-{i+1}", x => 
					{
						x.CategoryId = categorieChannel.Id;
					});

					RestRole userRole = await guild.CreateRoleAsync($"Player#{user.Id}"); //on peut remplacer par le nom du perso
					OverwritePermissions userPermissions = new OverwritePermissions(viewChannel : PermValue.Allow);
					OverwritePermissions everyonePermissions = new OverwritePermissions(viewChannel : PermValue.Deny);

					await (user as SocketGuildUser).AddRoleAsync(userRole);
					await channel.AddPermissionOverwriteAsync(guild.EveryoneRole, everyonePermissions);
					await channel.AddPermissionOverwriteAsync(userRole, userPermissions);

					DataManager.ElementsToDelete.Add(channel);
					DataManager.ElementsToDelete.Add(userRole);
				}
				//jpense que faire une liste de IDeletable où on balance tout le caca (categories, channels, roles, etc) ce serait cool
				//comme ça pour delete à la fin de la partie, on aura juste un ForEach(elem => elem.DeleteAsync());
				DataManager.ElementsToDelete.Add(gameChannel);
				DataManager.ElementsToDelete.Add(categorieChannel);
			}
			catch (System.Exception e) {
				e.Display(MethodBase.GetCurrentMethod().ToString());
			}
		Bonsouar:
			try {
				if (message.Content.ToLower() != "!d") { return; }
				DataManager.ElementsToDelete.ForEach(elem => elem.DeleteAsync());
			}
			catch (System.Exception e) {
				e.Display(MethodBase.GetCurrentMethod().ToString());
			}
		}

		//faudra faire le même système que pour les messages, histoire d'avoir des trucs automatiques bien faits
		public async Task Guild_Message_ReactionAdded(Cacheable<IUserMessage, ulong> cachedMessage, ISocketMessageChannel channel, SocketReaction reaction) 
		{
			IUserMessage message = channel.GetMessageAsync(cachedMessage.Id).Result as IUserMessage;

			//donc ici bien faire gaffe à vérif que le message est un message stocké comme lanceur de partie et que c'est le même user
			int playerNumber = 0;
			/* Methode préférée */
			if (reaction.Emote is Emoji emoji) {
				string number = EmoteManager.TextEmojis.WhichOf(emoji, "0", "1", "2", "3", "4", "5", "6", "7", "8", "9");
				if (number != null) {
					int i = number[0] - '0';
					playerNumber = i;
					if (i > 7 || i < 3) {
						await message.Channel.SendMessageAsync($"Ce jeu accepte de 3 à 7 joueurs uniquement.");
					}
					else {
						await message.Channel.SendMessageAsync($"Lancement de la partie avec {playerNumber} joueurs.");
						await message.DeleteAsync();
					}
				}
				else {await message.Channel.SendMessageAsync($"Ce n'est pas un nombre!");}
				return;
			}
			else {await message.Channel.SendMessageAsync($"Ce n'est pas un nombre!");}
			return;
		}
	}
}
