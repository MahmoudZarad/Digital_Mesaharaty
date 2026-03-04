using DailyReligiousMessages.Containers;
using DailyReligiousMessages.Interfaces;
using DailyReligiousMessages.Jobs;
using DailyReligiousMessages.Services;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Settings
builder.Services.Configure<TelegramSettings>(builder.Configuration.GetSection("TelegramSettings"));
builder.Services.Configure<AiSettings>(builder.Configuration.GetSection("AiSettings"));

// App Services
builder.Services.AddHttpClient<IAiService, AiService>();
builder.Services.AddScoped<ITelegramService, TelegramService>();
builder.Services.AddScoped<DailyMessageJob>();

// Hangfire
builder.Services.AddHangfire(config => config.UseInMemoryStorage());
builder.Services.AddHangfireServer();

var app = builder.Build();

// Swagger UI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DailyReligiousMessages API v1"));
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Hangfire Dashboard
app.MapHangfireDashboard("/hangfire");

// Recurring Job - كل يوم الساعة 8 صباحًا بتوقيت القاهرة
var cairoTz = TimeZoneInfo.GetSystemTimeZones()
    .FirstOrDefault(tz => tz.Id is "Egypt Standard Time" or "Africa/Cairo")
    ?? TimeZoneInfo.Utc;

RecurringJob.AddOrUpdate<DailyMessageJob>(
    "daily-ramadan-message",
    job => job.SendDailyMessageAsync(),
    "0 8 * * *",
    new RecurringJobOptions { TimeZone = cairoTz }
);

app.MapControllers();

app.Run();
