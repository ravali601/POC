using Microsoft.AspNetCore.Mvc;
using TextResponder.Models;

namespace TextResponder.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    [HttpPost("send")]
    public IActionResult Send([FromBody] MessageInput input)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(new MessageResult { Text = input.Text });
    }
}
