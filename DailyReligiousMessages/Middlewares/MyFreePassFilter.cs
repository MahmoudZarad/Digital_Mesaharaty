namespace DailyReligiousMessages.Middlewares;

public class MyFreePassFilter : Hangfire.Dashboard.IDashboardAuthorizationFilter
{
    public bool Authorize(Hangfire.Dashboard.DashboardContext context) => true;
}