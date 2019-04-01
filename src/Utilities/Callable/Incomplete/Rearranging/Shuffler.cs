using System;
using System.Collections.Generic;

using CollectionExtensions;
namespace Callable.Incomplete.Rearranging {
	public class Shuffler<TElement, TRequired> : ACaller<IEnumerable<TElement>, TRequired>,  ICallable<IEnumerable<TElement>, TRequired> {
		public Shuffler(ICallable<IEnumerable<TElement>, TRequired> callable) : base(callable) {}
		public IEnumerable<TElement> Call(TRequired requirement) => this.callable.Call(requirement).Shuffle();
	}
}
