using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace WebAPIBasic.Filters
{
    public class LogActivityFilter : IAsyncActionFilter
    {
        private readonly ILogger<LogActivityFilter> _logger;

        public LogActivityFilter(ILogger<LogActivityFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} execute on controller {context.Controller} with argument {JsonSerializer.Serialize(context.ActionArguments)}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} finished execution on controller {context.Controller}");
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation($"(Async) Executing action {context.ActionDescriptor.DisplayName} execute on controller {context.Controller} with argument {JsonSerializer.Serialize(context.ActionArguments)}");
            await next();
            _logger.LogInformation($"(Async) Action {context.ActionDescriptor.DisplayName} finished execution on controller {context.Controller}");

        }
    }
}
