using System;
using System.Diagnostics.CodeAnalysis;

namespace RandomHelpers.API
{
    [ExcludeFromCodeCoverage]
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // More information on Log4Net logging can be found here: http://wiki/dokuwiki/doku.php?id=arch:log4net_setup

            // Set ServiceStack's LogFactory
            ServiceStack.Logging.LogManager.LogFactory = new ServiceStack.Logging.Log4Net.Log4NetFactory();

            // Set our LogFactory
            CHRobinson.Enterprise.Logging.LogManager.LogFactory = new CHRobinson.Enterprise.Logging.Log4Net.Log4NetFactory(true);

            log4net.GlobalContext.Properties["AppName"] = "RandomHelpers.API";

            log4net.Config.XmlConfigurator.Configure();

            new AppHost().Init();
        }
    }
}
