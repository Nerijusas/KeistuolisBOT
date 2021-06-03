using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace KeistuolisBot.Services
{
    public class StartupService
    {

         public static IServiceProvider _provider;
         public readonly DiscordSocketClient _discord;
         private readonly CommandService _commands;
         private readonly IConfigurationRoot _config;

        public StartupService(IServiceProvider provider, DiscordSocketClient discord, CommandService commands, IConfigurationRoot config)
        {
            _provider = provider;
            _discord = discord;
            _commands = commands;
            _config = config;
        }

        public async Task StartAsync()
        {
            string token = _config["tokens:discord"];
            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("No discord token found, put discord token in _config.yml ");
            }

            await _discord.LoginAsync(TokenType.Bot, token);
            await _discord.StartAsync();

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }



    }
}
