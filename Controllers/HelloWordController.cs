using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HelloWordController : ControllerBase

{
    private readonly ILogger<HelloWordController> _logger;
    IHelloWordServices _helloWordServices;

    public HelloWordController(IHelloWordServices helloWordServices, ILogger<HelloWordController> logger)
    {
        _helloWordServices = helloWordServices;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Hello Word en la terminal");
        return Ok(_helloWordServices.GetHelloWord());
    }

}