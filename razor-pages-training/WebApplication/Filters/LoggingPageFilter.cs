using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Filters
{
    public class LoggingPageFilter : IAsyncPageFilter
    {
        private readonly ILogger<LoggingPageFilter> _logger;

        public LoggingPageFilter(ILogger<LoggingPageFilter> logger)
        {
            _logger = logger;
        }

        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var message = $"After Handler : page = {context.RouteData.Values["page"]}";
            _logger.LogInformation(message);
            await next.Invoke();
        }

        public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            var message = $"Before Handler : page = {context.RouteData.Values["page"]}";
            _logger.LogInformation(message);
            return Task.CompletedTask;
        }
    }
}
