using Microsoft.AspNetCore.Mvc;
using CleanArchitectureExample.Domain.Interfaces;

namespace CleanArchitectureExample.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class GreetingController : ControllerBase
{
    private readonly IGreetingService _greetingService;
    public GreetingController(IGreetingService greetingService)
    {
        _greetingService = greetingService;
    }
    [HttpGet("{name}")]
    public IActionResult GetGreeting(string name)
    {
        var greeting = _greetingService.Greet(name);
        return Ok(greeting);
    }
}