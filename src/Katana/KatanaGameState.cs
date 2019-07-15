using System.Collections.Generic;

namespace KatanaGame {
	public class KatanaGameState {
		public List<KatanaPlayer> Players { get; }
		public KatanaGameState() {
			this.Players = new List<KatanaPlayer>();
		}
	}
}
