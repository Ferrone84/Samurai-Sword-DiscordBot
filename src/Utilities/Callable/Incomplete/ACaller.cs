namespace Callable.Incomplete {
	public abstract class ACaller<TIn, TRequired> {
		protected readonly ICallable<TIn, TRequired> callable;
		protected ACaller(ICallable<TIn, TRequired> callable) {
			this.callable = callable;
		}
	}
}
