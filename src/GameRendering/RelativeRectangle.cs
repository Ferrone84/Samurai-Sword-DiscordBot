using System.Drawing;

namespace KatanaBot.GameRendering {
	public class RelativeRectangle {
		private ContentAlignment alignment;
		public ContentAlignment Alignment {get => this.alignment;}
		private readonly RelativePoint offset;
		public RelativePoint Offset {get;}
		private readonly RelativeSize size;
		public RelativeSize Size {get;}
		public RelativeRectangle(RelativePoint offset, RelativeSize size, ContentAlignment alignment=ContentAlignment.TopLeft) {
			this.offset = offset;
			this.size = size;
			this.alignment = alignment;
		}
		public RelativeRectangle((RelativePoint, RelativeSize) rect, ContentAlignment alignment=ContentAlignment.TopLeft) : this(rect.Item1, rect.Item2, alignment) {}
		public RelativeRectangle((RelativePoint, RelativeSize, ContentAlignment) rect) : this(rect.Item1, rect.Item2, rect.Item3) {}
		public static implicit operator RelativeRectangle((RelativePoint, RelativeSize) rect) => new RelativeRectangle(rect);
		public static implicit operator RelativeRectangle((RelativePoint, RelativeSize, ContentAlignment) rect) => new RelativeRectangle(rect);
	}
}
