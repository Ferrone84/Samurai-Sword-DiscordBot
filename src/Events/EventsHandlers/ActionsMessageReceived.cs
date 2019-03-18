using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Discord.WebSocket;
using KatanaBot.Data;

namespace KatanaBot.Events.EventsHandlers
{
	public class ActionsMessageReceived : IGuildMessageReceivedEventHandler, IDMMessageReceivedEventHandler
	{
		public async Task DM_Message_Received(SocketUserMessage message)
		{
			await ActionsHandler(message);
		}

		public async Task Guild_Message_Received(SocketUserMessage message)
		{
			await ActionsHandler(message);
		}

		private async Task ActionsHandler(SocketUserMessage message)
		{
			try {
				await message.Channel.SendMessageAsync("Bonsouar.");
			}
			catch (System.Exception e) {
				e.DisplayException("ActionsMessageReceived");
			}
		}
	}
}