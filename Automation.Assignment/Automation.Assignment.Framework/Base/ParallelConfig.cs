using AventStack.ExtentReports;
using OpenQA.Selenium;

namespace Automation.Assignment.Framework.Base
{
    public class ParallelConfig
    {
        public IWebDriver Driver { get; set; }
        public BasePage CurrentPage { get; set; }
        public MediaEntityModelProvider CaptureScreenshotAndReturnModel(string Name)
        {
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, Name).Build();
        }
    }
}

