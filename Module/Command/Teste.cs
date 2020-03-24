using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harpya.Module.Command
{
    public class Teste : ModuleBase<SocketCommandContext>
    {
        [Command("ping")]
        public async Task ping()
        {
            await ReplyAsync("+8000");
        }
    }
}
