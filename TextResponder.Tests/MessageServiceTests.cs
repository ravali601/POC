using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TextResponder.Controllers;
using TextResponder.Models;
using TextResponder.Services;
using TextResponder.Services.Interfaces;

namespace TextResponder.Tests;

public class MessageServiceTests
{
    [Fact]
    public async Task SendAsync_Returns_Expected_Result()
    {
        // Arrange
        var handler = new Mock<HttpMessageHandler>();

        handler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(new MessageResult { Text = "Test" })
            });

        var client = new HttpClient(handler.Object)
        {
            BaseAddress = new Uri("http://localhost/")
        };

        var service = new MessageService(client);

        // Act
        var result = await service.SendAsync("Test");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test", result!.Text);
    }

    [Fact]
    public async Task SendAsync_Throws_When_ApiReturnsError()
    {
        var handler = new Mock<HttpMessageHandler>();

        handler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest
            });

        var client = new HttpClient(handler.Object)
        {
            BaseAddress = new Uri("http://localhost/")
        };

        var service = new MessageService(client);

        await Assert.ThrowsAsync<HttpRequestException>(() => service.SendAsync("abc"));
    }
}
