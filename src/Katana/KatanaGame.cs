using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Games;
using Games.Cards;
using KatanaGame.Cards;
using CollectionExtensions;
using NumericExtensions;

namespace KatanaGame {
	public class KatanaGame : IGame<KatanaGameInstance> {
		private ConcurrentDictionary<string, Cards.AKatanaPlayingCardModel> playing_cards = new ConcurrentDictionary<string, AKatanaPlayingCardModel>(
			new Dictionary<string, Cards.AKatanaPlayingCardModel>( ) {
				/* Weapons */
				{"bokken"     , new Cards.Weapon("Bokken"     , "bokken"     , 1, 1)},
				{"bo"         , new Cards.Weapon("Bo"         , "bo"         , 2, 1)},
				{"daikyu"     , new Cards.Weapon("Daikyu"     , "daikyu"     , 5, 2)},
				{"kanabo"     , new Cards.Weapon("Kanabo"     , "kanabo"     , 3, 2)},
				{"katana"     , new Cards.Weapon("Katana"     , "katana"     , 2, 3)},
				{"kiseru"     , new Cards.Weapon("Kiseru"     , "kiseru"     , 1, 2)},
				{"kusarigama" , new Cards.Weapon("Kusarigama" , "kusarigama" , 2, 2)},
				{"nagayari"   , new Cards.Weapon("Nagayari"   , "nagayari"   , 4, 2)},
				{"naginata"   , new Cards.Weapon("Naginata"   , "naginata"   , 4, 1)},
				{"nodachi"    , new Cards.Weapon("Nodachi"    , "nodachi"    , 3, 3)},
				{"shuriken"   , new Cards.Weapon("Shuriken"   , "shuriken"   , 3, 1)},
				{"tanegashima", new Cards.Weapon("Tanegashima", "tanegashima", 5, 1)},
				{"wakizashi"  , new Cards.Weapon("Wakizashi"  , "wakizashi"  , 1, 3)},
				/* Actions */
				{"tea-ceremony", new Cards.Action("Cérémonie du thé", "Piochez 3 cartes. Les autres joueurs piochent 1 carte.", "tea-ceremony")},
				{"bushido", new Cards.Action("Code du Bushido", "Si la carte est devant vous au début de votre tour, piochez une carte et défaussez-la. Si c'est une arme, défaussez une arme et passez ensuite la carte à votre voisin de gauche, sinon, passez la carte à votre voisin de gauche. Si vous devez défausser une arme mais n'en avez pas, vous perdez un point d'honneur et défaussez cette carte. Votre tour se déroule ensuite normalement.", "bushido")},
				{"battle-cry", new Cards.Action("Cri de guerre", "Les autres joueurs doivent défausser 1 parade ou perdre 1 point de vie.", "battle-cry")},
				{"daimyo", new Cards.Action("Daimyo", "Piochez 2 cartes. Si vous possédez cette carte à la fin de la partie et n'êtes pas Ronin, vous gagner 1 point d'honneur après l'application d'un éventuel mutliplicateur.", "daimyo")},
				{"diversion", new Cards.Action("Diversion", "Piochez au hasard 1 carte dans la main d'un joueur de votre choix et ajoutez-la à votre main.", "diversion")},
				{"geisha", new Cards.Action("Geisha", "Défaussez 1 carte permanente en jeu (hors Code du Bushido) ou 1 carte au hasard de la main d'un autre joueur.", "geisha")},
				{"jujitsu", new Cards.Action("Jujitsu", "Les autres joueurs doivent défausser 1 arme ou perdre 1 point de vie.", "jujitsu")},
				{"breathing", new Cards.Action("Méditation", "Récupérez tous vos points de vie. Un joueur de votre choix (autre que vous) pioche 1 carte.", "breathing")},
				{"parry", new Cards.Action("Parade", "Bloque une attaque causée par une arme.", "parry")},
				/* Properties */
				{"armor", new Cards.Property("Armure", "Les autres joueurs vous attaquent avec une difficulté augmentée de 1.", "armor")},
				{"fast-draw", new Cards.Property("Attaque rapide", "Vos armes causent 1 blessure supplémentaire.", "fast-draw")},
				{"focus", new Cards.Property("Concentration", "Vous pouvez jouer 1 arme de plus par tour.", "focus")}
			}
		);
		private ConcurrentDictionary<string, Character> character_cards = new ConcurrentDictionary<string, Character>(
			new Dictionary<string, Character>() {
				{"benkei", new Character("Benkei", "Les autres joueurs vous attaquent avec une difficulté augmentée de 1.", "benkei", 5)},
				{"chiyome", new Character("Chiyome", "Seules les armes peuvent vous faire perdre des points de vie.", "chiyome", 4)},
				{"ginchiyo", new Character("Ginchiyo", "Les armes vous infligent 1 blessure de moins (minimum 1).", "ginchiyo", 4)},
				{"goemon", new Character("Goemon", "Vous pouvez jouer 1 arme supplémentaire par tour.", "goemon", 5)},
				{"hanzo", new Character("Hanzo", "Vous pouvez jouer vos cartes d'arme comme des parades (à moins que ça ne soit votre dernière carte en main).", "hanzo", 4)},
				{"hideyoshi", new Character("Hideyoshi", "Durant votre phase de pioche, piochez 1 carte supplémentaire.", "hideyoshi", 4)},
				{"ieyasu", new Character("Ieyasu", "Durant votre phase de pioche, vous pouvez prendre la carte du dessus de la défausse au lieu de piocher votre première carte. Vous devez piocher la deuxième normalement.", "ieyasu", 5)},
				{"kojiro", new Character("Kojiro", "Vos armes peuvent attaquer n'importe quel autre joueur sans tenir compte de la difficulté.", "kojiro", 5)},
				{"musashi", new Character("Musashi", "Vos armes causent 1 blessure supplémentaire.", "musashi", 5)},
				{"nobunaga", new Character("Nobunaga", "Durant votre tour, vous pouvez sacrifier 1 point de vie (sauf votre dernier) pour piocher 1 carte.", "nobunaga", 5)},
				{"tomoe", new Character("Tomoe", "Chaque fois qu'une de vos armes blesse un joueur, piochez 1 carte.", "tomoe", 5)},
				{"ushiwaka", new Character("Ushiwaka", "Chaque fois que vous perdez 1 point de vie à cause d'une arme, piochez 1 carte.", "ushiwaka", 4)},
				{"rikyu", new Character("Rikyu", "Vous ne perdez pas de point d'honneur lorsque la pioche est épuisée.", "rikyu", 4)}
			}
		);
		private ConcurrentDictionary<string, Role> role_cards = new ConcurrentDictionary<string, Role>(
			new Dictionary<string, Role>() {
				{"ninja1x", new Role("Ninja (1 étoile)", "Fait équipe avec les autres ninjas.", "ninja", Role.StarRank.One)},
				{"ninja2x", new Role("Ninja (2 étoiles)", "Fait équipe avec les autres ninjas.", "ninja", Role.StarRank.Two)},
				{"ninja3x", new Role("Ninja (3 étoiles)", "Fait équipe avec les autres ninjas.", "ninja", Role.StarRank.Three)},
				{"ronin", new Role("Ronin", "Seul contre tous.", "ronin", Role.StarRank.None)},
				{"samurai", new Role("Samurai", "En équipe avec les autres samurais et le shogun.", "samurai", Role.StarRank.None)},
				{"samurai1x", new Role("Samurai (1 étoile)", "Fait équipe avec les autres samurais et le shogun.", "samurai", Role.StarRank.One)},
				{"samurai2x", new Role("Samurai (2 étoiles)", "Fait équipe avec les autres samurais et le shogun.", "samurai", Role.StarRank.Two)},
				{"shogun", new Role("Shogun", "Fait équipe avec les samurais.", "shogun", Role.StarRank.None)},
			}
		);
		public KatanaGame( ) {
			
		}

