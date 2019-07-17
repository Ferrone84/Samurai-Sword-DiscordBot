using System;

namespace NumericExtensions {
	public static class Extensions {
		public static string Format(this int number, bool dashzero=false, bool enforce_plus=false) {
			return ((number == 0) && dashzero ? "-" : ((enforce_plus && (number >= 0)) ? "+" : "") + number.ToString());
		}
		public static void Times(this int number, Action action) {
			for (int i = 0 ; i < number ; ++i) { action( ); }
		}
		public static void Times(this int number, Action<int> action) {
			for (int i = 0 ; i < number ; ++i) { action(i); }
		}
	}
}
