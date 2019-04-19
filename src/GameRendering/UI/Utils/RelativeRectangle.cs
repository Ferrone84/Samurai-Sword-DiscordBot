using System.Drawing;

namespace GameRendering.UI {
	public struct RelativeRectangle {
		public RelativePoint Offset { get; }
		public RelativeSize Size { get; }
		public RelativeRectangle(RelativePoint offset, RelativeSize size) {
			this.Offset = offset;
			this.Size = size;
		}
		public RelativeRectangle((RelativePoint Offset, RelativeSize Size) rect) : this(rect.Offset, rect.Size) {}
		public static implicit operator RelativeRectangle((RelativePoint Offset, RelativeSize Size) rect) => new RelativeRectangle(rect);
		public int X {get => this.Offset.X;}
		public int Y {get => this.Offset.Y;}
		public int Width {get => this.Size.Width;}
		public int Height {get => this.Size.Height;}
	}
}
