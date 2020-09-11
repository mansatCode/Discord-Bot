using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
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

        [Command("respondMessage")]
        public async Task respondMessage(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            //"x" is the message. The message must be from the channel that the command was used in.
            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(message.Result.Content);
        }

        [Command("respondReaction")]
        public async Task respondReaction(CommandContext ctx)
        {
            var interactivity = ctx.Client.GetInteractivity();
            
            var message = await interactivity.WaitForReactionAsync(x => x.Channel == ctx.Channel && x.User == ctx.User).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(message.Result.Emoji);
        }

        [Command("Morgz")]
        public async Task morgz(CommandContext ctx)
        {
            //Read roasts from a website
            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData("https://insult.mattbas.org/api/insult");
            string webData = System.Text.Encoding.UTF8.GetString(raw);

            //Post roast in Discord
            await ctx.Channel.SendMessageAsync($"Morgz is {webData.Substring(8)}.");
        }
    }
}
