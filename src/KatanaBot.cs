using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

using Events.EventsHandling;
using KatanaBot.Data;

using Cards;

namespace KatanaBot
{
	public class KatanaBot
	{
		private EventHandlersManager eventHandlersManager;

		public async Task MainAsync()
		{
			DiscordSocketConfig discordSocketConfig = new DiscordSocketConfig {
				MessageCacheSize = 100
			};

			DataManager.Client = new DiscordSocketClient(discordSocketConfig);
			eventHandlersManager = new EventHandlersManager(DataManager.Client);
			DataManager.Client.Log += Log;
			try {
				eventHandlersManager.AddHandlers(GetAllEventsHandlers("Events.EventsHandlers").ToArray());
			}
			catch (Exception e) { e.Display("MainAsync() => EventHandlersManager.AddHandlers"); }
			
			DataManager.LicenceToLive = new CancellationTokenSource();
			await DataManager.Client.LoginAsync(TokenType.Bot, Utils.Token);
			await DataManager.Client.StartAsync();

			Console.CancelKeyPress += async delegate (object sender, ConsoleCancelEventArgs e) {
				e.Cancel = true;
				await Deconnection();
			};

			// Block this task until the program is closed.
			try {
				await Task.Delay(-1, DataManager.LicenceToLive.Token);
			}
			catch (TaskCanceledException) {
				await Deconnection();
			}
		}

		private async Task Deconnection()
		{
			try {
				Console.WriteLine("Le bot a bien été coupé.");
				eventHandlersManager.Unbind(DataManager.Client);
				DataManager.Client.Log -= Log;
				await DataManager.Client.LogoutAsync();
				await DataManager.Client.StopAsync();
				DataManager.Client.Dispose();
				Environment.Exit(0);
			}
			catch (Exception e) {
				e.Display(MethodBase.GetCurrentMethod().ToString());
			}
		}

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());

