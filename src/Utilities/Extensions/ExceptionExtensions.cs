
using System;

namespace KatanaBot
{
	public static class ExceptionExtentions
	{
		public static void DisplayException(this Exception e, string message = "Error")
		{
			("\n" + message + " : (" + e.GetType().Name + ") \n" + e.Message + "\n").Println();
			("[StackTrace]\n" + e.StackTrace + "\n").Println();
		}
	}
}
