using Microsoft.Extensions.Configuration;

namespace Automation.Assignment.Framework.Config
{
    public class ConfigReader
    {
        public static void SetFrameworkSettings()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfigurationRoot configurationRoot = builder.Build();

            Settings.AUT = configurationRoot.GetSection("testSettings").Get<TestSettings>().AUT;
            Settings.BrowserType = configurationRoot.GetSection("testSettings").Get<TestSettings>().Browser;
            Settings.Timeout = Convert.ToInt32(configurationRoot.GetSection("testSettings").Get<TestSettings>().TimeOut);
        }
    }
}
