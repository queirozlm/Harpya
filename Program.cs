using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Harpya
{
    class Program
    {

        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient client;
        private CommandService command;
        private IServiceProvider services;

        public async Task RunBotAsync()
        {
            client = new DiscordSocketClient();
            command = new CommandService();
            services = new ServiceCollection().AddSingleton(client).AddSingleton(command).BuildServiceProvider();

            string Key = "NjkxODE4NDQ1NjU4MTI4NDc0.Xnlgqg.TvVagaIdUUmQztyUGoNKcO9uJoE";

            // Eventos recebidos pelo bot.

            client.Ready += Client_Ready;
            client.Log += Client_Log;
            //client.UserJoined += userJoinned();


            await Client_Ready();
            await Command_Bot();

            client.LoginAsync(TokenType.Bot, Key);

            await client.StartAsync();
            await Task.Delay(-1);
        }

        private async Task StartAsync()
        {

        }
        //private async Task userJoinned(SocketGuildUser user)
        //{
        //    var _user = user.Guild;
        //}

        private async Task Client_Ready()
        {
            await client.SetGameAsync("Programming", "http://VisualStudio", ActivityType.Playing);
        }
        public async Task Command_Bot()
        {
            client.MessageReceived += initialCommand;
            await command.AddModulesAsync(Assembly.GetEntryAssembly(), services);
        }

        private async Task initialCommand(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message is null || message.Author.IsBot) return;

            var Context = new SocketCommandContext(client, message);

            int argPost = 0;
            if (message.HasStringPrefix("§", ref argPost))
            {
                var result = await command.ExecuteAsync(Context, argPost, services);

                if (!result.IsSuccess)
                {
                    Console.WriteLine(result.ErrorReason);
                }
            }
        }

        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }
    }
}

