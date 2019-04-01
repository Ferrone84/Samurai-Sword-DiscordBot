using System;
using System.Collections.Generic;

using CollectionExtensions;
namespace Callable.Generic.Rearranging {
	public class Shuffler<TElement> : ACaller<IEnumerable<TElement>>,  ICallable<IEnumerable<TElement>> {
		public Shuffler(ICallable<IEnumerable<TElement>> callable) : base(callable) {}
		public IEnumerable<TElement> Call() => this.callable.Call().Shuffle();
	}
}
