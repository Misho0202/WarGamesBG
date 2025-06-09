using FluentValidation;
using WarGamesBG.Controllers;
using WarGamesBG.Models.DTO;
using WarGamesBG.Models.Requests;

namespace WarGamesBG.Validators
{
    public class GetAllGamesByPublisherRequestValidator : AbstractValidator<Game>
    {
        public GetAllGamesByPublisherRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(100).MinimumLength(2);
            RuleFor(x => x.Year).GreaterThan(0).NotNull().WithMessage("Price must be greater than zero and should not be null.");

          
        }
    }
}
