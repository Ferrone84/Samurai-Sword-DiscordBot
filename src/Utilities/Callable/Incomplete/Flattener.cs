using System.Collections.Generic;
using CollectionExtensions;
namespace Callable.Incomplete {
	public class Flattener<TElement, TRequired> : ACaller<IEnumerable<IEnumerable<TElement>>, TRequired>, ICallable<IEnumerable<TElement>, TRequired> {
		public Flattener(ICallable<IEnumerable<IEnumerable<TElement>>, TRequired> callable) : base(callable) {}
		public IEnumerable<TElement> Call(TRequired requirement) => this.callable.Call(requirement).Flatten();
	}
}
