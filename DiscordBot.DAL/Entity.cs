using System.ComponentModel.DataAnnotations;

namespace DiscordBot.DAL
{
    public abstract class Entity //"abstract" it will only be inherited.
    {
        [Key]
        public int Id { get; set; }
    }
}
