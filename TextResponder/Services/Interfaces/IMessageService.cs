using TextResponder.Models;

namespace TextResponder.Services.Interfaces;

public interface IMessageService
{
    Task<MessageResult?> SendAsync(string text);
}
