using static Automation.Assignment.Framework.Base.Browser;

namespace Automation.Assignment.Framework.Config
{
    public class Settings
    {
        public static int Timeout { get; set; }
        public static string AUT { get; set; }
        public static BrowserType BrowserType { get; set; }
    }
}
