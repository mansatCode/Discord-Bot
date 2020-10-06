using DiscordBot.Bots.Handlers.Dialogue;
using DiscordBot.Bots.Handlers.Dialogue.Steps;
using DiscordBot.Core.Services.Items;
using DiscordBot.DAL;
using DiscordBot.DAL.Models.Items;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Bots.Commands
{
    public class ItemCommands : BaseCommandModule
    {
        private readonly IItemService _itemService;
        public ItemCommands(IItemService itemService)
        {
            _itemService = itemService;
        }

        [Command("createitem")]
        [RequireRoles(RoleCheckMode.Any, "Pokimane")]
        public async Task CreateItem(CommandContext ctx)
        {
            var itemDescriptionStep = new TextStep("Enter item description:", null);
            var itemNameStep = new TextStep("Enter item name:", itemDescriptionStep);

            var item = new Item();

            itemNameStep.OnValidResult += (result) => item.Name = result;
            itemDescriptionStep.OnValidResult += (result) => item.Description = result;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false); //ctx.Channel;
            var inputDialogueHandler = new DialogueHandler(ctx.Client, userChannel, ctx.User, itemNameStep);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            await _itemService.CreateNewItemAsync(item);
            await ctx.Channel.SendMessageAsync($"Item {item.Name} successfully created!").ConfigureAwait(false);

        }


        [Command("iteminfo")]

        public async Task ItemInfo(CommandContext ctx)
        {
            var itemNameStep = new TextStep("What item are you looking for?", null);

            string itemName = string.Empty;

            itemNameStep.OnValidResult += (result) => itemName = result;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

            var inputDialogueHandler = new DialogueHandler(ctx.Client, userChannel, ctx.User, itemNameStep);

            bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

            if (!succeeded) { return; }

            var item = await _itemService.GetItemByName(itemName).ConfigureAwait(false);

            if (item == null)
            {
                await ctx.Channel.SendMessageAsync($"There is no item called {itemName}");
                return;
            }

            await ctx.Channel.SendMessageAsync($"Name: {item.Name} Description: {item.Description}");
        }
    }
}
