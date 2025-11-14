using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextResponder.Models;

namespace TextResponder.Tests;

public class ModelValidationTests
{
    [Fact]
    public void MessageInput_Fails_When_Empty()
    {
        var model = new MessageInput { Text = "" };
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var valid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(valid);
    }

    [Fact]
    public void MessageInput_Fails_When_SpecialCharactersUsed()
    {
        var model = new MessageInput { Text = "hello @$" };
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var valid = Validator.TryValidateObject(model, context, results, true);

        Assert.False(valid);
    }

    [Fact]
    public void MessageInput_Passes_When_Alphanumeric()
    {
        var model = new MessageInput { Text = "abc123" };
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();

        var valid = Validator.TryValidateObject(model, context, results, true);

        Assert.True(valid);
    }
}
