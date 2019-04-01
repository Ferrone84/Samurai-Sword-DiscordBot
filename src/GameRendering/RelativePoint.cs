using System.Drawing;

namespace KatanaBot.GameRendering {
	public class RelativePoint {
		private int x;
		public int X {get => this.x;}
		private int y;
		public int Y {get => this.y;}
		private ContentAlignment alignment;
		public ContentAlignment Alignment {get => this.alignment;}
		public RelativePoint(int x, int y, ContentAlignment alignment=ContentAlignment.TopLeft) {
			this.x = x;
			this.y = y;
			this.alignment = alignment;
		}
		public RelativePoint((int, int) rp) : this(rp.Item1, rp.Item2) {}
		public RelativePoint((int, int, ContentAlignment) rp) : this(rp.Item1, rp.Item2, rp.Item3) {}
		public static implicit operator RelativePoint((int, int) rp) => new RelativePoint(rp);
		public static implicit operator RelativePoint((int, int, ContentAlignment) rp) => new RelativePoint(rp);
	}
}
