using Funq;
using RandomHelpers.API.Repositories;
using RandomHelpers.API.Template.Interfaces.Managers;
using RandomHelpers.API.Template.Interfaces.Repositories;
using RandomHelpers.API.Template.Managers;
using RandomHelpers.API.Validators;
using ServiceStack.Validation;

namespace RandomHelpers.API
{
    public static class ContainerManager
    {
        public static void Register(Container container)
        {
            // add service validation
            container.RegisterValidators(ReuseScope.Container, typeof(ValidationInfo).Assembly);

            // Managers
            container.RegisterAs<RdlToHtmlConversionManager, IRdlToHtmlConversionManager>().ReusedWithin(ReuseScope.None);

            // Repositories
            container.RegisterAs<FileSaveRepository, IFileSaveRepository>().ReusedWithin(ReuseScope.None);
        }
    }
}
