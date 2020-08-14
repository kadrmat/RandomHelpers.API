using System.Reflection;

namespace RandomHelpers.API.ServiceDefinitions
{
    public static class ServiceDefinitionInfo
    {
        public static Assembly Assembly => typeof(ServiceDefinitionInfo).Assembly;

        public static readonly string Name = "ServiceDefinitionInfo";
    }
}
