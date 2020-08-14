using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Text;

namespace RandomHelpers.API.Tests.UnitTests
{
    [TestClass]
    public class AppHostShould
    {
        [TestMethod]
        public void Configure_AllScenarios_JsonConfigured()
        {
            //Arrange
            AppHost appHost = new AppHost();
            Funq.Container funqContainer = new Funq.Container();

            //Act
            appHost.Configure(funqContainer);

            //Assert
            Assert.IsTrue(JsConfig.EmitCamelCaseNames);
            Assert.AreEqual(DateHandler.ISO8601, JsConfig.DateHandler);
        }
    }
}
