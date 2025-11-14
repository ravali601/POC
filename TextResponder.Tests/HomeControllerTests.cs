using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextResponder.Controllers;
using TextResponder.Models;
using TextResponder.Services.Interfaces;

namespace TextResponder.Tests;

public class HomeControllerTests
{
    [Fact]
    public async Task Post_Returns_Response_When_Input_Is_Valid()
    {
        var mockService = new Mock<IMessageService>();
        mockService.Setup(s => s.SendAsync("Hello123"))
                   .ReturnsAsync(new MessageResult { Text = "Hello123" });

        var controller = new HomeController(mockService.Object);

        var result = await controller.Index(new MessageInput { Text = "Hello123" }) as ViewResult;

        var view = Assert.IsType<ViewResult>(result);
        Assert.True(view.ViewData["ShowResponse"] is true);
        Assert.Equal("Hello123", result.ViewData["Response"]);
    }

    [Fact]
    public async Task Post_Returns_View_When_Input_Is_Invalid()
    {
        var mockService = new Mock<IMessageService>();
        var controller = new HomeController(mockService.Object);

        controller.ModelState.AddModelError("Text", "Invalid");

        var result = await controller.Index(new MessageInput { Text = "Hello 123" }) as ViewResult;

        var view = Assert.IsType<ViewResult>(result);
        Assert.False(view.ViewData["ShowResponse"] is true);
    }

    [Fact]
    public async Task Index_Post_Calls_Service_Once_When_Valid()
    {
        var mock = new Mock<IMessageService>();
        mock.Setup(m => m.SendAsync("abc")).ReturnsAsync(new MessageResult { Text = "abc" });

        var controller = new HomeController(mock.Object);

        await controller.Index(new MessageInput { Text = "abc" });

        mock.Verify(m => m.SendAsync("abc"), Times.Once);
    }

    [Fact]
    public async Task Index_Post_DoesNotCall_Service_When_Invalid()
    {
        var mock = new Mock<IMessageService>();

        var controller = new HomeController(mock.Object);
        controller.ModelState.AddModelError("Text", "Required");

        await controller.Index(new MessageInput { Text = "" });

        mock.Verify(m => m.SendAsync(It.IsAny<string>()), Times.Never);
    }

}
