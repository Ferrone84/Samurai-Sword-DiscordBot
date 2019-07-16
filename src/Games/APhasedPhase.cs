using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Games {

	abstract class APhasedPhase<TEvent> : APhase<TEvent> {
		private IEnumerable<IPhase> phases;
		private IPhase current_phase;
		protected IPhase CurrentPhase { get => this.current_phase; }

		public APhasedPhase(params IPhase[] phases) : base( ) {
			this.phases = phases;
			this.current_phase = null;
		}
		protected override sealed async Task Proceed( ) {
			while (!this.Terminated) {
				foreach (IPhase phase in this.phases) {
					if (this.Terminated) { return; }
					this.current_phase = phase;
					await phase.Run( );
					this.current_phase = null;
					if (await this.CheckForPrematureTermination( )) { await this.Terminate( ); }
				}
			}
		}
		protected virtual async Task<bool> CheckForPrematureTermination( ) { return false; /* Should be overridden to terminate the phase if needed */ }
		public override sealed async Task Terminate( ) {
			await this.current_phase?.Terminate( );
			await base.Terminate( );
		}
	}
	abstract class APhasedPhase<TGameInstance, TEvent> : APhase<TGameInstance, TEvent> {
		private IEnumerable<IPhase> phases;
		private IPhase current_phase;
		protected IPhase CurrentPhase { get => this.current_phase; }

		public APhasedPhase(TGameInstance game_instance, params IPhase[] phases) : base (game_instance) {
			this.phases = phases;
			this.current_phase = null;
		}
		protected override sealed async Task Proceed( ) {
			while (!this.Terminated) {
				foreach (IPhase phase in this.phases) {
					if (this.Terminated) { return; }
					this.current_phase = phase;
					await phase.Run( );
					this.current_phase = null;
					if (await this.CheckForPrematureTermination( )) { await this.Terminate( ); }
				}
			}
		}
		protected virtual async Task<bool> CheckForPrematureTermination( ) { return false; /* Should be overridden to terminate the phase if needed */ }
		public override sealed async Task Terminate( ) {
			if (this.current_phase != null) { await this.current_phase.Terminate( ); }
			await base.Terminate( );
		}
	}
}
