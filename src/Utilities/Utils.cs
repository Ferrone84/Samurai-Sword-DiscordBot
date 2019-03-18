
using System;
using System.Collections.Generic;
using System.IO;

using KatanaBot.Data;

namespace KatanaBot
{
	public class Utils
	{
		public static string Token => File.ReadAllLines(DataManager.Text.TOKEN_FILE)[0];
	}
}
