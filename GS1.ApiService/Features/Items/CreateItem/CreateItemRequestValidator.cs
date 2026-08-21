using FluentValidation;

namespace GS1.ApiService.Features.Items.CreateItem;

public class CreateItemRequestValidator : AbstractValidator<CreateItemRequest>
{
    public CreateItemRequestValidator()
    {
        RuleFor(i => i.Gtin)
            .NotEmpty()
            .Matches("^[A-Z0-9]{4}[0-9]{10}$");

        RuleFor(i => i.Name)
            .NotEmpty()
            .MaximumLength(255);
    }
}
