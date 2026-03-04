using DailyReligiousMessages.Containers;
using DailyReligiousMessages.Interfaces;
using Microsoft.Extensions.Options;

namespace DailyReligiousMessages.Services;

public class TelegramService(IOptions<TelegramSettings> options) : ITelegramService
{
    public async Task SendMessageAsync(string message)
    {
        using var client = new HttpClient();
        var url = $"https://api.telegram.org/bot{options.Value.BotToken}/sendMessage";

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("chat_id", options.Value.ChatId),
            new KeyValuePair<string, string>("text", message)
        });

        await client.PostAsync(url, content);
    }
}