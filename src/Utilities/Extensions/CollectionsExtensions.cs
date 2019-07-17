using System;
using System.Collections;
using System.Collections.Generic;
namespace CollectionExtensions {
	public static class Extensions {
		public static bool IsEmpty(this IEnumerable collection) {
			foreach (var element in collection) { return false;}
			return true;
		}
		public static IEnumerable<TElement> Yield<TElement>(this TElement element) {
			yield return element;
			yield break;
		}
		public static IEnumerable<TElement> Filtered<TElement>(this IEnumerable<TElement> collection, Func<TElement, int, bool> do_keep=null, Func<TElement, int, bool> do_break=null) {
			int i = 0;
			foreach (TElement element in collection) {
				if ((do_keep == null) || do_keep(element, i)) {yield return element;}
				if ((do_break != null) && do_break(element, i)) {yield break;}
				++i;
			}
			yield break;
		}
		public static IEnumerable<TOut> Mapped<TIn, TOut>(this IEnumerable<TIn> collection, Func<TIn, TOut> converter) {
			foreach (TIn element in collection) {
				yield return converter(element);
			}
			yield break;
		}
		public static IEnumerable<TElement> Flattened<TElement>(this IEnumerable<IEnumerable<TElement>> collection) {
			foreach (IEnumerable<TElement> subcollection in collection) {
				foreach (TElement element in subcollection) {yield return element;}
			}
			yield break;
		}
		public static IEnumerable<TElement> Shuffled<TElement>(this IEnumerable<TElement> collection) {
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
		public static IEnumerable<TElement> Picked<TElement>(this IEnumerable<TElement> collection, int min, int max=0) {
			min = Math.Max(0, min);
			max = Math.Max(min, max) + 1;
			int seed = new Random().Next(min, max);
			return collection.Filtered(do_break:(e,i)=>i<seed);
		}
		public static TElement Shift<TElement>(this IList<TElement> list) {
			TElement element = list[0];
			list.RemoveAt(0);
			return element;
		}
		public static bool TryShift<TElement>(this IList<TElement> list, out TElement element) {
			bool success = (list.Count == 0);
			element = (success ? list[0] : default(TElement));
			if (success) { list.RemoveAt(0); }
			return success;
		}
		public static TElement Pop<TElement>(this IList<TElement> list) {
			TElement element = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
			return element;
		}
		public static bool TryPop<TElement>(this IList<TElement> list, out TElement element) {
			bool success = (list.Count == 0);
			element = (success ? list[list.Count-1] : default(TElement));
			if (success) { list.RemoveAt(list.Count - 1); }
			return success;
		}
		public static TElement Shift<TElement>(this LinkedList<TElement> list) {
			TElement element = list.First.Value;
			list.RemoveFirst( );
			return element;
		}
		public static bool TryShift<TElement>(this LinkedList<TElement> list, out TElement element) {
			bool success = (list.Count == 0);
			element = (success ? list.First.Value : default(TElement));
			if (success) { list.RemoveFirst( ); }
			return success;
		}
		public static TElement Pop<TElement>(this LinkedList<TElement> list) {
			TElement element = list.Last.Value;
			list.RemoveLast( );
			return element;
		}
		public static bool TryPop<TElement>(this LinkedList<TElement> list, out TElement element) {
			bool success = (list.Count == 0);
			element = (success ? list.Last.Value : default(TElement));
			if (success) { list.RemoveLast( ); }
			return success;
		}
	}
}
