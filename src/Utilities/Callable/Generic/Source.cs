using System.Collections.Generic;
namespace Callable.Generic {
	public class Source<TElement> : ICallable<IEnumerable<TElement>> {
		private IEnumerable<TElement> source;
		public Source(IEnumerable<TElement> source) {
			this.source = source;
		}
		public IEnumerable<TElement> Call() => this.source;
	}
}
