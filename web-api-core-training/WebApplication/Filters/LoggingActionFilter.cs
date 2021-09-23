using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Filters
{
    public class LoggingActionFilter : IAsyncActionFilter
    {
        private readonly ILogger<LoggingActionFilter> _logger;

        public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var message = $"Execution : action = {context.RouteData.Values["action"]} controller = {context.RouteData.Values["controller"]}";
            _logger.LogInformation(message);

            await next();
        }
    }
}
