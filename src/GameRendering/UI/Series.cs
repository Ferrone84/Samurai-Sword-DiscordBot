using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Drawing;

namespace GameRendering.UI {
	public class FixedSeries : GameRendering.UI.FixedElement {
		private readonly string[] order;
		private readonly ConcurrentDictionary<string, GameRendering.UI.IFixedElement> elements;
		private readonly DynamicPoint dynamic_offset;
		public FixedSeries(RelativeRectangle origin,
				string[] order, IDictionary<string, GameRendering.UI.IFixedElement> elements,
				DynamicPoint dynamic_offset) : base(origin) {
			this.order = order;
			this.elements = new ConcurrentDictionary<string, IFixedElement>(elements);
			this.dynamic_offset = dynamic_offset;
		}
		protected override void OnRender(Graphics graphics, Rectangle origin) {
			int i = 0;
			foreach (string element_id in order) {
				var point = dynamic_offset.Point(i);
				int x = point.X + origin.X;
				int y = point.X + origin.X;
				var shifted_origin = new Rectangle(x, y, origin.Width, origin.Height);
				var element = elements.GetValueOrDefault(element_id, null);
				element?.Render(graphics, shifted_origin);
				++i;
			}
		}
	}
	public class DynamicSeries<TReceived, TUsed, TEmitted> : GameRendering.UI.DynamicElement<TReceived, TUsed> {
		private readonly Func<TUsed, int, string, TEmitted> selector;
		private readonly string[] order;
		private readonly ConcurrentDictionary<string, GameRendering.UI.IElement> elements;
		private readonly DynamicPoint dynamic_offset;
		public DynamicSeries(RelativeRectangle origin, Func<TReceived, TUsed> converter, Func<TUsed, int, string, TEmitted> selector,
				string[] order, IDictionary<string, GameRendering.UI.IElement> elements,
				DynamicPoint dynamic_offset) : base(origin, converter) {
			this.selector = selector;
			this.order = order;
			this.elements = new ConcurrentDictionary<string, IElement>(elements);
			this.dynamic_offset = dynamic_offset;
		}
		protected override void OnRender(Graphics graphics, Rectangle origin, TUsed data) {
			int i = 0;
			foreach (string element_id in this.order) {
				var point = dynamic_offset.Point(i);
				int x = point.X + origin.X;
				int y = point.X + origin.X;
				var shifted_origin = new Rectangle(x, y, origin.Width, origin.Height);
				var element = this.elements.GetValueOrDefault(element_id, null);
				if (element == null) {}
				else if (element is GameRendering.UI.IFixedElement fixed_element) {fixed_element.Render(graphics, shifted_origin);}
				else if (element is GameRendering.UI.IDynamicElement<TEmitted> dynamic_element) {dynamic_element.Render(graphics, shifted_origin, this.selector(data, i, element_id));}
				++i;
			}
		}
	}
}
