namespace WebAPIBasic.Middlewares
{
    public class ReateLimitingMiddleware
    {
        private readonly RequestDelegate _next;

        private static int _countered = 0;
        private static DateTime _lastRequestDate = DateTime.Now;
        public ReateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            _countered++;
            if (DateTime.Now.Subtract(_lastRequestDate).Seconds > 10)
            {
                _countered = 1;
                _lastRequestDate = DateTime.Now;
                await _next(context);
            }
            else
            {
                if (_countered > 5)
                {
                    _lastRequestDate = DateTime.Now;
                    await context.Response.WriteAsync("Rate Limit Exceeded");
                }
                else
                {
                    _lastRequestDate = DateTime.Now;
                    await _next(context);
                }
            }
        }
    }
}
