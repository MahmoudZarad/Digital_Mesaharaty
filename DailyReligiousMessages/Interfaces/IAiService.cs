namespace DailyReligiousMessages.Interfaces;

public interface IAiService
{
    Task<string> GenerateAsync(string prompt);
}
