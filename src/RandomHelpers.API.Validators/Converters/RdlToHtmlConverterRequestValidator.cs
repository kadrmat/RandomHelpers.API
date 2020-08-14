using RandomHelpers.API.ServiceModels.Messages;
using ServiceStack.FluentValidation;

namespace RandomHelpers.API.Validators.Converters
{
    public class RdlToHtmlConverterRequestValidator : AbstractValidator<RdlToHtmlConverterRequest>
    {
        public RdlToHtmlConverterRequestValidator()
        {
            RuleFor(x => x.RdlInfo).NotEmpty();
            RuleFor(x => x.RdlInfo).SetValidator(x => new RdlInfoValidator());
        }
    }
}
