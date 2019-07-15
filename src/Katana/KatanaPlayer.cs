using System.Collections.Generic;
using System.Linq;

using Games.Cards;

using CollectionExtensions;

namespace KatanaGame {
	public class KatanaPlayer : IHandedPlayer /* %TODO%, IBoardedPlayer*/ {
		public struct StatValues {
			public int Base;
			public int Bonus;
		}
		public struct StatsValues {
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
		private readonly IEnumerable<ICardCopy> _hand;
		public IEnumerable<ICardCopy> Hand { get =>this._hand.AsEnumerable(); }
		public KatanaPlayer() {
			/* Global  */
			this.Name = "MockChiyome";
			this.Role = new Role("Shogun", "Le shogun", "shogun", Role.StarRank.Two);
			this.Character = new Character("Chiyome", "Nope", "chiyome", 5);
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
			this.Stats = s;

			this._hand = new List<ICardCopy>();
			/* Note this is a mock */
		}
	}
}
