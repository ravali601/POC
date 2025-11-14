using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextResponder.Controllers;
using TextResponder.Models;

namespace TextResponder.Tests;

public class MessageControllerTests
{
    [Fact]
    public void Returns_Ok_For_Valid_Input()
    {
        var controller = new MessageController();
        var input = new MessageInput { Text = "Hello123" };

        var result = controller.Send(input) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public void Returns_BadRequest_For_Invalid_Input()
    {
        var controller = new MessageController();

        controller.ModelState.AddModelError("Text", "Invalid");

        var result = controller.Send(new MessageInput()) as BadRequestObjectResult;

        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequest.StatusCode);
    }

    [Fact]
    public void Send_ReturnsSameValueBack()
    {
        var controller = new MessageController();
        var input = new MessageInput { Text = "Ping123" };

        var result = controller.Send(input);

        var ok = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<MessageResult>(ok.Value);

        Assert.Equal("Ping123", response.Text);
    }
}
