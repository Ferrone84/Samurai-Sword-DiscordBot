using System.Collections.Generic;
using System.Linq;

using CollectionExtensions;

namespace Cards
{
	public class CardManager
	{
		public static string CARDS_DIR = "resources/cards/";
		public static string WEAPONS_DIR = $"{CARDS_DIR}weapons/";
		public static string ROLES_DIR = $"{CARDS_DIR}genders/";
		public static string CHARACTERS_DIR = $"{CARDS_DIR}characters/";
		public static string BUFFS_DIR = $"{CARDS_DIR}buffs/";
		public static string SPELLS_DIR = $"{CARDS_DIR}spells/";
        
        public List<Card> Deck { get; }
		public List<Card> Characters { get; }
        public List<Card> Roles { get; }

        public CardManager(int playerNumber) 
        {
            Weapon bokken = new Weapon("bokken", $"{WEAPONS_DIR}bokken.jpg", 1, 1);
			Weapon bo = new Weapon("bo", $"{WEAPONS_DIR}bo.jpg", 2, 1);
			Weapon daikyu = new Weapon("daikyu", $"{WEAPONS_DIR}daikyu.jpg", 5, 2);
			Weapon kanabo = new Weapon("kanabo", $"{WEAPONS_DIR}kanabo.jpg", 3, 2);
			Weapon katana = new Weapon("katana", $"{WEAPONS_DIR}katana.jpg", 2, 3);
			Weapon kiseru = new Weapon("kiseru", $"{WEAPONS_DIR}kiseru.jpg", 1, 2);
			Weapon kusarigama = new Weapon("kusarigama", $"{WEAPONS_DIR}kusarigama.jpg", 2, 2);
			Weapon nagayari = new Weapon("nagayari", $"{WEAPONS_DIR}nagayari.jpg", 4, 2);
			Weapon naginata = new Weapon("naginata", $"{WEAPONS_DIR}naginata.jpg", 4, 1);
			Weapon nodachi = new Weapon("nodachi", $"{WEAPONS_DIR}nodachi.jpg", 3, 3);
			Weapon shuriken = new Weapon("shuriken", $"{WEAPONS_DIR}shuriken.jpg", 3, 1);
			Weapon tanegashima = new Weapon("tanegashima", $"{WEAPONS_DIR}tanegashima.jpg", 5, 1);
			Weapon wakizashi = new Weapon("wakizashi", $"{WEAPONS_DIR}wakizashi.jpg", 1, 3);

			Character benkei = new Character("Benkei", $"{CHARACTERS_DIR}benkei.jpg", "Les autres joueurs vous attaquent avec une difficulté augmentée de 1.", 5);
			Character chiyome = new Character("Chiyome", $"{CHARACTERS_DIR}chiyome.jpg", "Seules les armes peuvent vous faire perdre des points de vie.", 4);
			Character ginchiyo = new Character("Ginchiyo", $"{CHARACTERS_DIR}ginchiyo.jpg", "Vous réduisez de 1 les dégâts des armes (minimum 1).", 4);
			Character goemon = new Character("Goemon", $"{CHARACTERS_DIR}goemon.jpg", "Vous pouvez jouer 1 arme supplémentaire par tour.", 5);
			Character hanzo = new Character("Hanzo", $"{CHARACTERS_DIR}hanzo.jpg", "Vous pouvez jouer vos cartes d'arme comme des parades (à moins que ça ne soit votre dernière carte en main).", 4);
			Character hideyoshi = new Character("Hideyoshi", $"{CHARACTERS_DIR}hideyoshi.jpg", "Durant votre phase de pioche, piochez 1 carte supplémentaire.", 4);
			Character ieyasu = new Character("Ieyasu", $"{CHARACTERS_DIR}ieyasu.jpg", "Durant votre phase de pioche, vous pouvez prendre la carte du dessus de la défausse au lieu de piocher votre première carte. Vous devez piocher normalement la deuxième.", 5);
			Character kojiro = new Character("Kojiro", $"{CHARACTERS_DIR}kojiro.jpg", "Vos armes peuvent attaquer n'importe quel autre joueur sans tenir compte de la difficulté.", 5);
			Character musashi = new Character("Musashi", $"{CHARACTERS_DIR}musashi.jpg", "Vos armes causent 1 dégat supplémentaire.", 5);
			Character nobunaga = new Character("Nobunaga", $"{CHARACTERS_DIR}nobunaga.jpg", "Durant votre tour, vous pouvez perdre 1 point de vie (sauf votre dernier) pour piocher 1 carte.", 5);
			Character tomoe = new Character("Tomoe", $"{CHARACTERS_DIR}tomoe.jpg", "Chaque fois qu'une de vos armes blesse un joueur, piochez 1 carte.", 5);
			Character ushiwaka = new Character("Ushiwaka", $"{CHARACTERS_DIR}ushiwaka.jpg", "Chaque fois que vous perdez 1 point de vie à cause d'une arme, piochez 1 carte.", 4);
			Character rikyu = new Character("Rikyu", $"{CHARACTERS_DIR}rikyu.jpg", "Lorsque la pioche est épuisée, vous ne perdez pas de point d'honneur.", 4);

			Buff armure = new Buff("armure", $"{BUFFS_DIR}armure.jpg", "Les autres joueurs vous attaquent avec une difficulté augmentée de 1.", Buff.Type.Armor);
			Buff attaque_rapide = new Buff("attaque rapide", $"{BUFFS_DIR}attaque_rapide.jpg", "Vos armes causent 1 dégâts supplémentaire.", Buff.Type.Damage);
			Buff concentration = new Buff("concentration", $"{BUFFS_DIR}concentration.jpg", "Vous pouvez jouer 1 arme de plus par tour.", Buff.Type.WeaponNumber);

			Spell ceremonie_du_the = new Spell("Cérémonie du thé", $"{SPELLS_DIR}ceremonie_du_the.jpg", "Piochez 3 cartes. Les autres joueurs piochent 1 carte.");
			Spell code_du_bushido = new Spell("code_du_bushido", $"{SPELLS_DIR}code_du_bushido.jpg", "Si la carte est devant vous, piochez une carte et défaussez-la. Si c'est une arme vous devez défausser une arme sinon vous perdais 1 point d'honneur. Si ce n'est pas une arme, passez la carte à votre voisin de gauche.");
			Spell cri_de_guerre = new Spell("cri_de_guerre", $"{SPELLS_DIR}cri_de_guerre.jpg", "Les joueurs doivent défausser 1 parade ou perdre 1 point de vie.");
			Spell daimyo = new Spell("daimyo", $"{SPELLS_DIR}daimyo.jpg", "Piochez 2 cartes. Si vous possédez cette carte à la fin de la partie, elle vous rapporte 1 point d'honneur.");
			Spell diversion = new Spell("diversion", $"{SPELLS_DIR}diversion.jpg", "Piochez au hasard 1 carte dans la main d'un joueur de votre choix et ajoutez-la à votre main.");
			Spell geisha = new Spell("geisha", $"{SPELLS_DIR}geisha.jpg", "Défaussez 1 carte permanente en jeu ou 1 carte au hasard de la main d'un autre joueur.");
			Spell jujitsu = new Spell("jujitsu", $"{SPELLS_DIR}jujitsu.jpg", "Les autres joueurs doivent défausser 1 arme ou perdre 1 point de vie.");
			Spell meditation = new Spell("méditation", $"{SPELLS_DIR}meditation.jpg", "Récupérez tous vos points de vie. Un autre joueur de votre choix pioche 1 carte.");
			Spell parade = new Spell("parade", $"{SPELLS_DIR}parade.jpg", "Bloque une attaque causée par une arme.");

			Role ninja = new Role("ninja", $"{ROLES_DIR}ninja.jpg", "En équipe avec les autres ninjas.", 4, Role.MultiplierType.one);
			Role ninja_x2 = new Role("ninja", $"{ROLES_DIR}ninja.jpg", "En équipe avec les autres ninjas.", 4, Role.MultiplierType.two);
			Role ronin_x2 = new Role("ronin", $"{ROLES_DIR}ronin.jpg", "Seul contre tous.", 4, Role.MultiplierType.two);
			Role ronin_x3 = new Role("ronin", $"{ROLES_DIR}ronin.jpg", "Seul contre tous.", 4, Role.MultiplierType.three);
			Role samurai = new Role("samurai", $"{ROLES_DIR}samurai.jpg", "En équipe avec les autres samurais et le shogun.", 4, Role.MultiplierType.one);
			Role samurai_x2 = new Role("samurai", $"{ROLES_DIR}samurai.jpg", "En équipe avec le shogun.", 4, Role.MultiplierType.two);
			Role shogun = new Role("shogun", $"{ROLES_DIR}shogun.jpg", "En équipe avec les samurais.", 5, Role.MultiplierType.one);
			Role shogun_x2 = new Role("shogun", $"{ROLES_DIR}shogun.jpg", "Seul contre les ninjas.", 5, Role.MultiplierType.two);


            Deck = new List<Card>();
            //Actions
            Register(parade, 15);
            Register(geisha, 6);
            Register(diversion, 5);
            Register(ceremonie_du_the, 4);
            Register(cri_de_guerre, 4);
            Register(jujitsu, 3);
            Register(meditation, 3);
            Register(daimyo, 3);

            //Propriétés
            Register(concentration, 6);
            Register(armure, 4);
            Register(attaque_rapide, 3);
            Register(code_du_bushido, 2);

            //Armes
            Register(bokken, 6);
            Register(bo, 5);
            Register(kiseru, 5);
            Register(kusarigama, 4);
            Register(shuriken, 3);
            Register(naginata, 2);
            Register(daikyu);
            Register(kanabo);
            Register(katana);
            Register(nagayari);
            Register(nodachi);
            Register(tanegashima);
            Register(wakizashi);

            Characters = new List<Card>() {
				benkei, chiyome, ginchiyo, goemon, hanzo, hideyoshi, ieyasu, kojiro, musashi, nobunaga, tomoe, ushiwaka, rikyu
			};

            switch (playerNumber) {
                case 3:
                    Roles = new List<Card>() {shogun_x2, ninja, ninja}; //1 Shogun, 2 Ninja
                break;                
                case 4:
                    Roles = new List<Card>() {shogun, samurai_x2};
                    Roles.AddRange(new List<Card>() {ninja, ninja, ninja_x2}.Shuffle().Take(2)); //1 Shogun, 1 Samurai, 2 Ninja (remove from the game one of the 3 Ninjas at random, without looking at it)
                break;                
                case 5:
                    Roles = new List<Card>() {shogun, samurai, ronin_x2, ninja, ninja}; //1 Shogun, 1 Samurai, 1 Ronin, 2 Ninja
                break;                
                case 6:
                    Roles = new List<Card>() {shogun, samurai_x2, ronin_x3, ninja, ninja, ninja}; //1 Shogun, 1 Samurai, 1 Ronin, 3 Ninja
                break;                
                case 7:
                    Roles = new List<Card>() {shogun, samurai, samurai, ronin_x3, ninja, ninja, ninja}; //1 Shogun, 2 Samurai, 1 Ronin, 3 Ninja
                break;
            }
        }
        
        private void Register(Card card, int number = 1)
        {
            for (int i=0; i < number; i++) {
                Deck.Add(card);
            }
        }
    }
}
