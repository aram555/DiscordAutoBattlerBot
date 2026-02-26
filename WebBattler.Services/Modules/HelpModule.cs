using Discord;
using Discord.Interactions;

namespace WebBattler.Services.Modules;

public class HelpModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly InteractionService _interactionService;

    public HelpModule(InteractionService interactionService)
    {
        _interactionService = interactionService;
    }

    [SlashCommand("help", "Показывает список всех доступных slash-команд")]
    public async Task HelpAsync()
    {
        var commandLines = _interactionService
            .SlashCommands
            .OrderBy(c => c.Name)
            .Select(c =>
            {
                var fullCommand = c.Module.SlashGroupName is null
                    ? $"/{c.Name}"
                    : $"/{c.Module.SlashGroupName} {c.Name}";

                return $"`{fullCommand}` — {c.Description}";
            })
            .ToList();

        if (commandLines.Count == 0)
        {
            commandLines.Add("Команды пока не зарегистрированы.");
        }

        var embed = new EmbedBuilder()
            .WithTitle("📘 Помощь по командам WebBattler")
            .WithDescription("Ниже собраны все доступные slash-команды и краткое объяснение, что они делают.")
            .WithColor(new Color(88, 101, 242))
            .WithFooter("Подсказка: начните вводить / в чате, чтобы быстро выбрать команду")
            .WithCurrentTimestamp();

        const int chunkSize = 8;

        for (var i = 0; i < commandLines.Count; i += chunkSize)
        {
            var chunk = commandLines.Skip(i).Take(chunkSize);
            var fieldTitle = i == 0 ? "🧭 Команды" : "🧭 Команды (продолжение)";

            embed.AddField(fieldTitle, string.Join("\n", chunk));
        }

        await RespondAsync(embed: embed.Build());
    }
}
