using FluentValidation;

namespace GS1.ApiService.Features.Items.UpdateItem;

public class UpdateItemRequestValidator : AbstractValidator<UpdateItemRequest>
{
    public UpdateItemRequestValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(i => i.VersionId)
            .NotEmpty();
    }
}
