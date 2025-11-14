using System.ComponentModel.DataAnnotations;

namespace TextResponder.Models;
public class MessageInput
{
    [Required(ErrorMessage = "Please enter some text.")]
    [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Use letters and numbers only.")]
    public string Text { get; set; } = string.Empty;
}
