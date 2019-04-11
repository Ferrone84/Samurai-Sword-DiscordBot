using System.Collections.Generic;
using Games.Cards;

namespace KatanaGame {
	public class KatanaPlayer : IHandedPlayer {
		public string Name { get => "MockPlayer"; }
		public Character Character { get => new Character("Chiyome", "Nope", "chiyome", 5); }
		public Role Role { get => new Role("Shogun", "Le shogun", "shogun", Role.StarRank.Two); }
		public IEnumerable<ICardCopy> Hand {
			get { yield break; }
		}
	}
}
