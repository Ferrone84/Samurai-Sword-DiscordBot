using System.Collections.Generic;
namespace Callable.Incomplete {
	public class Source<TElement> : ICallable<IEnumerable<TElement>, IEnumerable<TElement>> {
		public Source() {}
		public IEnumerable<TElement> Call(IEnumerable<TElement> requirement) => requirement;
	}
}
