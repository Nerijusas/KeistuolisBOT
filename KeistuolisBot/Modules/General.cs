using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeistuolisBot.Modules
{
    public class General : ModuleBase
    {
        
        [Command("ping")]
        public async Task Ping()
        {
            await Context.Channel.SendMessageAsync("Pong!");
            
        }


        [Command("clear")]
        public async Task Clear(int count)
        {
            var messages = await Context.Channel.GetMessagesAsync(count + 1).FlattenAsync();
            await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

            var message = await Context.Channel.SendMessageAsync($"{messages.Count() - 1} messages deleted successfully!");
            await Task.Delay(2000);
            await message.DeleteAsync();
        }
    }
}
