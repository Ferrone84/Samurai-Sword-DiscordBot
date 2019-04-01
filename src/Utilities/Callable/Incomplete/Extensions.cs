using System.Collections.Generic;

using Callable.Incomplete;
using Callable.Incomplete.Filtering;
// using Callable.Incomplete.Mapping;
using Callable.Incomplete.Rearranging;
namespace Callable.Incomplete.Extensions {
	public static class Extensions {
		public static Source<T> ToIncompleteSource<T>(this IEnumerable<T> collection) => new Source<T>();
		public static Picker<TProduced, TRequired> Pick<TProduced, TRequired>(this ICallable<IEnumerable<TProduced>, TRequired> callable, int number=1)
			=> new Picker<TProduced, TRequired>(callable, number);
		public static Flattener<TProduced, TRequired> Flatten<TProduced, TRequired>(this ICallable<IEnumerable<IEnumerable<TProduced>>, TRequired> callable)
			=> new Flattener<TProduced, TRequired>(callable);
		public static Shuffler<TProduced, TRequired> Shuffle<TProduced, TRequired>(this ICallable<IEnumerable<TProduced>, TRequired> callable)
			=> new Shuffler<TProduced, TRequired>(callable);
	}
}
