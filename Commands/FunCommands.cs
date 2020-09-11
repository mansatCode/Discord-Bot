using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace Discord_Bot.Commands
{
    public class FunCommands : BaseCommandModule
    {

        [Command("ping")] //The word used to trigger the command in Discord
        [Description("Returns pong")]
        public async Task Ping(CommandContext ctx)
        {
            //Referencing the discord channel the command was used in
            //"await" means the function will run this line, but it won't move on until this line is done.
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }

        [Command("add")]
        [Description("Adds two numbers together")]
        public async Task Add(CommandContext ctx, [Description("First number")] int numberOne, [Description("Second number")] int numberTwo)
        {
            await ctx.Channel.SendMessageAsync($"{numberOne} + {numberTwo} = {(numberOne + numberTwo).ToString()}").ConfigureAwait(false);
        }
    }
}
