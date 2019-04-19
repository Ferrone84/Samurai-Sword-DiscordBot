using System.Drawing;

namespace GameRendering.UI {
	public struct RelativeSize {
		public int Width { get; }
		public int Height { get; }
		public ContentAlignment Alignment { get; }
		public RelativeSize(int width, int height, ContentAlignment alignment=ContentAlignment.TopLeft) {
			this.Width = width;
			this.Height = height;
			this.Alignment = alignment;
		}
		public RelativeSize((int, int) rs) : this(rs.Item1, rs.Item2) {}
		public RelativeSize((int, int, ContentAlignment) rs) : this(rs.Item1, rs.Item2, rs.Item3) {}
		public static implicit operator RelativeSize((int, int) rs) => new RelativeSize(rs);
		public static implicit operator RelativeSize((int, int, ContentAlignment) rs) => new RelativeSize(rs);
	}
}
