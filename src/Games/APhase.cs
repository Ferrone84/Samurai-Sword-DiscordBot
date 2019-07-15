using System.Threading;
using System.Threading.Tasks;

namespace Games {
	abstract class APhase< TEvent> : IPhase<TEvent> {
		private CancellationTokenSource running;
		protected bool Terminated { get => this.running.IsCancellationRequested; }
		public APhase( ) { }
		public async Task Run( ) {
			await this.Setup( );
			this.running = new CancellationTokenSource( );
			await this.Proceed( );
			await this.ClearUp( );
		}
		protected virtual async Task Setup( ) { /* Should be overridden to execute tasks before the phase pauses to handle events */ }
		protected virtual async Task Proceed( ) {
			try { await Task.Delay(-1, this.running.Token); }
			catch (TaskCanceledException) { }
		}
		public virtual async Task Event(TEvent game_event) { /* Should be overridden tp handle Events */ }
		public virtual async Task Terminate( ) { this.running.Cancel( ); /* Should be overridden to handle complex cases of phase interruption */ }
		protected virtual async Task ClearUp( ) { }
	}
	abstract class APhase<TGameInstance, TEvent> : APhase<TEvent> {
		protected TGameInstance GameInstance { get; }
		public APhase(TGameInstance game_instance) {
			this.GameInstance = game_instance;
		}
	}
}
