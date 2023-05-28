using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Tournament.Controllers;

[EnableCors("EnableCORS")]
[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase
{
    // protected IActionResult HandleFailure(ValidationResult result)
    // {
    //     return BadRequest(
    //         CreateProblemDetails(
    //             "Validation Error", 
    //             StatusCodes.Status400BadRequest,
    //             result.Errors)
    //     );
    // }
    //
    // private static ProblemDetails CreateProblemDetails(
    //     string title,
    //     int status,
    //     Error[] errors = null) =>
    //     new()
    //     {
    //         Title = title,
    //         Status = status,
    //         Extensions = { { nameof(errors), errors } }
    //     };
}