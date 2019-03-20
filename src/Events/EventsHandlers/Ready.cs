using System.Threading.Tasks;
using Events.EventsHandling;

using KatanaBot;

namespace Events.EventsHandlers
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
