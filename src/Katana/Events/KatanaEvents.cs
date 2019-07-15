using System;
using System.Collections.Generic;
using System.Text;

namespace KatanaGame.Events {
	/* Death related achievments / ranked achievements (10/50/100/500
	 * - Premier sang => Au cours d'une partie, être le premier à tuer un autre joueur
	 * - Acharnement => Au cours d'une partie, être tué trois fois d'affilé sans qu'un autre joueur ne meure.
	 * - Le Phoenix => Au cours d'une partie, mourir chaque tour.
	 * - Perte sèche => Poser quatre cartes de propriété druant votre tour, et mourir avant votre porochain.
	 * - Double peine => Le même tour, voler une arme à un adversaire et le tuer avec.
	 * - Grand final => Le même tour, revivre, être victime du bushido et perdre son dernier point d'honneur en piochant une carte de trop.
	 */
	public class PlayerEvent : KatanaGameEvent {
		public ulong DiscordId { get; }
		public PlayerEvent(ulong discord_id) {
			this.DiscordId = discord_id;
		}
	}
	public class PlayerJoinsEvent : PlayerEvent {
		public PlayerJoinsEvent(ulong discord_id) : base(discord_id) { }
	}
	public class PlayerLeavesEvent : PlayerEvent {
		public PlayerLeavesEvent(ulong discord_id) : base(discord_id) { }
	}
	public class GameLaunchEvent : KatanaGameEvent { }
	public class GamePlayEvent : KatanaGameEvent {
		public GamePlayEvent( ) : base( ) { }
	}

	public class CharacterEvent : GamePlayEvent {
		/* public <CharacterType> Character { get; } */
		public CharacterEvent(/* %TODO% <CharacterType> character */) : base( ) {
			
		}
	}
	public class CharacterDiedEvent : CharacterEvent {
		public CharacterDiedEvent(/* %TODO% <CharacterType> character */) : base(/* character */) { }
	}
	public class CharacterRevivedEvent : CharacterEvent {
		public CharacterRevivedEvent(/* %TODO% <CharacterType> character */) : base(/* character */) { }
	}
	public class CharacterPointsEvent : CharacterEvent {
		public int Amount { get; }
		public CharacterPointsEvent(/* %TODO% <CharacterType> character, */ int amount) : base(/* character */) {
			this.Amount = amount;
		}
	}
	public class CharacterLostHonorPointsEvent : CharacterPointsEvent {
		public CharacterLostHonorPointsEvent(/* %TODO% <CharacterType> character, */ int amount) : base(/* character, */ amount) { }
	}
	public class CharacterGainedHonorPointsEvent : CharacterPointsEvent {
		public CharacterGainedHonorPointsEvent(/* %TODO% <CharacterType> character, */ int amount) : base(/* character, */ amount) { }
	}
	public class CharacterLostResiliencePointsEvent : CharacterPointsEvent {
		public CharacterLostResiliencePointsEvent(/* %TODO% <CharacterType> character, */ int amount) : base(/* character, */ amount) { }
	}
	public class CharacterGainedResiliencePointsEvent : CharacterPointsEvent {
		public CharacterGainedResiliencePointsEvent(/* %TODO% <CharacterType> character, */ int amount) : base(/* character, */ amount) { }
	}
	public class CharacterRanoutOfCardsEvent : CharacterEvent {
		public CharacterRanoutOfCardsEvent(/* %TODO% <CharacterType> character */) : base(/* character */) { }
	}
}
