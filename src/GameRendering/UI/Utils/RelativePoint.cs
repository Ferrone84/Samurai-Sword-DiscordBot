using System.Drawing;

namespace GameRendering.UI {
	public class RelativePoint {
		public int X { get; }
		public int Y { get; }
		public ContentAlignment Alignment { get; }
		public RelativePoint(int x, int y, ContentAlignment alignment=ContentAlignment.TopLeft) {
			this.X = x;
			this.Y = y;
			this.Alignment = alignment;
		}
		public RelativePoint((int, int) rp) : this(rp.Item1, rp.Item2) {}
		public RelativePoint((int, int, ContentAlignment) rp) : this(rp.Item1, rp.Item2, rp.Item3) {}
		public static implicit operator RelativePoint((int, int) rp) => new RelativePoint(rp);
		public static implicit operator RelativePoint((int, int, ContentAlignment) rp) => new RelativePoint(rp);
	}
}
