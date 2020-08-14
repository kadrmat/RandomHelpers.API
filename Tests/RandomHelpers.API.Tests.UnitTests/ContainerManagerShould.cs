using FluentAssertions;
using Funq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomHelpers.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomHelpers.API.Tests.UnitTests
{
    [TestClass]
    public class ContainerManagerShould
    {
        /// <summary>
        /// Verifies all interfaces defined in the Interfaces assembly are registered in the Container.
        /// </summary>
        [TestMethod]
        public void Initialize_AllScenarios_AllInterfacesInTheInterfacesAssemblyRegistered()
        {
            try
            {
                // Arrange
                Container container = new Container();
                Type interfacesAssemblyInfoType = typeof(InterfacesAssemblyInfo);
                System.Reflection.Assembly interfacesAssembly = interfacesAssemblyInfoType.Assembly;

                Type[] interfacesThatDoNotNeedToBeRegistered = new[]
                {
                    typeof(IExampleInterface)
                };

                //var expectedGenericInterfaces = new[]
                //{
                //    typeof(IExampleGenericInterface<ExampleType>)
                //};

                IEnumerable<Type> expectedInterfaces = interfacesAssembly
                    .GetTypes()
                    .Where(type =>
                        type.IsInterface &&
                        !type.IsGenericType)
                    .Except(interfacesThatDoNotNeedToBeRegistered);

                //  .Union(expectedGenericInterfaces);

                // Act
                ContainerManager.Register(container);

                // Assert
                foreach (Type expectedInterface in expectedInterfaces)
                {
                    object actualInterface = container.TryResolve(expectedInterface);

                    actualInterface.Should().NotBeNull("because {0} should be registered with the container", expectedInterface.Name);
                }
            }
            // When reading in the types via reflection weird errors can happen so catch these exceptions and 
            // return an exception with message containing the underlying error
            catch (System.Reflection.ReflectionTypeLoadException ex)
            {
                // Also return the loader exception messages as those are the ones that tell you the actual problem
                List<string> exceptionMessages = new List<string> { ex.Message };
                IEnumerable<string> loaderExceptionMessages = ex.LoaderExceptions.Select(loaderException => loaderException.Message);
                exceptionMessages.AddRange(loaderExceptionMessages);

                string consolidatedExceptionMessage = string.Join("|", exceptionMessages);

                throw new Exception(consolidatedExceptionMessage, ex);
            }
        }
    }
}
