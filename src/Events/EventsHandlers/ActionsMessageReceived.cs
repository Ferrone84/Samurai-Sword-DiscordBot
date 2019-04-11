using System.Xml.Linq;

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

using CollectionExtensions;
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

				//"en admettant qu'on possède déjà la collection des users"
				List<IUser> users = new List<IUser>() {guild.GetUser(293780484822138881), guild.GetUser(150338863234154496), guild.GetUser(342330092032098304)};

				//setup du deck et des cartes
				// CardManager cardManager = new CardManager(users.Count);
				// List<Card> deck = cardManager.Deck.Shuffle().ToList();
				// List<Card> roles = cardManager.Roles.Shuffle().ToList();
				// List<Card> characters = cardManager.Characters.Shuffle().Take(users.Count).ToList();

				RestCategoryChannel channelsCategorie = await guild.CreateCategoryChannelAsync("Katana");
				RestTextChannel gameChannel = await guild.CreateTextChannelAsync("Jeu", x =>
				{
					x.CategoryId = channelsCategorie.Id;
					x.Topic = "Affiche le jeu avec les joueurs.";
				});

				foreach ((IUser user, int i) in users.Shuffle().Select((value, i) => (value, i))) {

					/*Card role = roles[i];
					Card character = characters[i];
					//alors ici j'hésite entre un Player qui serait de la forme => Player(IUser, role, character) : IPlayer
					//ou un Tuple<IUser, Player> avec un Player(role, character) : Iplayer
					//à toi de décider, en tout cas la classe player est obligée d'avoir "role" et "character" pour savoir les HP et l'honneur
					//il faudra que Player ait aussi la liste des cartes du joueur

					RestTextChannel channel = await guild.CreateTextChannelAsync($"cartes-{character.Name}", x => 
					{
						x.CategoryId = channelsCategorie.Id;
					});

					RestRole discordRole = await guild.CreateRoleAsync($"{character.Name}");
					OverwritePermissions userPermissions = new OverwritePermissions(viewChannel : PermValue.Allow);
					OverwritePermissions everyonePermissions = new OverwritePermissions(viewChannel : PermValue.Deny);

					await (user as SocketGuildUser).AddRoleAsync(discordRole);
					await channel.AddPermissionOverwriteAsync(guild.EveryoneRole, everyonePermissions);
					await channel.AddPermissionOverwriteAsync(discordRole, userPermissions);

					await channel.SendMessageAsync($"Bienvenue à toi {user.Mention} ! Dans cette partie tu aura le rôle de {role.Name}.");

					DataManager.ElementsToDelete.Add(channel);
					DataManager.ElementsToDelete.Add(discordRole);**/
				}
				DataManager.ElementsToDelete.Add(gameChannel);
				DataManager.ElementsToDelete.Add(channelsCategorie);

				//maintenant il reste à distribuer les cartes en clockwise à chacun en commençant par le shogun !
				//puis à envoyer la main de chaque joueurs dans leur channel respectif
				//(de ce fait, faut vérif que les paths dans CardManager sont bons (ce n'est pas le cas actuellement) pck sinon si on affiche les mains des joueurs => patatra)
				
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
			}
			else {await message.Channel.SendMessageAsync($"Ce n'est pas un nombre!");}
		}
	}
}
