
using System.Diagnostics;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private Stopwatch _stopwatch;
        private readonly ILogger<RequestTimeMiddleware> _logger;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _stopwatch = new Stopwatch();
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);

            _stopwatch.Stop();

            var result = _stopwatch.ElapsedMilliseconds;

            if (result/1000 > 4 ) 
            {
                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {result} ms";

                _logger.LogInformation(message);
            }
        }
    }
}
