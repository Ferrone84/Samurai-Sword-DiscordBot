namespace Games {
	public interface IEffect<in TSubject> {
		void Apply(TSubject subject);
	}
}