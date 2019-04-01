namespace Callable.Incomplete {
	public interface ICallable<out TProduced, in TRequired> {
		TProduced Call(TRequired requirement);
	}
}
