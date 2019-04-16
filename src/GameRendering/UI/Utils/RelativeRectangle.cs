using System.Drawing;

namespace GameRendering.UI {
	public class RelativeRectangle {
		public RelativePoint Offset { get; }
		public RelativeSize Size { get; }
		public RelativeRectangle(RelativePoint offset, RelativeSize size) {
			this.Offset = offset;
			this.Size = size;
		}
		public RelativeRectangle((RelativePoint, RelativeSize) rect) : this(rect.Item1, rect.Item2) {}
		public static implicit operator RelativeRectangle((RelativePoint, RelativeSize) rect) => new RelativeRectangle(rect);
		public int X {get => this.Offset.X;}
		public int Y {get => this.Offset.Y;}
		public int Width {get => this.Size.Width;}
		public int Height {get => this.Size.Height;}
	}
}
