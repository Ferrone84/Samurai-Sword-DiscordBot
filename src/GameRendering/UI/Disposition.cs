using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Drawing;

namespace GameRendering.UI {
	public class FixedDisposition : GameRendering.UI.FixedElement {
		private readonly string[] order;
		private readonly ConcurrentDictionary<string, GameRendering.UI.IFixedElement> elements;
		public FixedDisposition(RelativeRectangle origin,
				string[] order, IDictionary<string, GameRendering.UI.IFixedElement> elements) : base(origin) {
			this.order = order;
			this.elements = new ConcurrentDictionary<string, IFixedElement>(elements);
		}
		protected override void OnRender(Graphics graphics, Rectangle origin) {
			foreach (string element_id in this.order) {
				var element = this.elements.GetValueOrDefault(element_id, null);
				element?.Render(graphics, origin);
			}
		}
	}
	public class DynamicDisposition<TReceived, TUsed, TEmitted> : GameRendering.UI.DynamicElement<TReceived, TUsed> {
		private readonly Func<TUsed, string, TEmitted> selector;
		private readonly string[] order;
		private readonly ConcurrentDictionary<string, GameRendering.UI.IElement> elements;
		public DynamicDisposition(RelativeRectangle origin, Func<TReceived, TUsed> converter, Func<TUsed, string, TEmitted> selector,
				string[] order, IDictionary<string, GameRendering.UI.IElement> elements) : base(origin, converter) {
			this.selector = selector;
			this.order = order;
			this.elements = new ConcurrentDictionary<string, IElement>(elements);
		}
		protected override void OnRender(Graphics graphics, Rectangle origin, TUsed data) {
			foreach (string element_id in this.order) {
				var element = this.elements.GetValueOrDefault(element_id, null);
				if (element == null) {}
				else if (element is GameRendering.UI.IFixedElement fixed_element) {fixed_element.Render(graphics, origin);}
				else if (element is GameRendering.UI.IDynamicElement<TEmitted> dynamic_element) {dynamic_element.Render(graphics, origin, this.selector(data, element_id));}
			}
		}
	}
	public class DynamicDisposition<TReceived, TUsed> : DynamicDisposition<TReceived, TUsed, TUsed> {
		public DynamicDisposition(RelativeRectangle origin, Func<TReceived, TUsed> converter,
				string[] order, IDictionary<string, GameRendering.UI.IElement> elements) : base(origin, converter, (_item, _id) => _item, order, elements) {}
	}
	public class DynamicDisposition<TReceived> : DynamicDisposition<TReceived, TReceived> {
		public DynamicDisposition(RelativeRectangle origin,
				string[] order, IDictionary<string, GameRendering.UI.IElement> elements) : base(origin, (_item) => _item, order, elements) {}
	}
}
