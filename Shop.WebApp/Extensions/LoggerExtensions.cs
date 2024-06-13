using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Shop.Shared.Entities;
using ILogger=Serilog.ILogger;

namespace Shop.WebApp.Extensions
{
    public static class LoggerExtensions
    {
        public static ILogger WithClassAndMethodNames<T>(
            this ILogger logger,
            [CallerMemberName] string memberName = ""
        )
        {
            var className = typeof(T).Name;
            return logger
            .ForContext("ClassName", className)
            .ForContext("MethodName", memberName);
        }
    }
}
