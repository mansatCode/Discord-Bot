using DiscordBot.DAL.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.DAL
{
    public class RPGContext : DbContext
    {
        public RPGContext(DbContextOptions<RPGContext> options) : base(options) { }

        //To add new tables to the database, add more DbSets
        public DbSet<Item> Items { get; set; }
    }
}
