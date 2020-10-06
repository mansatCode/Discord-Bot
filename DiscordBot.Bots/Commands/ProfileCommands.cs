using DiscordBot.Bots.Handlers.Dialogue;
using DiscordBot.Bots.Handlers.Dialogue.Steps;
using DiscordBot.Core.Services.Items;
using DiscordBot.Core.Services.Profiles;
using DiscordBot.DAL;
using DiscordBot.DAL.Models.Items;
using DiscordBot.DAL.Models.Profiles;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace DiscordBot.Bots.Commands
{
    public class ProfileCommands : BaseCommandModule
    {
        private readonly IProfileService _profileService;
        public ProfileCommands(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [Command("profile")]
        public async Task Profile(CommandContext ctx, DiscordMember member)
        {
            await GetProfileToDisplayAsync(ctx, member.Id);
        }

        private async Task GetProfileToDisplayAsync(CommandContext ctx, ulong memberId)
        {
            Profile profile = await _profileService.GetOrCreateProfileAsync(memberId, ctx.Guild.Id).ConfigureAwait(false);

            DiscordMember member = ctx.Guild.Members[profile.DiscordId];

            var profileEmbed = new DiscordEmbedBuilder
            {
                Title = $"{member.DisplayName}'s Profile",
                ThumbnailUrl = member.AvatarUrl
            };

            profileEmbed.AddField("XP", profile.Xp.ToString());
            await ctx.Channel.SendMessageAsync(embed: profileEmbed).ConfigureAwait(false);
        }
    }
}
