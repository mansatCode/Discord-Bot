using DSharpPlus;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Handlers.Dialogue.Steps
{
    //Defines what an instance of IDialogueStep must have.
    public interface IDialogueStep
    {
        Action<DiscordMessage> OnMessageAdded { get; set; }
        IDialogueStep NextStep { get; }
        //Processes whether or not the input was valid or not.
        Task<bool> ProcessStep(DiscordClient client, DiscordChannel channel, DiscordUser user);

    };
}
