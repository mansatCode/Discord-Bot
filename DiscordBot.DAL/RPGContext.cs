﻿using DiscordBot.DAL.Models.Items;
using DiscordBot.DAL.Models.Profiles;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.DAL
{
    public class RPGContext : DbContext
    {
        public RPGContext(DbContextOptions<RPGContext> options) : base(options) { }
        
        public DbSet<Profile> Profiles { get; set; }
        
        public DbSet<Item> Items { get; set; }
    }
}
