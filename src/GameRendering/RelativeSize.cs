using System.Drawing;

namespace KatanaBot.GameRendering {
	public class RelativeSize {
		private int width;
		public int Width {get => this.width;}
		private int height;
		public int Height {get => this.height;}
		private ContentAlignment alignment;
		public ContentAlignment Alignment {get => this.alignment;}
		public RelativeSize(int width, int height, ContentAlignment alignment=ContentAlignment.TopLeft) {
			this.width = width;
			this.height = height;
			this.alignment = alignment;
		}
		public RelativeSize((int, int) rs) : this(rs.Item1, rs.Item2) {}
		public RelativeSize((int, int, ContentAlignment) rs) : this(rs.Item1, rs.Item2, rs.Item3) {}
		public static implicit operator RelativeSize((int, int) rs) => new RelativeSize(rs);
		public static implicit operator RelativeSize((int, int, ContentAlignment) rs) => new RelativeSize(rs);
	}
}
