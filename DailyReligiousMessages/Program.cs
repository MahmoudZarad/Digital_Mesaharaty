using DailyReligiousMessages.Containers;
using DailyReligiousMessages.Interfaces;
using DailyReligiousMessages.Jobs;
using DailyReligiousMessages.Middlewares;
using DailyReligiousMessages.Services;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration bindings
builder.Services.Configure<TelegramSettings>(builder.Configuration.GetSection("TelegramSettings"));
builder.Services.Configure<AiSettings>(builder.Configuration.GetSection("AiSettings"));

// App services
builder.Services.AddHttpClient<IAiService, AiService>();
builder.Services.AddScoped<ITelegramService, TelegramService>();
builder.Services.AddScoped<DailyMessageJob>();

// Hangfire
builder.Services.AddHangfire(config => config.UseInMemoryStorage());
builder.Services.AddHangfireServer();

var app = builder.Build();

// Dev only: Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseMiddleware<BasicAuthMiddleware>();

// Basic auth middleware protects the Hangfire dashboard. Credentials are read from environment or config.
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{ Authorization = new[] { new MyFreePassFilter() } });

// Recurring job: everyday at 08:00 (Cairo timezone if available)
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