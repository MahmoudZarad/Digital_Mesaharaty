using DailyReligiousMessages.Containers;
using DailyReligiousMessages.Interfaces;
using Microsoft.Extensions.Options;

namespace DailyReligiousMessages.Jobs;

public class DailyMessageJob(
    IAiService aiService,
    ITelegramService telegramService,
    IOptions<AiSettings> aiSettings)
{
    private static readonly string[] Topics =
    [
        "الصيام وأسراره الروحية",
        "قيام الليل والتهجد",
        "الإخلاص في العبادة",
        "تلاوة القرآن الكريم والتدبر",
        "الصدقة وفضل الإنفاق",
        "التوبة والاستغفار",
        "حسن الخلق مع الناس",
        "الصبر والشكر",
        "ذكر الله وفضله",
        "صلة الرحم والبر",
        "الدعاء وآداب المسألة",
        "الإحسان في العمل والعبادة",
        "محاسبة النفس",
        "الزهد في الدنيا",
        "التفكر في خلق الله",
        "فضل العشر الأواخر",
        "ليلة القدر والاجتهاد فيها",
        "الاعتكاف وتصفية النفس",
        "الرجاء والخوف من الله",
        "الشكر على نعمة الإسلام"
    ];

    public async Task SendDailyMessageAsync()
    {
        var today = DateTime.Now;
        var topic = Topics[today.DayOfYear % Topics.Length];

        var fullPrompt = $"{aiSettings.Value.RamadanPrompt}\n\nتاريخ اليوم: {today:yyyy-MM-dd}\nموضوع اليوم: {topic}";

        var message = await aiService.GenerateAsync(fullPrompt);
        await telegramService.SendMessageAsync(message);
    }
}
