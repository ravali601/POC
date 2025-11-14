using Microsoft.AspNetCore.Mvc;
using TextResponder.Models;
using TextResponder.Services.Interfaces;

namespace TextResponder.Controllers;

public class HomeController(IMessageService messages) : Controller
{
    private readonly IMessageService _messages = messages;

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.ShowResponse = false;
        return View(new MessageInput());
    }

    [HttpPost]
    public async Task<IActionResult> Index(MessageInput input)
    {
        if (!ModelState.IsValid)
        {
            // Clear old response when validation fails
            ViewBag.ShowResponse = false;
            return View(input);
        }

        var result = await _messages.SendAsync(input.Text);

        ViewBag.Response = result?.Text;
        ViewBag.ShowResponse = true;

        return View(input);
    }
}