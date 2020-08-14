using CHR.ServiceStack.Plugins;
using Funq;
using RandomHelpers.API.ServiceDefinitions;
using ServiceStack;
using ServiceStack.Api.Swagger;
using ServiceStack.Text;
using ServiceStack.Validation;

namespace RandomHelpers.API
{
    public class AppHost : AppHostBase
    {
        /// <summary>
        /// Default constructor.
        /// Base constructor requires a name and assembly to locate web service classes. 
        /// </summary>
        public AppHost() : base(ServiceDefinitionInfo.Name, ServiceDefinitionInfo.Assembly) { }

        /// <summary>
        /// Application specific configuration
        /// This method should initialize any IoC resources utilized by your web service classes.
        /// </summary>
        /// <param name="container"></param>
        public override void Configure(Container container)
        {
            JsConfig.EmitCamelCaseNames = true;
            JsConfig.DateHandler = DateHandler.ISO8601;

            ContainerManager.Register(container);

            InitializePlugins(container);

            //wiki for enterprise monitoring logging sdk 
            //https://github.chrobinson.com/CHR/EnterpriseMonitoring.Logging/wiki

            //wiki for enterprise monitoring servicestack extensions
            //https://github.chrobinson.com/CHR/EnterpriseMonitoring.Logging/wiki/ServiceStack
            //wire in correlation for adding header, IRequest container for capturing routing, and expection capturing for service & validation errors for the enterprise monitoring logging sdk.
            this.AddCorrelationLogging().AddServiceExceptionLogging().AddIRequestToContainer();


            //For capturing KPI check out
            //https://github.chrobinson.com/CHR/EnterpriseMonitoring.Logging/wiki/.NET-Full-Framework#kpi-designation
        }


        private void InitializePlugins(Container container)
        {
            Plugins.Add(new ValidationFeature());
            Plugins.Add(new PostmanFeature());
            Plugins.Add(new SwaggerFeature());
            Plugins.Add(new HealthCheckFeature(container));
        }

    }
}