		public string Name { get => "Katana"; }
		public string Description { get => "Incarnez le Shogun, un de ses fidèles samuraïs, un ninja ou le solitaire ronin, et battez vous pour défendre l'honneur de votre camp."; }
		public string Rules { get => throw new System.NotImplementedException(); }
		IGameInstance IGame.NewGame( ) => this.NewGame( );
		public KatanaGameInstance NewGame( ) => new KatanaGameInstance(this);
		internal IReadOnlyDictionary<string, Role> GetRoles( ) { return this.role_cards; }
		internal IReadOnlyDictionary<string, Character>  GetCharacters( ) { return this.character_cards; }
		internal IReadOnlyDictionary<string, AKatanaPlayingCardModel> GetPlayingCards( ) { return this.playing_cards; }
		internal IEnumerable<ICardCopy<AKatanaPlayingCardModel>> MakeDefaultDeck( ) {
			var list = new LinkedList<AKatanaPlayingCardModel>( );
			Action<string, int> put = (model_name, times) => {
				AKatanaPlayingCardModel model = this.playing_cards[model_name];
				times.Times(( ) => list.AddLast(model));
			};
			//Actions
			put("parade", 15);
			put("geisha", 6);
			put("diversion", 5);
			put("ceremonie_du_the", 4);
			put("cri_de_guerre", 4);
			put("jujitsu", 3);
			put("meditation", 3);
			put("daimyo", 3);

			//Propriétés
			put("concentration", 6);
			put("armure", 4);
			put("attaque_rapide", 3);
			put("code_du_bushido", 2);

			//Armes
			put("bokken", 6);
			put("bo", 5);
			put("kiseru", 5);
			put("kusarigama", 4);
			put("shuriken", 3);
			put("naginata", 2);
			put("daikyu", 1);
			put("kanabo", 1);
			put("katana", 1);
			put("nagayari", 1);
			put("nodachi", 1);
			put("tanegashima", 1);
			put("wakizashi", 1);
			return list.Mapped(model => new CardCopy<AKatanaPlayingCardModel>(model));
		}
	}
}