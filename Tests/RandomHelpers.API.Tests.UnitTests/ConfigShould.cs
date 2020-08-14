using Microsoft.VisualStudio.TestTools.UnitTesting;
using RandomHelpers.API.Tests.UnitTests.Helpers;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;

namespace RandomHelpers.API.Tests.UnitTests
{
    [TestClass]
    public class ConfigShould
    {
        private const string RandomHelpersClassNameAppConfigPath = "..\\..\\..\\..\\src\\RandomHelpers.API\\Web.config";
        private const string RandomHelpersClassNameTokenPath = "..\\..\\..\\..\\src\\RandomHelpers.API\\Web.config.token";
        private const string AcceptanceTestAppConfigPath = "..\\..\\..\\RandomHelpers.API.Tests.AcceptanceTests\\App.config";
        private const string AcceptanceTestTokenPath = "..\\..\\..\\RandomHelpers.API.Tests.AcceptanceTests\\App.config.token";

        [TestMethod]
        public void AppSettingsInPluginConfig_Keys_MatchTokenFile()
        {
            List<string> appSettingsKeys;
            using (AppConfig.Change(RandomHelpersClassNameAppConfigPath))
            {
                // tempFileName is used for the app config during this context
                appSettingsKeys = ConfigurationManager.AppSettings.AllKeys.Select(key => key).ToList();
            }

            List<string> appSettingsTokenKeys;
            List<string> connectionStringTokenNames = new List<string>();
            using (AppConfig.Change(RandomHelpersClassNameTokenPath))
            {
                // tempFileName is used for the app config during this context
                appSettingsTokenKeys = ConfigurationManager.AppSettings.AllKeys.Select(key => key).ToList();
            }

            foreach (string key in appSettingsKeys)
            {
                Assert.IsTrue(appSettingsTokenKeys.Any(k => k == key), $"Key:{key} not found in AppSettings section of token file.");
            }

            foreach (string key in appSettingsTokenKeys)
            {
                Assert.IsTrue(appSettingsKeys.Any(k => k == key), $"Key:{key} not found in AppSettings section of config file.");
            }
        }

        [TestMethod]
        public void AppSettingsInAATConfig_Keys_MatchTokenFile()
        {
            List<string> keys;
            using (AppConfig.Change(AcceptanceTestAppConfigPath))
            {
                // tempFileName is used for the app config during this context
                keys = ConfigurationManager.AppSettings.AllKeys.Select(key => key).ToList();
            }

            List<string> tokenKeys;
            using (AppConfig.Change(AcceptanceTestTokenPath))
            {
                // tempFileName is used for the app config during this context
                tokenKeys = ConfigurationManager.AppSettings.AllKeys.Select(key => key).ToList();
            }

            foreach (string key in keys)
            {
                Assert.IsTrue(tokenKeys.Any(k => k == key), $"Key:{key} not found in AppSettings section of AAT token file.");
            }

            foreach (string key in tokenKeys)
            {
                Assert.IsTrue(keys.Any(k => k == key), $"Key:{key} not found in AppSettings section of AAT config file.");
            }
        }

        [TestMethod]
        public void AssemblyRedirectsInWebConfig_MatchWebConfigToken()
        {
            XDocument webConfigXmlDoc = XDocument.Load(RandomHelpersClassNameAppConfigPath);
            XDocument webConfigTokenXmlDoc = XDocument.Load(RandomHelpersClassNameTokenPath);

            XElement webConfigXmlAssemblyBindingElement = webConfigXmlDoc.Descendants().FirstOrDefault(element => element.Name.LocalName == "assemblyBinding");
            XElement webConfigTokenXmlAssemblyBindingElement = webConfigTokenXmlDoc.Descendants().FirstOrDefault(element => element.Name.LocalName == "assemblyBinding");

            Assert.AreEqual(webConfigXmlAssemblyBindingElement?.ToString(), webConfigTokenXmlAssemblyBindingElement?.ToString());
        }

        [TestMethod]
        public void ConnectionStringsInPluginConfig_Names_MatchTokenFile()
        {
            List<string> connectionStringNames = new List<string>();
            using (AppConfig.Change(RandomHelpersClassNameAppConfigPath))
            {
                // tempFileName is used for the app config during this context
                foreach (ConnectionStringSettings connectionStringSetting in ConfigurationManager.ConnectionStrings)
                {
                    if ((connectionStringSetting.ElementInformation.Source != null)
                        && (connectionStringSetting.ElementInformation.Source.EndsWith(RandomHelpersClassNameAppConfigPath)))
                    {
                        connectionStringNames.Add(connectionStringSetting.Name);
                    }
                }
            }

            List<string> connectionStringTokenNames = new List<string>();
            using (AppConfig.Change(RandomHelpersClassNameTokenPath))
            {
                // tempFileName is used for the app config during this context
                foreach (ConnectionStringSettings connectionStringSetting in ConfigurationManager.ConnectionStrings)
                {
                    if ((connectionStringSetting.ElementInformation.Source != null)
                        && (connectionStringSetting.ElementInformation.Source.EndsWith(RandomHelpersClassNameTokenPath)))
                    {
                        connectionStringTokenNames.Add(connectionStringSetting.Name);
                    }
                }
            }

            foreach (string name in connectionStringNames)
            {
                Assert.IsTrue(connectionStringTokenNames.Any(n => n == name), $"Name:{name} not found in ConnectionStrings section of token config file.");
            }

            foreach (string name in connectionStringTokenNames)
            {
                Assert.IsTrue(connectionStringNames.Any(n => n == name), $"Name:{name} not found in ConnectionStrings section of config file.");
            }
        }

        [TestMethod]
        public void ConnectionStringsInAATConfig_Names_MatchTokenFile()
        {
            List<string> connectionStringNames = new List<string>();
            using (AppConfig.Change(AcceptanceTestAppConfigPath))
            {
                // tempFileName is used for the app config during this context
                foreach (ConnectionStringSettings connectionStringSetting in ConfigurationManager.ConnectionStrings)
                {
                    if ((connectionStringSetting.ElementInformation.Source != null)
                        && (connectionStringSetting.ElementInformation.Source.EndsWith(AcceptanceTestAppConfigPath)))
                    {
                        connectionStringNames.Add(connectionStringSetting.Name);
                    }
                }
            }

            List<string> connectionStringTokenNames = new List<string>();
            using (AppConfig.Change(AcceptanceTestTokenPath))
            {
                // tempFileName is used for the app config during this context
                foreach (ConnectionStringSettings connectionStringSetting in ConfigurationManager.ConnectionStrings)
                {
                    if ((connectionStringSetting.ElementInformation.Source != null)
                        && (connectionStringSetting.ElementInformation.Source.EndsWith(AcceptanceTestTokenPath)))
                    {
                        connectionStringTokenNames.Add(connectionStringSetting.Name);
                    }
                }
            }

            foreach (string name in connectionStringNames)
            {
                Assert.IsTrue(connectionStringTokenNames.Any(n => n == name), $"Name:{name} not found in ConnectionStrings section of token config file.");
            }

            foreach (string name in connectionStringTokenNames)
            {
                Assert.IsTrue(connectionStringNames.Any(n => n == name), $"Name:{name} not found in ConnectionStrings section of config file.");
            }
        }
    }
}