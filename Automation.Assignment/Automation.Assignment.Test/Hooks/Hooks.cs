using Automation.Assignment.Framework.Base;
using Automation.Assignment.Framework.Config;
using Automation.Assignment.Test.Pages;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Automation.Assignment.Test.Hooks
{
    [Binding]
    public class Hooks : InitializeTest
    {
        public Hooks(ParallelConfig parallelConfig) : base(parallelConfig)
        {
            _parallelConfig = parallelConfig ?? throw new ArgumentNullException("scenarioContext");
        }

        [ThreadStatic]
        static ExtentTest feature;
        static string reportPath = Directory.GetParent(@"../../../").FullName
            + Path.DirectorySeparatorChar + "Result"
            + Path.DirectorySeparatorChar;

        static ExtentReports extent;
        ExtentTest scenario;
        private readonly ParallelConfig _parallelConfig;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            //Initilize setting before suite run
            ConfigReader.SetFrameworkSettings();
            //setup report
            ExtentHtmlReporter htmlReport = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AddSystemInfo("Execution Environment", Settings.AUT);
            extent.AddSystemInfo("Execution Browser", Settings.BrowserType.ToString());
            extent.AddSystemInfo("Author", "Pankaj Kumar[naukri.pankaj1@gmail.com]");
            extent.AttachReporter(htmlReport);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext context)
        {
            feature = extent.CreateTest(context.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void TestSetup(ScenarioContext context)
        {
            InitializeSettings();
            scenario = feature.CreateNode(context.ScenarioInfo.Title);
            _parallelConfig.Driver.Navigate().GoToUrl(Settings.AUT);
            _parallelConfig.CurrentPage = new HomePage(_parallelConfig);
        }

        [AfterStep]
        public void InsertReportingSteps(ScenarioContext context)
        {
            if (context.TestError == null)
            {
                scenario.Log(Status.Pass, context.StepContext.StepInfo.Text);
            }
            else if (context.TestError != null)
            {
                var mediaEntity = _parallelConfig.CaptureScreenshotAndReturnModel(context.ScenarioInfo.Title.Trim());
                scenario.Log(Status.Fail, context.StepContext.StepInfo.Text);
                scenario.Log(Status.Fail, context.TestError.Message, mediaEntity);
            }
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            extent.Flush();
        }

        [AfterScenario]
        public void TestStop()
        {
            _parallelConfig.Driver.Quit();
        }
    }
}
