using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using TweetBook.Contracts.V1.Request;

namespace TweetBook.Validators;

public class CreatePostRequestValidator : AbstractValidator<CreateTagRequest>
{
    public CreatePostRequestValidator()
    {
        RuleForEach(x => x.Tags)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9 ]*$");
    }
}