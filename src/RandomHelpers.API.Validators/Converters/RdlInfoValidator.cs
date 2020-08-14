using RandomHelpers.API.ServiceModels.Types;
using ServiceStack.FluentValidation;

namespace RandomHelpers.API.Validators.Converters
{
    public class RdlInfoValidator : AbstractValidator<RdlInfo>
    {
        public RdlInfoValidator()
        {
            RuleFor(x => x.RdlFolderPath).NotEmpty();
        }
    }
}
