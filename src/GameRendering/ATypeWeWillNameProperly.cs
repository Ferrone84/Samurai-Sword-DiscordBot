namespace KatanaBot.GameRendering {	
	public class ATypeWeWillNameProperly : RelativeRectangle {
		private readonly string[] types;
		public string[] Types {get;}
		private readonly DynamicPoint dynamic_offset;
		public DynamicPoint DynamicOffset {get;}
		private readonly RelativeRectangle value;
		public RelativeRectangle Value;
		public ATypeWeWillNameProperly(string[] types, RelativePoint offset, RelativeSize size, DynamicPoint dynamic_offset, RelativePoint value_offset, RelativeSize value_size) : base(offset, size) {
			this.types = types;
			this.dynamic_offset = dynamic_offset;
			this.value = new RelativeRectangle(value_offset, value_size);
		}
		public ATypeWeWillNameProperly(string[] types, RelativeRectangle rect, DynamicPoint dynamic_offset, RelativeRectangle valuerect) :
			this(types, rect.Offset, rect.Size, dynamic_offset, valuerect.Offset, valuerect.Size) {}
	}
}
