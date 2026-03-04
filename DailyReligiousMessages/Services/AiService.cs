using DailyReligiousMessages.Containers;
using DailyReligiousMessages.Interfaces;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace DailyReligiousMessages.Services;

public class AiService(IOptions<AiSettings> options, HttpClient httpClient) : IAiService
{
    public async Task<string> GenerateAsync(string prompt)
    {
        var request = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };

        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
        );

        var response = await httpClient.PostAsync(
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={options.Value.ApiKey}",
            content
        );

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);

        return doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString() ?? string.Empty;
    }
}