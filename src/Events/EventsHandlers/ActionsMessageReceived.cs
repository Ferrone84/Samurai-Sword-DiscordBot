using System.Threading.Tasks;
using Discord.WebSocket;
using Events.EventsHandling;
using KatanaBot;

namespace Events.EventsHandlers
{
	public class ActionsMessageReceived : IGuildMessageReceivedEventHandler
	{
		public async Task Guild_Message_Received(SocketUserMessage message)
		{
			if (message.Channel.Id != 559060763864596495) { return; } //tests-channel

			try {
				await message.Channel.SendMessageAsync("Bonsouar.");
			}
			catch (System.Exception e) {
				e.Display("ActionsMessageReceived");
			}
		}
	}
}
