using Automation.Assignment.Framework.Config;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using static Automation.Assignment.Framework.Base.Browser;

namespace Automation.Assignment.Framework.Base
{
    public class InitializeTest
    {
        private readonly ParallelConfig _parallelConfig;

        public InitializeTest(ParallelConfig parallelConfig)
        {
            _parallelConfig = parallelConfig;
        }

        public void InitializeSettings()
        {
            //Set all the settings for framework
            ConfigReader.SetFrameworkSettings();
            //Open Browser
            OpenBrowser(Settings.BrowserType);
        }

        private void OpenBrowser(BrowserType browserType = BrowserType.Chrome)
        {
            switch (browserType)
            {
                case BrowserType.Chrome:
                    ChromeOptions copt = new ChromeOptions();
                    copt.AddArguments("--ignore-certificate-errors");
                    copt.AddArguments("--incognito");
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                    _parallelConfig.Driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), copt, TimeSpan.FromSeconds(Settings.Timeout));
                    _parallelConfig.Driver.Manage().Window.Maximize();
                    break;
                case BrowserType.Edge:
                    EdgeOptions eopt = new EdgeOptions();
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);
                    _parallelConfig.Driver = new EdgeDriver(EdgeDriverService.CreateDefaultService(), eopt, TimeSpan.FromSeconds(Settings.Timeout));
                    _parallelConfig.Driver.Manage().Window.Maximize();
                    break;
            }
        }
    }
}
