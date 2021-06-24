using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord.Addons.Hosting;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace KeistuolisBot.Services
{
    public class CommandHandler : InitializedService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _service;
        private readonly IConfiguration _config;

        public CommandHandler(IServiceProvider provider, DiscordSocketClient client, CommandService service,
            IConfiguration config)
        {
            _provider = provider;
            _client = client;
            _service = service;
            _config = config;
        }

        public override async Task InitializeAsync(CancellationToken cancellationToken)
        {
            _client.MessageReceived += OnMessageReceived;
            await _service.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }


        private async Task OnMessageReceived(SocketMessage arg)
        {
            if (!(arg is SocketUserMessage msg)) return;

            if (msg.Author.IsBot) return;

            var pos = 0;
            if (!msg.HasStringPrefix(_config["prefix"], ref pos) &&
                !msg.HasMentionPrefix(_client.CurrentUser, ref pos)) return;

            var context = new SocketCommandContext(_client, msg);
            var result = await _service.ExecuteAsync(context, pos, _provider);

            if (!result.IsSuccess)
            {
                var reason = result.Error;
                await context.Channel.SendMessageAsync($"**The following error ocurred:** \n{reason}");
                Console.Error.WriteLine(reason);
            }
        }

        private Task OnReady()
        {
            Console.WriteLine($"Connected as {_client.CurrentUser.Username}#{_client.CurrentUser.Discriminator}");
            return Task.CompletedTask;
        }
    }
}