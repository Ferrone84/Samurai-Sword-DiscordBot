using System.Collections.Generic;
using System.Linq;

using Games.Cards;
using KatanaGame.Cards;

using CollectionExtensions;

namespace KatanaGame {
	public class KatanaPlayer : IHandedPlayer /* %TODO%, IBoardedPlayer*/ {
		public class StatValues {
			public int Base;
			public int Bonus;
		}
		public class StatsValues {
			public StatValues Armor;
			public StatValues Weapons;
			public StatValues Damage;
		}
		public string Name { get; set; }
		public Role Role { get; set; }
		public Character Character { get; set; }
		public bool IsDead { get => (this.Resilience == 0); }
		public bool IsHarmless { get => (this.Hand.IsEmpty()); }
		public int Honor { get; }
		public int Resilience { get; }
		public StatsValues Stats { get; }
		IEnumerable<ICardCopy> IHandedPlayer.Hand { get => this.Hand; }
		internal IEnumerable<ICardCopy<AKatanaPlayingCardModel>> Hand { get; }
		public KatanaPlayer( ) {
			/* Global  */
			this.Name = "";
			this.Role = null;
			this.Character = null;
			/* State */
			this.Honor = 4;
			this.Resilience = this.Character.Resilience;
			var s = default(StatsValues);
			s.Armor.Base = 27;
			s.Armor.Bonus = 1;
			s.Weapons.Base = 1;
			s.Weapons.Bonus = 0;
			s.Damage.Base = 1;
			s.Damage.Bonus = 18;
			this.Stats = default(StatsValues);

			this.Hand = new List<ICardCopy<AKatanaPlayingCardModel>>();
			/* Note this is a mock */
		}
	}
}
