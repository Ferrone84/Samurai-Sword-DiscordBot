namespace Callable.Generic {
	public abstract class ACaller<TIn> {
		protected readonly ICallable<TIn> callable;
		protected ACaller(ICallable<TIn> callable) {
			this.callable = callable;
		}
	}
}
