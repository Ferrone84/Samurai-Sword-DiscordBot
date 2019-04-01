namespace Callable.Generic {
	public interface ICallable<out TOut> {
		TOut Call();
	}
}
