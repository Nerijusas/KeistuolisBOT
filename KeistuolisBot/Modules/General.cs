using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using KeistuolisBot.Common;

namespace KeistuolisBot.Modules
{

    public class General : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync("Pong!");
        }


        [Command("clear")]
        public async Task Clear(int count)
        {
            var messages = await Context.Channel.GetMessagesAsync(count + 1).FlattenAsync();
            await (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

            var message =
                await Context.Channel.SendMessageAsync($"{messages.Count() - 1} messages deleted successfully!");
            await Task.Delay(2000);
            await message.DeleteAsync();
        }


        [Command("test")]
        public async Task Test()
        {
            WeirdEmbedBuilder builder = new WeirdEmbedBuilder();
            await ReplyAsync(embed: builder.Build());
        }
    }
}