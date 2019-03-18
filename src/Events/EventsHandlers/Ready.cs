using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Discord;

namespace KatanaBot.Events.EventsHandlers
{
	public class Ready : ISelfReadyEventHandler
	{
		public /*async*/ Task Self_Ready()
		{
			("Ready.").Println();
			return Task.CompletedTask;
		}
	}
}
