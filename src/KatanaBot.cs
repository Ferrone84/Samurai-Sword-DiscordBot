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

			DataManager._client = new DiscordSocketClient(discordSocketConfig);
			eventHandlersManager = new EventHandlersManager(DataManager._client);
			DataManager._client.Log += Log;
			try {
				eventHandlersManager.AddHandlers(GetAllEventsHandlers("KatanaBot.Events.EventsHandlers").ToArray());
			}
			catch (Exception e) { e.Display("MainAsync() => EventHandlersManager.AddHandlers"); }

			TestsCards();

			DataManager.LicenceToLive = new CancellationTokenSource();
			await DataManager._client.LoginAsync(TokenType.Bot, Utils.Token);
			await DataManager._client.StartAsync();

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
				eventHandlersManager.Unbind(DataManager._client);
				DataManager._client.Log -= Log;
				await DataManager._client.LogoutAsync();
				await DataManager._client.StopAsync();
				DataManager._client.Dispose();
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
				var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace.StartsWith(nameSpace));
				foreach (var t in types) {
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
			string cardsDir = "resources/cards/";
			string weaponsDir = $"{cardsDir}weapons/";
			string gendersDir = $"{cardsDir}genders/";
			string charactersDir = $"{cardsDir}characters/";
			string buffsDir = $"{cardsDir}buffs/";
			string spellsDir = $"{cardsDir}spells/";

			Weapon bokken = new Weapon("bokken", $"{weaponsDir}bokken.jpg", 1, 1);
			Weapon bo = new Weapon("bo", $"{weaponsDir}bo.jpg", 2, 1);
			Weapon daikyu = new Weapon("daikyu", $"{weaponsDir}daikyu.jpg", 5, 2);
			Weapon kanabo = new Weapon("kanabo", $"{weaponsDir}kanabo.jpg", 3, 2);
			Weapon katana = new Weapon("katana", $"{weaponsDir}katana.jpg", 2, 3);
			Weapon kiseru = new Weapon("kiseru", $"{weaponsDir}kiseru.jpg", 1, 2);
			Weapon kusarigama = new Weapon("kusarigama", $"{weaponsDir}kusarigama.jpg", 2, 2);
			Weapon nagayari = new Weapon("nagayari", $"{weaponsDir}nagayari.jpg", 4, 2);
			Weapon naginata = new Weapon("naginata", $"{weaponsDir}naginata.jpg", 4, 1);
			Weapon nodachi = new Weapon("nodachi", $"{weaponsDir}nodachi.jpg", 3, 3);
			Weapon shuriken = new Weapon("shuriken", $"{weaponsDir}shuriken.jpg", 3, 1);
			Weapon tanegashima = new Weapon("tanegashima", $"{weaponsDir}tanegashima.jpg", 5, 1);
			Weapon wakizashi = new Weapon("wakizashi", $"{weaponsDir}wakizashi.jpg", 1, 3);

			Role ninja = new Role("ninja", $"{gendersDir}ninja.jpg", "En équipe avec les autres ninjas.", 4, Role.MultiplierType.two);
			Role ronin = new Role("ronin", $"{gendersDir}ronin.jpg", "Tu es seul contre tous.", 4, Role.MultiplierType.three);
			Role samurai = new Role("samurai", $"{gendersDir}samurai.jpg", "En équipe avec les autres samurais et le shogun.", 4, Role.MultiplierType.one);
			Role shogun = new Role("shogun", $"{gendersDir}shogun.jpg", "En équipe avec les samurais.", 5, Role.MultiplierType.one);

			Character benkei = new Character("benkei", $"{charactersDir}benkei.jpg", "Les autres joueurs vous attaquent avec une difficulté augmentée de 1.", 5);
			Character chiyome = new Character("chiyome", $"{charactersDir}chiyome.jpg", "Seules les armes peuvent vous faire perdre des points de vie.", 4);
			Character ginchiyo = new Character("ginchiyo", $"{charactersDir}ginchiyo.jpg", "Vous réduisez de 1 les dégâts des armes (minimum 1).", 4);
			Character goemon = new Character("goemon", $"{charactersDir}goemon.jpg", "Vous pouvez jouer 1 arme supplémentaire par tour.", 5);
			Character hanzo = new Character("hanzo", $"{charactersDir}hanzo.jpg", "Vous pouvez jouer vos cartes d'arme comme des parades (à moins que ça ne soit votre dernière carte en main).", 4);
			Character hideyoshi = new Character("hideyoshi", $"{charactersDir}hideyoshi.jpg", "Durant votre phase de pioche, piochez 1 carte supplémentaire.", 4);
			Character ieyasu = new Character("ieyasu", $"{charactersDir}ieyasu.jpg", "Durant votre phase de pioche, vous pouvez prendre la carte du dessus de la défausse au lieu de piocher votre première carte. Vous devez piocher normalement la deuxième.", 5);
			Character kojiro = new Character("kojiro", $"{charactersDir}kojiro.jpg", "Vos armes peuvent attaquer n'importe quel autre joueur sans tenir compte de la difficulté.", 5);
			Character musashi = new Character("musashi", $"{charactersDir}musashi.jpg", "Vos armes causent 1 dégat supplémentaire.", 5);
			Character nobunaga = new Character("nobunaga", $"{charactersDir}nobunaga.jpg", "Durant votre tour, vous pouvez perdre 1 point de vie (sauf votre dernier) pour piocher 1 carte.", 5);
			Character tomoe = new Character("tomoe", $"{charactersDir}tomoe.jpg", "Chaque fois qu'une de vos armes blesse un joueur, piochez 1 carte.", 5);
			Character ushiwaka = new Character("ushiwaka", $"{charactersDir}ushiwaka.jpg", "Chaque fois que vous perdez 1 point de vie à cause d'une arme, piochez 1 carte.", 4);

			Buff armure = new Buff("armure", $"{buffsDir}armure.jpg", "Les autres joueurs vous attaquent avec une difficulté augmentée de 1.", Buff.Type.Armor);
			Buff attaque_rapide = new Buff("attaque rapide", $"{buffsDir}attaque_rapide.jpg", "Vos armes causent 1 dégâts supplémentaire.", Buff.Type.Damage);
			Buff concentration = new Buff("concentration", $"{buffsDir}concentration.jpg", "Vous pouvez jouer 1 arme de plus par tour.", Buff.Type.WeaponNumber);

			Spell ceremonie_du_the = new Spell("Cérémonie du thé", $"{spellsDir}ceremonie_du_the.jpg", "Piochez 3 cartes. Les autres joueurs piochent 1 carte.");
			Spell code_du_bushido = new Spell("code_du_bushido", $"{spellsDir}code_du_bushido.jpg", "Si la carte est devant vous, piochez une carte et défaussez-la. Si c'est une arme vous devez défausser une arme sinon vous perdais 1 point d'honneur. Si ce n'est pas une arme, passez la carte à votre voisin de gauche.");
			Spell cri_de_guerre = new Spell("cri_de_guerre", $"{spellsDir}cri_de_guerre.jpg", "Les joueurs doivent défausser 1 parade ou perdre 1 point de vie.");
			Spell daimyo = new Spell("daimyo", $"{spellsDir}daimyo.jpg", "Piochez 2 cartes. Si vous possédez cette carte à la fin de la partie, elle vous rapporte 1 point d'honneur.");
			Spell diversion = new Spell("diversion", $"{spellsDir}diversion.jpg", "Piochez au hasard 1 carte dans la main d'un joueur de votre choix et ajoutez-la à votre main.");
			Spell geisha = new Spell("geisha", $"{spellsDir}geisha.jpg", "Défaussez 1 carte permanente en jeu ou 1 carte au hasard de la main d'un autre joueur.");
			Spell jujitsu = new Spell("jujitsu", $"{spellsDir}jujitsu.jpg", "Les autres joueurs doivent défausser 1 arme ou perdre 1 point de vie.");
			Spell meditation = new Spell("méditation", $"{spellsDir}meditation.jpg", "Récupérez tous vos points de vie. Un autre joueur de votre choix pioche 1 carte.");
			Spell parade = new Spell("parade", $"{spellsDir}parade.jpg", "Bloque une attaque causée par une arme.");

			foreach (Card card in new List<Card>() {
					bokken, bo, daikyu, kanabo, katana, kiseru, kusarigama, nagayari, naginata, nodachi, shuriken, tanegashima, wakizashi,
					ninja, ronin, samurai, shogun,
					benkei, chiyome, ginchiyo, goemon, hanzo, hideyoshi, ieyasu, kojiro, musashi, nobunaga, tomoe, ushiwaka,
					armure, attaque_rapide, concentration,
					ceremonie_du_the, code_du_bushido, cri_de_guerre, daimyo, diversion, geisha, jujitsu, meditation, parade
				}) 
			{
				card.Println();
			}

			Environment.Exit(0);
		}
	}
}
