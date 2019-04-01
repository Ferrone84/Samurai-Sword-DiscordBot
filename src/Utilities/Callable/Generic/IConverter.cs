namespace Callable.Generic {
	public interface IConverter<TIn, TOut> {
		TOut Call(TIn element);
	}
}