			return Task.CompletedTask;
		}

		private List<IEventHandler> GetAllEventsHandlers(string nameSpace)
		{
			List<IEventHandler> eventsHandlers = new List<IEventHandler>();

			try {
				var types = Assembly.GetExecutingAssembly().GetTypes();
				var filtered_types = types.Where(
					t => ((t.Namespace != null) && t.Namespace.StartsWith(nameSpace))
				);
				foreach (var t in filtered_types) {
					if (!t.Name.Contains("d_")) {
						eventsHandlers.Add((t.GetConstructor(Type.EmptyTypes).Invoke(Type.EmptyTypes) as IEventHandler));
					}
				}
			}
			catch (Exception e) {
				e.Display(MethodBase.GetCurrentMethod().ToString());
			}
			return eventsHandlers;
		}

		private void TestsCards()
		{
			Weapon bokken = new Weapon("bokken", $"{DataManager.WEAPONS_DIR}bokken.jpg", 1, 1);
			Weapon bo = new Weapon("bo", $"{DataManager.WEAPONS_DIR}bo.jpg", 2, 1);
			Weapon daikyu = new Weapon("daikyu", $"{DataManager.WEAPONS_DIR}daikyu.jpg", 5, 2);
			Weapon kanabo = new Weapon("kanabo", $"{DataManager.WEAPONS_DIR}kanabo.jpg", 3, 2);
			Weapon katana = new Weapon("katana", $"{DataManager.WEAPONS_DIR}katana.jpg", 2, 3);
			Weapon kiseru = new Weapon("kiseru", $"{DataManager.WEAPONS_DIR}kiseru.jpg", 1, 2);
			Weapon kusarigama = new Weapon("kusarigama", $"{DataManager.WEAPONS_DIR}kusarigama.jpg", 2, 2);
			Weapon nagayari = new Weapon("nagayari", $"{DataManager.WEAPONS_DIR}nagayari.jpg", 4, 2);
			Weapon naginata = new Weapon("naginata", $"{DataManager.WEAPONS_DIR}naginata.jpg", 4, 1);
			Weapon nodachi = new Weapon("nodachi", $"{DataManager.WEAPONS_DIR}nodachi.jpg", 3, 3);
			Weapon shuriken = new Weapon("shuriken", $"{DataManager.WEAPONS_DIR}shuriken.jpg", 3, 1);
			Weapon tanegashima = new Weapon("tanegashima", $"{DataManager.WEAPONS_DIR}tanegashima.jpg", 5, 1);
			Weapon wakizashi = new Weapon("wakizashi", $"{DataManager.WEAPONS_DIR}wakizashi.jpg", 1, 3);

			Role ninja = new Role("ninja", $"{DataManager.ROLES_DIR}ninja.jpg", "En équipe avec les autres ninjas.", 4, Role.MultiplierType.two);
			Role ronin = new Role("ronin", $"{DataManager.ROLES_DIR}ronin.jpg", "Tu es seul contre tous.", 4, Role.MultiplierType.three);
			Role samurai = new Role("samurai", $"{DataManager.ROLES_DIR}samurai.jpg", "En équipe avec les autres samurais et le shogun.", 4, Role.MultiplierType.one);
			Role shogun = new Role("shogun", $"{DataManager.ROLES_DIR}shogun.jpg", "En équipe avec les samurais.", 5, Role.MultiplierType.one);

			Character benkei = new Character("benkei", $"{DataManager.CHARACTERS_DIR}benkei.jpg", "Les autres joueurs vous attaquent avec une difficulté augmentée de 1.", 5);
			Character chiyome = new Character("chiyome", $"{DataManager.CHARACTERS_DIR}chiyome.jpg", "Seules les armes peuvent vous faire perdre des points de vie.", 4);
			Character ginchiyo = new Character("ginchiyo", $"{DataManager.CHARACTERS_DIR}ginchiyo.jpg", "Vous réduisez de 1 les dégâts des armes (minimum 1).", 4);
			Character goemon = new Character("goemon", $"{DataManager.CHARACTERS_DIR}goemon.jpg", "Vous pouvez jouer 1 arme supplémentaire par tour.", 5);
			Character hanzo = new Character("hanzo", $"{DataManager.CHARACTERS_DIR}hanzo.jpg", "Vous pouvez jouer vos cartes d'arme comme des parades (à moins que ça ne soit votre dernière carte en main).", 4);
			Character hideyoshi = new Character("hideyoshi", $"{DataManager.CHARACTERS_DIR}hideyoshi.jpg", "Durant votre phase de pioche, piochez 1 carte supplémentaire.", 4);
			Character ieyasu = new Character("ieyasu", $"{DataManager.CHARACTERS_DIR}ieyasu.jpg", "Durant votre phase de pioche, vous pouvez prendre la carte du dessus de la défausse au lieu de piocher votre première carte. Vous devez piocher normalement la deuxième.", 5);
			Character kojiro = new Character("kojiro", $"{DataManager.CHARACTERS_DIR}kojiro.jpg", "Vos armes peuvent attaquer n'importe quel autre joueur sans tenir compte de la difficulté.", 5);
			Character musashi = new Character("musashi", $"{DataManager.CHARACTERS_DIR}musashi.jpg", "Vos armes causent 1 dégat supplémentaire.", 5);
			Character nobunaga = new Character("nobunaga", $"{DataManager.CHARACTERS_DIR}nobunaga.jpg", "Durant votre tour, vous pouvez perdre 1 point de vie (sauf votre dernier) pour piocher 1 carte.", 5);
			Character tomoe = new Character("tomoe", $"{DataManager.CHARACTERS_DIR}tomoe.jpg", "Chaque fois qu'une de vos armes blesse un joueur, piochez 1 carte.", 5);
			Character ushiwaka = new Character("ushiwaka", $"{DataManager.CHARACTERS_DIR}ushiwaka.jpg", "Chaque fois que vous perdez 1 point de vie à cause d'une arme, piochez 1 carte.", 4);
			Character rikyu = new Character("rikyu", $"{DataManager.CHARACTERS_DIR}rikyu.jpg", "Lorsque la pioche est épuisée, vous ne perdez pas de point d'honneur.", 4);

			Buff armure = new Buff("armure", $"{DataManager.BUFFS_DIR}armure.jpg", "Les autres joueurs vous attaquent avec une difficulté augmentée de 1.", Buff.Type.Armor);
			Buff attaque_rapide = new Buff("attaque rapide", $"{DataManager.BUFFS_DIR}attaque_rapide.jpg", "Vos armes causent 1 dégâts supplémentaire.", Buff.Type.Damage);
			Buff concentration = new Buff("concentration", $"{DataManager.BUFFS_DIR}concentration.jpg", "Vous pouvez jouer 1 arme de plus par tour.", Buff.Type.WeaponNumber);

			Spell ceremonie_du_the = new Spell("Cérémonie du thé", $"{DataManager.SPELLS_DIR}ceremonie_du_the.jpg", "Piochez 3 cartes. Les autres joueurs piochent 1 carte.");
			Spell code_du_bushido = new Spell("code_du_bushido", $"{DataManager.SPELLS_DIR}code_du_bushido.jpg", "Si la carte est devant vous, piochez une carte et défaussez-la. Si c'est une arme vous devez défausser une arme sinon vous perdais 1 point d'honneur. Si ce n'est pas une arme, passez la carte à votre voisin de gauche.");
			Spell cri_de_guerre = new Spell("cri_de_guerre", $"{DataManager.SPELLS_DIR}cri_de_guerre.jpg", "Les joueurs doivent défausser 1 parade ou perdre 1 point de vie.");
			Spell daimyo = new Spell("daimyo", $"{DataManager.SPELLS_DIR}daimyo.jpg", "Piochez 2 cartes. Si vous possédez cette carte à la fin de la partie, elle vous rapporte 1 point d'honneur.");
			Spell diversion = new Spell("diversion", $"{DataManager.SPELLS_DIR}diversion.jpg", "Piochez au hasard 1 carte dans la main d'un joueur de votre choix et ajoutez-la à votre main.");
			Spell geisha = new Spell("geisha", $"{DataManager.SPELLS_DIR}geisha.jpg", "Défaussez 1 carte permanente en jeu ou 1 carte au hasard de la main d'un autre joueur.");
			Spell jujitsu = new Spell("jujitsu", $"{DataManager.SPELLS_DIR}jujitsu.jpg", "Les autres joueurs doivent défausser 1 arme ou perdre 1 point de vie.");
			Spell meditation = new Spell("méditation", $"{DataManager.SPELLS_DIR}meditation.jpg", "Récupérez tous vos points de vie. Un autre joueur de votre choix pioche 1 carte.");
			Spell parade = new Spell("parade", $"{DataManager.SPELLS_DIR}parade.jpg", "Bloque une attaque causée par une arme.");


			List<Card> allCards = new List<Card>() {
				bokken, bo, daikyu, kanabo, katana, kiseru, kusarigama, nagayari, naginata, nodachi, shuriken, tanegashima, wakizashi,
				ninja, ronin, samurai, shogun,
				benkei, chiyome, ginchiyo, goemon, hanzo, hideyoshi, ieyasu, kojiro, musashi, nobunaga, tomoe, ushiwaka, rikyu,
				armure, attaque_rapide, concentration,
				ceremonie_du_the, code_du_bushido, cri_de_guerre, daimyo, diversion, geisha, jujitsu, meditation, parade
			};

			List<Card> deck = new List<Card>() {
				//Actions
				parade, parade, parade, parade, parade, parade, parade, parade, parade, parade, parade, parade, parade, parade, parade,
				geisha, geisha, geisha, geisha, geisha, geisha,
				diversion, diversion, diversion, diversion, diversion,
				ceremonie_du_the, ceremonie_du_the, ceremonie_du_the, ceremonie_du_the,
				cri_de_guerre, cri_de_guerre, cri_de_guerre, cri_de_guerre,
				jujitsu, jujitsu, jujitsu,
				meditation, meditation, meditation,
				daimyo, daimyo, daimyo, 

				//Propriétés
				concentration, concentration, concentration, concentration, concentration, concentration,
				armure, armure, armure, armure,
				attaque_rapide, attaque_rapide, attaque_rapide,
				code_du_bushido, code_du_bushido, 

				//Armes
				bokken, bokken, bokken, bokken, bokken, bokken,
				bo, bo, bo, bo, bo,
				kiseru, kiseru, kiseru, kiseru, kiseru,
				kusarigama, kusarigama, kusarigama, kusarigama,
				shuriken, shuriken, shuriken,
				naginata,naginata,
				daikyu,
				kanabo,
				katana,
				nagayari,
				nodachi,
				tanegashima,
				wakizashi
			};

			("\nNombre de cartes dans le deck : " + deck.Count).Println();
		}
	}
}
