namespace GamesDraft {
	public interface IPlayer {}
	public interface IGameState {
			// Doit pouvoir renvoyer l'état précis
			// -> Phase (IPhase)
			// -> Joueur courant (IPlayer)
	}
	public interface IGameState<TPlayer> : IGameState where TPlayer : IPlayer {
		/*
			Doit pouvoir renvoyer l'état précis
			-> Phase (IPhase<IGameState<TPlayer>>)
			-> Joueur courant (TPlayer)

			Le fait qu'une phase soit découpée en tours ou non n'est pas géré par cette interface.
			Si c'est fait ainsi, il y a alors une autre interface du genre ITurnBasedGameState<TPhase> where TPhase : IPhased<ITurn>
			Cette autre interface pourra alors garantir un type de retour de type TPhase
			-> le out du IPhased est alors nécessaire (pour garantir qu'une classe implémentant IPhased<MonTurnCustom> est bien une IPhased<ITurn> (cast implicite))
			et le in du IPhase aussi, si ça merde je corrigerai ce point
		*/

	}
	public interface IPhase {
		// ne garantit pas grand chose
	}
	public interface IPhase<in TGameState> : IPhase where TGameState : IGameState {
		/*
			Application d'une phase à un TGameState
			void LeNomDeLaMethode(TGameState gamestate){ doSomething(); }
		 */
	}
	public interface IPhased {
		/*
			Doit pouvoir renvoyer ses phases (IPhase)
		 */
	}
	public interface IPhased<out TPhase> : IPhased where TPhase : IPhase {
		/*
			Doit pouvoir renvoyer ses phases (TPhase)
		 */
	}
	public interface ITurn : IPhased {}
	public interface ITurn<TPhase> : ITurn, IPhased<TPhase> where TPhase : IPhase {
		/*
			Faut y attacher un joueur, mais j'ai pas prévu le truc là
		 */
	}
	public interface IGame : IPhased {
		/* Doit pouvoir générer une nouvelle partie, du IGameInProgress NewGame() */
		/* Le type de partie exact dépend du jeu lui-même, et donc la classe exacte de la partie ne sera connue que de la classe du jeu.
			Du coup la méthode NewGame() qui renvoie le type exact de la partie sera propre à la classe du jeu et ne doit pas exister dans les
			interfaces, donc pas d'interface du style IGame<TGameInProgess>
			Par contre on peut avoir ICardGameInProgress ICardGame.NewGame()
			Mais c'est pas forcément utile ouintelligent à faire, à voir
		 */
	}
	public interface IGame<TPlayer, TPhase> : IGame, IPhased<TPhase> where TPlayer : IPlayer where TPhase : IPhase<IGameState<TPlayer>> {}
	public interface IGameInProgress {}
	public interface IGameInProgress<TGame, TGameState> where TGame : IGame where TGameState : IGameState {
		/*
			Je ne pense pas que le GameInProgress ait quoi que ce soit à exposer en dehors de :
			La liste des joueurs
			Le jeu dont il découle
			L'interface de jeu à laquelle il est connecté

			Si on veut exposer le gamestate pour récupérer les stats des joueurs... pourquoi pas.
			Mais sinon le gamestate est transmis à l'interface de jeu en interne, l'interface ne le réclame pas : on lui donne avec les events de jeu.
			Le gameinprogress n'a pas grand chose à offrir en tant qu'interface^^
			Il doit quand même pouvoir recevoir des inputs de la part de l'interface, qui lui envoie les interactions des joueurs (pas les interactions brutes,
			genre on clique sur une réaction et l'interface de jeu dit au GameInProgress que le joueur veut jouer cette carte : transformer les inputs en actions
			 c'est son rôle).
		 */
	}

	/*
	---- Partie Cartes ----
	Types de cartes :
	Une carte ça a un nom, une description si possible, un type et des effets (si on peut rendre ça générique, c'est génial)
	La généralisation des effets va de paire avec les filtres... qui automatiseraient les mécanismes de pioche :
		Piocher dans le katana c'est :
			On prend où? Le deck commun
			On prend quoi? Les deux premières Cartes
			From(Common.Deck)
			Order(top to bottom)
			Pick(2)
		Appliquer la carte diversion (vol d'une carte) c'est :
			On prend où ? une main mais pas la sienne
			On prend quoi ? une carte au pif
			From(Players) IPlayer[]
			Exclude(Self) IPlayer[]
			Select(Hand) Hand[] -> appliqué sur une collection, renvoie une collection résultant de l'application à chaque élément
			Order(Reveal) Hand[] -> On révèle les mains, pas les cartes. Donc les noms des joueurs.
			Pick(1) IHand[] -> On une collection d'un élément
			Select(Cards) ICard[] -> collection de cartes. S'il y avait plusieurs mains, il y aurait aussi une seule collection des cartes de toutes les mains
			Order(Random) ICard[] -> au pif
			Pick(1) ICard[] -> 1 une seule carte
		
		S'il y a une règle où on présente les trois premières cartes et la personne choisit (révélation des cartes) :
			From(La collection) ICard[]
			Order(Top to bottom) ICard[]
			Pick(3) ICard[]
			Order(Reveal) ICard[] -> on révèle les cartes
			Pick(1) ICard[]
		on peut imaginer les mécaniques plus complexes où le joueur dot choisir parmi une collection
		et on révèle les cartes une par une jusqu'à qu'il en choisisse une, les autres restant alors cachées (RevealFromTop, RevealFromBottom...)
		On peut utiliser des notions comme Top/Beginning/Head pour désigner indifféremment le début d'une pioche ou d'une main.
		(les mains n'étant pas ordonnées dans notre cas, on est moyennement concernés POUR LE MOMENT)
	Pour les types de joueurs :
	On peut avoir les joueurs de carte avec une main IHandedCardPlayer
	Les joueurs avec un deck personnel IDeckedCardPlayer (pioche et défausse personnelles, style pokémon, yugiho, Magic the gathering, hearthstone...)
	Les joueurs avec un plateau personnel IBoardedCardPlayer (Katana, avec les propriétés)
	Plein de truc de ce genre qui sont importés au niveau de la classe (KatanaPlayer, ...) directement.

	de même on peut avoir les jeux à plateau commun (Poker à 2 cartes par exemple, et aussi katana avec le bushido en fait...)
	et deck commun (Katana)
	*/
}
