using System.Text;
using Discord;
using Discord.Interactions;
using WebBattler.DAL.Basis;
using WebBattler.DAL.DTO;
using WebBattler.Services.Interfaces;

namespace WebBattler.Services.Modules;

public class CountryModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly ICountryService _service;

    public CountryModule(ICountryService service)
    {
        _service = service; 
    }

    [SlashCommand("create_country", "Создание страны")]
    public async Task CreateCountryAsync(string name, string desc)
    {
        CountryDTO country = new()
        {
            OwnerId = Context.User.Id,
            Name = name,
            Description = desc,
            Provinces = new List<ProvinceDTO>()
        };

        _service.Create(country);

        await RespondAsync($"Создана страна {name}");
    }

    [SlashCommand("show_country", "Показать страну и провинциии")]
    public async Task ShowcountryAsync()
    {
        await DeferAsync();

        EmbedBuilder embed;

        var list = _service.GetAll(Context.User.Id);

        foreach (var country in list)
        {
            embed = new EmbedBuilder()
                .WithColor(Color.DarkGreen)
                .WithTitle(country.Name)
                .WithDescription(country.Description);

            foreach(var province in country.Provinces)
            {
                var sb = new StringBuilder();

                sb.Append(province.Name);
                sb.AppendLine();
                sb.AppendLine(province.Description);
                sb.AppendLine($"-Количество Городов {province.Cities.Count}");

                foreach (var city in province.Cities)
                {
                    sb.AppendLine($"--🏙 **{city.Name}** (строений: {city.Buildings.Count})");

                    foreach (var building in city.Buildings)
                    {
                        sb.AppendLine($"----🏗 {building.Name}");
                    }
                }

                embed.AddField(
                    name: $"📍 Провинция",
                    value: sb.ToString(),
                    inline: false
                );

            }

            await FollowupAsync(embed: embed.Build());
        }
    }
}
