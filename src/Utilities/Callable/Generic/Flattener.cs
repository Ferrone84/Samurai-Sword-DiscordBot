using System.Collections.Generic;

using CollectionExtensions;
namespace Callable.Generic {
	public class Flattener<T> : ACaller<IEnumerable<IEnumerable<T>>>, ICallable<IEnumerable<T>> {
		public Flattener(ICallable<IEnumerable<IEnumerable<T>>> callable) : base(callable) {}
		public IEnumerable<T> Call() => this.callable.Call().Flatten();
	}
}
