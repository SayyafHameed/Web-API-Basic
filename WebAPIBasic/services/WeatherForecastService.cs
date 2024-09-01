//namespace WebAPIBasic.services
//{
//    public class WeatherForecastService
//    {
//        private static readonly string[] Summaries = new[]
//        {
//            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//        };
//        private readonly ILogger<WeatherForecastService> _logger;

//        public WeatherForecastService(ILogger<WeatherForecastService> logger)
//        {
//            _logger = logger;
//        }
//        public IEnumerable<WeatherForecast> GetForecast()
//        {
//            _logger.LogInformation("Getting Information Data");
//            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
//            {
//                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                TemperatureC = Random.Shared.Next(-20, 55),
//                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
//            })
//            .ToArray();
//        }
//    }
//}
