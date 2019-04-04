using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Drawing;

namespace GameRendering.UI {
	public class FixedSeries : GameRendering.UI.FixedElement {
		private readonly string[] order;
		private readonly ConcurrentDictionary<string, GameRendering.UI.IFixedElement> children;
		private readonly DynamicPoint dynamic_offset;
		public FixedSeries(RelativeRectangle origin,
				string[] order, IDictionary<string, GameRendering.UI.IFixedElement> children,
				DynamicPoint dynamic_offset) : base(origin) {
			this.order = order;
			this.children = new ConcurrentDictionary<string, IFixedElement>(children);
			this.dynamic_offset = dynamic_offset;
		}
		protected override void OnRender(Graphics graphics, Rectangle origin) {
			int i = 0;
			foreach (string child_id in order) {
				var point = dynamic_offset.Point(i);
				int x = point.X + origin.X;
				int y = point.X + origin.X;
				var shifted_origin = new Rectangle(x, y, origin.Width, origin.Height);
				var child = children.GetValueOrDefault(child_id, null);
				child?.Render(graphics, shifted_origin);
				++i;
			}
		}
	}
	public class DynamicSeries<TReceived, TUsed, TEmitted> : GameRendering.UI.DynamicElement<TReceived, TUsed> {
		private readonly Func<TUsed, int, TEmitted> selector;
		private readonly string[] order;
		private readonly ConcurrentDictionary<string, GameRendering.UI.IElement> children;
		private readonly DynamicPoint dynamic_offset;
		public DynamicSeries(RelativeRectangle origin, Func<TReceived, TUsed> converter, Func<TUsed, int, TEmitted> selector,
				string[] order, IDictionary<string, GameRendering.UI.IElement> children,
				DynamicPoint dynamic_offset) : base(origin, converter) {
			this.selector = selector;
			this.order = order;
			this.children = new ConcurrentDictionary<string, IElement>(children);
			this.dynamic_offset = dynamic_offset;
		}
		protected override void OnRender(Graphics graphics, Rectangle origin, TUsed data) {
			int i = 0;
			foreach (string child_id in order) {
				var point = dynamic_offset.Point(i);
				int x = point.X + origin.X;
				int y = point.X + origin.X;
				var shifted_origin = new Rectangle(x, y, origin.Width, origin.Height);
				var child = children.GetValueOrDefault(child_id, null);
				if (child == null) {}
				else if (child is GameRendering.UI.IFixedElement fixed_child) {fixed_child.Render(graphics, shifted_origin);}
				else if (child is GameRendering.UI.IDynamicElement<TEmitted> dynamic_child) {dynamic_child.Render(graphics, shifted_origin, this.selector(data, i));}
				++i;
			}
		}
	}
}
