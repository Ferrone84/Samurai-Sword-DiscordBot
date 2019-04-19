namespace NumericExtensions {
	public static class Extensions {
		public static string Format(this int number, bool dashzero=false, bool enforce_plus=false) {
			return ((number == 0) && dashzero ? "-" : ((enforce_plus && (number >= 0)) ? "+" : "") + number.ToString());
		}
	}
}
