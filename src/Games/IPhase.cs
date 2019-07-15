using System.Threading.Tasks;

namespace Games {
	public interface IPhase {
		Task Run( );
		Task Terminate( );
	}
	public interface IPhase<in TEvent> : IPhase {
		Task Event(TEvent game_event);
	}
}
