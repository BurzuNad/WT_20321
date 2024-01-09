using VT_20321.Middleware;

namespace VT_20321.Extensions
{
    public static class AppExtensions {
        public static IApplicationBuilder UseFileLogging(this IApplicationBuilder app) => app.UseMiddleware<LogMiddleware>();
    }
}
