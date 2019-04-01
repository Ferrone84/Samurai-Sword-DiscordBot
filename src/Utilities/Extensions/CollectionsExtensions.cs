using System;
using System.Collections.Generic;
namespace CollectionExtensions {
	public static class Extensions {
		public static IEnumerable<TElement> Yield<TElement>(this TElement element) {
			yield return element;
			yield break;
		}
		public static IEnumerable<TElement> Filter<TElement>(this IEnumerable<TElement> collection, Func<TElement, int, bool> do_keep=null, Func<TElement, int, bool> do_break=null) {
			int i = 0;
			foreach (TElement element in collection) {
				if ((do_keep == null) || do_keep(element, i)) {yield return element;}
				if ((do_break != null) && do_break(element, i)) {yield break;}
				++i;
			}
			yield break;
		}
		public static IEnumerable<TOut> Map<TIn, TOut>(this IEnumerable<TIn> collection, Func<TIn, TOut> converter) {
			foreach (TIn element in collection) {
				yield return converter(element);
			}
			yield break;
		}
		public static IEnumerable<TElement> Flatten<TElement>(this IEnumerable<IEnumerable<TElement>> collection) {
			foreach (IEnumerable<TElement> subcollection in collection) {
				foreach (TElement element in subcollection) {yield return element;}
			}
			yield break;
		}
		public static IEnumerable<TElement> Shuffle<TElement>(this IEnumerable<TElement> collection) {
			var rnd = new Random();
			var weighed_list = new LinkedList<(TElement Element, double Rnd)>();
			foreach (TElement element in collection) {
				weighed_list.AddLast((element, rnd.NextDouble()));
			}
			var randomized = new List<(TElement Element, double Rnd)>(weighed_list);
			randomized.Sort((a, b) => a.Rnd.CompareTo(b.Rnd));
			foreach (var t in randomized) {
				yield return t.Element;
			}
			yield break;
		}
		public static IEnumerable<TElement> Pick<TElement>(this IEnumerable<TElement> collection, int min, int max=0) {
			min = Math.Max(0, min);
			max = Math.Max(min, max) + 1;
			int seed = new Random().Next(min, max);
			return collection.Filter(do_break:(e,i)=>i<seed);
		}
	}
}
