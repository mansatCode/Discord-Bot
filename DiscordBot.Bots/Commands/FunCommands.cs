using DiscordBot.Bots.Attributes;
using DiscordBot.Bots.Handlers.Dialogue;
using DiscordBot.Bots.Handlers.Dialogue.Steps;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace DiscordBot.Bots.Commands
{
    public class FunCommands : BaseCommandModule
    {

        [Command("ping")] //The word used to trigger the command in Discord
        [Description("Returns pong")]
        [RequireCategories(ChannelCheckMode.Any, "Private Text Channels")]
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

        [Command("Dialogue")]
        public async Task Dialogue(CommandContext ctx)
        {
            var inputStep = new TextStep("Say: abc", null);
            var funnyStep = new IntStep("Haha, funny", null, 100);
            
            string input = string.Empty;
            int value = 0;

            inputStep.OnValidResult += (result) =>
            {
                input = result;
                if (result == "abc")
                {
                    inputStep.SetNextStep(funnyStep);
                }
            };

            funnyStep.OnValidResult += (result) => value = result;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);
            var inputDialogueHandler = new DialogueHandler(ctx.Client, userChannel, ctx.User, inputStep);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);
            
            if (!succeeded)
            {
                return;
            }

            await ctx.Channel.SendMessageAsync(input).ConfigureAwait(false);
            await ctx.Channel.SendMessageAsync(value.ToString()).ConfigureAwait(false);
        }

        [Command("emojiDialogue")]
        public async Task EmojiDialogue(CommandContext ctx)
        {
            var yesStep = new TextStep("You chose yes", null);
            var noStep = new TextStep("You chose no", null);

            var emojiStep = new ReactionStep("Yes or No?", new Dictionary<DiscordEmoji, ReactionStepData>
            {
                {DiscordEmoji.FromName(ctx.Client, ":thumbsup:"), new ReactionStepData { Content = "This means yes", NextStep = yesStep } },
                {DiscordEmoji.FromName(ctx.Client, ":thumbsdown:"), new ReactionStepData { Content = "This means no", NextStep = noStep } },
            });

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);
            
            var inputDialogueHandler = new DialogueHandler(
                ctx.Client,
                userChannel,
                ctx.User,
                emojiStep
            );

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);
            if (!succeeded) { return; }
        }
    }
}
