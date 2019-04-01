using System;
using System.Collections.Generic;

using CollectionExtensions;
namespace Callable.Incomplete.Filtering {
	public class Picker<TElement, TRequired> : ACaller<IEnumerable<TElement>, TRequired>, ICallable<IEnumerable<TElement>, TRequired> {
		private readonly int min, max;
		public Picker(ICallable<IEnumerable<TElement>, TRequired> callable, int min, int max=0) : base(callable) {
			this.min = min;
			this.max = max;
		}
		public IEnumerable<TElement> Call(TRequired requirement) => this.callable.Call(requirement).Pick(min, max);
	}
}
