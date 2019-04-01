using System.Collections.Generic;

namespace Games {
	public interface IPhased {
		IEnumerable<IPhase> Phases {get;}
	}
	public interface IPhased<out TPhase> : IPhased where TPhase : IPhase {
		new IEnumerable<TPhase> Phases {get;}
	}
}
