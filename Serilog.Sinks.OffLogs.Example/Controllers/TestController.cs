using Microsoft.AspNetCore.Mvc;

namespace Serilog.Sinks.OffLogs.Example.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/send-error")]
    public StatusCodeResult SendError()
    {
        _logger.LogError("Hello error!");
        return Ok();
    }
    
    [HttpGet("/send-debug")]
    public StatusCodeResult SendDebug()
    {
        _logger.LogDebug("Hello debug!");
        return Ok();
    }
}