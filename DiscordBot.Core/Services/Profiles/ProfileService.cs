using DiscordBot.DAL;
using DiscordBot.DAL.Models.Profiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Core.Services.Profiles
{
    public interface IProfileService
    {
        Task<Profile> GetOrCreateProfileAsync(ulong discordId, ulong guildId);
    }

    public class ProfileService : IProfileService
    {
        private readonly RPGContext _context;

        public ProfileService(RPGContext context)
        {
            _context = context;
        }

        public async Task<Profile> GetOrCreateProfileAsync(ulong discordId, ulong guildId)
        {
            var profile = await _context.Profiles
                .Where(x => x.GuildId == guildId)
                .FirstOrDefaultAsync(x => x.DiscordId == discordId).ConfigureAwait(false);
        
            if (profile != null) { return profile; }

            profile = new Profile
            {
                DiscordId = discordId,
                GuildId = guildId
            };

            _context.Add(profile);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return profile;
        }
    }
}
