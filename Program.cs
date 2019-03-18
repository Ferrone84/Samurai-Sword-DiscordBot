namespace KatanaBot
{
	public class Program
	{
		public static void Main(string[] args)
			=> new KatanaBot().MainAsync().GetAwaiter().GetResult();
	}
}
