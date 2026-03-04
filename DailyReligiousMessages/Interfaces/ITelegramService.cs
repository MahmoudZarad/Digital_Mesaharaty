namespace DailyReligiousMessages.Interfaces;

public interface ITelegramService
{
    Task SendMessageAsync(string message);
}
