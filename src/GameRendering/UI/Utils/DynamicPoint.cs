using System;
using System.Drawing;
namespace GameRendering.UI {
	public class DynamicPoint {
		public Func<int, int> X { get; }
		public Func<int, int> Y { get; }
		public ContentAlignment Alignment { get; }
		public DynamicPoint(Func<int, int> x, Func<int, int> y, ContentAlignment alignment=ContentAlignment.TopLeft) {
			this.X = x;
			this.Y = y;
			this.Alignment = alignment;
		}
		public DynamicPoint(int x, int y, ContentAlignment alignment=ContentAlignment.TopLeft) : this(i=>x, i=>y, alignment) {}
		public DynamicPoint((Func<int, int>, Func<int, int>) dynamic_coordinates, ContentAlignment alignment=ContentAlignment.TopLeft) :
			this(dynamic_coordinates.Item1, dynamic_coordinates.Item2, alignment) {}
		public Point Point(int i) => new Point(this.X(i), this.Y(i));
		public static implicit operator DynamicPoint((Func<int, int>, Func<int, int>) dynamic_coordinates) =>
			new DynamicPoint(dynamic_coordinates);
	}
}
