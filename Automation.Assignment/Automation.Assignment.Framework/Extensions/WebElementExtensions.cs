using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Automation.Assignment.Framework.Extensions
{
    public static class WebElementExtensions
    {
        public static void MouseHover(this IWebElement element, IWebDriver driver )
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Perform();
        }

        public static void JClick(this IWebElement element, IWebDriver driver)
        {
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].click()", element);
        }
    }
}
