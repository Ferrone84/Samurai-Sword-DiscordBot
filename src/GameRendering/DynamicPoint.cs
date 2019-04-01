using System;
using System.Drawing;
namespace KatanaBot.GameRendering {
	public class DynamicPoint {
		private readonly Func<int, int> x;
		public Func<int, int> X {get => this.x;}
		private readonly Func<int, int> y;
		public Func<int, int> Y {get => this.y;}
		private ContentAlignment alignment;
		public ContentAlignment Alignment {get => this.alignment;}
		public DynamicPoint(Func<int, int> x, Func<int, int> y, ContentAlignment alignment=ContentAlignment.TopLeft) {
			this.x = x;
			this.y = y;
			this.alignment = alignment;
		}
		public DynamicPoint(int x, int y, ContentAlignment alignment=ContentAlignment.TopLeft) : this(i=>x, i=>y, alignment) {}
		public DynamicPoint((Func<int, int>, Func<int, int>) dynamic_coordinates, ContentAlignment alignment=ContentAlignment.TopLeft) :
			this(dynamic_coordinates.Item1, dynamic_coordinates.Item2, alignment) {}
		public static implicit operator DynamicPoint((Func<int, int>, Func<int, int>) dynamic_coordinates) =>
			new DynamicPoint(dynamic_coordinates);
	}
}
