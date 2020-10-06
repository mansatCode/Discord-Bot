using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordBot.DAL.Models.Profiles
{
    public class Profile : Entity  
    {
        public ulong DiscordId { get; set; }
        public ulong GuildId { get; set; }
        public int Xp { get; set; }
        public int Level => Xp / 100;
    }
}
