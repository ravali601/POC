using TextResponder.Models;
using TextResponder.Services.Interfaces;

namespace TextResponder.Services;
public class MessageService(HttpClient http) : IMessageService
{
    private readonly HttpClient _http = http;

    public async Task<MessageResult?> SendAsync(string text)
    {
        var response = await _http.PostAsJsonAsync("api/message/send",
            new MessageInput { Text = text });

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<MessageResult>();
    }
}
