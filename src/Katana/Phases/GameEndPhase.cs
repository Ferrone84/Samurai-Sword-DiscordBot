using System.Threading.Tasks;
using Games;
using KatanaGame.Events;

namespace KatanaGame {
	internal class GameEndPhase : APhase<KatanaGameInstanceState, KatanaGameEvent> {
		public GameEndPhase(KatanaGameInstanceState game_state) : base(game_state) { }
		public override async Task Event(KatanaGameEvent katana_event) {
			if (katana_event is PlayerJoinsEvent player_joins) { }
			else if (katana_event is PlayerLeavesEvent player_leaves) { }
			else if (katana_event is GameLaunchEvent game_launch) { }
		}
	}
}
