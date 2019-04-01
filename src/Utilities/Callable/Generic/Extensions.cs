using System.Collections.Generic;

using Callable.Generic;
using Callable.Generic.Filtering;
using Callable.Generic.Mapping;
using Callable.Generic.Rearranging;
namespace Callable.Generic {
	public static class Extensions {
		public static Source<T> ToSource<T>(this IEnumerable<T> collection) => new Source<T>(collection);
		public static Picker<T> Pick<T>(this ICallable<IEnumerable<T>> callable, int number=1) => new Picker<T>(callable, number);
		public static Flattener<T> Flatten<T>(this ICallable<IEnumerable<IEnumerable<T>>> callable) => new Flattener<T>(callable);
		public static Shuffler<T> Shuffle<T>(this ICallable<IEnumerable<T>> callable) => new Shuffler<T>(callable);
	}
}
