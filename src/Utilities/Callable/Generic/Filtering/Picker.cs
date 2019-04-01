using System;
using System.Collections.Generic;

using CollectionExtensions;
namespace Callable.Generic.Filtering {
	public class Picker<TElement> : ACaller<IEnumerable<TElement>>, ICallable<IEnumerable<TElement>> {
		private readonly int min, max;
		public Picker(ICallable<IEnumerable<TElement>> callable, int min, int max=0) : base(callable) {
			this.min = min;
			this.max = max;
		}
		public IEnumerable<TElement> Call() => this.callable.Call().Pick(min, max);
	}
}
