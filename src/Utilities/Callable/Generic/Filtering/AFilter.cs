using System.Collections.Generic;
namespace Callable.Generic.Filtering {
	public abstract class AFilter<TElement, TCallArgument> : ACaller<IEnumerable<TElement>>, ICallable<IEnumerable<TElement>> {
		protected AFilter(ICallable<IEnumerable<TElement>> callable) : base(callable) {}
		public IEnumerable<TElement> Call() {
			TCallArgument call_argument = this.GenerateCallArgument();
			int i = 0;
			foreach (TElement element in this.callable.Call()) {
				if (this.DoKeep(element, i, call_argument)) {yield return element;}
				if (this.DoBreak(element, i, call_argument)) {yield break;}
				++i;
			}
			yield break;
		}
		protected abstract TCallArgument GenerateCallArgument();
		protected abstract bool DoKeep(TElement element, int index, TCallArgument data);
		protected abstract bool DoBreak(TElement element, int index, TCallArgument data);
	}
}
