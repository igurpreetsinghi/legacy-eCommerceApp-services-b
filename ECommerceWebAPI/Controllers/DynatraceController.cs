using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DynatraceController : ControllerBase
    {
        private readonly ILogger<DynatraceController> _logger;

        public DynatraceController(ILogger<DynatraceController> logger)
        {
            _logger = logger;
        }

        [HttpGet("ok")]
        public IActionResult GetStatus200OK()
        {
            _logger.LogInformation("Dynatrace health check OK response generated for {TraceId}", HttpContext.TraceIdentifier);
            return Ok(new { message = "Service is reachable", traceId = HttpContext.TraceIdentifier });
        }

        [HttpGet("no-content")]
        public IActionResult GetStatus204NoContent()
        {
            _logger.LogInformation("Dynatrace no-content response generated for {TraceId}", HttpContext.TraceIdentifier);
            return NoContent();
        }

        [HttpGet("not-found")]
        public IActionResult GetStatus404NotFound()
        {
            _logger.LogWarning("Dynatrace not-found response generated for {TraceId}", HttpContext.TraceIdentifier);
            return NotFound(new { message = "The requested resource could not be located", traceId = HttpContext.TraceIdentifier });
        }

        [HttpGet("forbidden")]
        public IActionResult GetStatus403Forbidden()
        {
            _logger.LogWarning("Dynatrace forbidden response generated for {TraceId}", HttpContext.TraceIdentifier);
            return StatusCode(StatusCodes.Status403Forbidden, new { message = "You are not allowed to access this resource", traceId = HttpContext.TraceIdentifier });
        }

        [HttpGet("db-error")]
        public IActionResult GetDbErrorStatus500InternalServerError()
        {
            try
            {
                throw new InvalidOperationException("Simulated database connectivity failure");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dynatrace database error response generated for {TraceId}", HttpContext.TraceIdentifier);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Database error occurred", traceId = HttpContext.TraceIdentifier });
            }
        }

        [HttpGet("unexpected-error")]
        public IActionResult GetStatus500InternalServerError()
        {
            try
            {
                throw new Exception("Simulated unexpected server exception");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dynatrace unexpected error response generated for {TraceId}", HttpContext.TraceIdentifier);
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unexpected server error occurred", traceId = HttpContext.TraceIdentifier });
            }
        }
    }
}

