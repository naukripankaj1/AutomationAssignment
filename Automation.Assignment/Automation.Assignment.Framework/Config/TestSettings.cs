using Newtonsoft.Json;
using static Automation.Assignment.Framework.Base.Browser;

namespace Automation.Assignment.Framework.Config
{
    [JsonObject("testSettings")]
    internal class TestSettings
    {
        [JsonProperty("aut")]
        public string AUT { get; set; }      

        [JsonProperty("browser")]
        public BrowserType Browser { get; set; }

        [JsonProperty("timeOut")]
        public string TimeOut { get; set; }
    }
}
