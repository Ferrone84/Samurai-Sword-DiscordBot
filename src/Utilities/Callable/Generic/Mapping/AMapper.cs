using System.Collections.Generic;
namespace Callable.Generic.Mapping {
	public abstract class AMapper<TIn, TOut> : ACaller<IEnumerable<TIn>>, ICallable<IEnumerable<TOut>> {
		public AMapper(ICallable<IEnumerable<TIn>> callable) : base(callable) {}
		public IEnumerable<TOut> Call() {
			foreach (TIn element in this.callable.Call()) {
				yield return this.Convert(element);
			}
			yield break;
		}
		protected abstract TOut Convert(TIn element);
	}
}
