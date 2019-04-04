using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameRendering.UI {
	public class Disposition<TReceived, TUsed> : GameRendering.UI.DynamicElement<TReceived, TUsed> {
		private readonly string[] order;
		private readonly Dictionary<string, GameRendering.UI.IElement> subviews;
		public Disposition(RelativeRectangle origin, Func<TReceived, TUsed> converter) : base(origin, converter) {}
		protected override void OnRender(Graphics graphics, Rectangle origin, TUsed data) {
			
		}
	}
}
