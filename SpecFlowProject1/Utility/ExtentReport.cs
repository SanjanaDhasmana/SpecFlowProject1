using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;

namespace SpecFlowProject1.Utility
{
    public class ExtentReport
    {
        public static ExtentReports _extentReports;
        public static ExtentTest _feature;
        public static ExtentTest _scenario;
        public static ExtentSparkReporter sparkReporter;

        public static String dir = AppDomain.CurrentDomain.BaseDirectory;
        public static String testResultPath = dir.Replace("bin\\Debug\\net6.0", "TestResults");

        public static void ExtentReportInit()
        {
            sparkReporter = new ExtentSparkReporter(@"D:\VisualStudio\SpecFlowProject1\SpecFlowProject1\TestResults\index.html");
            
            sparkReporter.Config.ReportName = "Auomation Status Report";
            sparkReporter.Config.DocumentTitle = "Auomation Status Report";
            sparkReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Standard;
            

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(sparkReporter);
            _extentReports.AddSystemInfo("Application", "YouTube");
            _extentReports.AddSystemInfo("OS", "Windows");
            _extentReports.AddSystemInfo("Browser", "Chrome");
        }

        public static void ExtentReportTearDown()
        {
            _extentReports.Flush();
        }

        public string addScreenShot(IWebDriver driver, ScenarioContext scenario)
        {
            ITakesScreenshot takeScreenshot = (ITakesScreenshot)driver;
            Screenshot screenshot = takeScreenshot.GetScreenshot();
            string screenshotLocation = Path.Combine(testResultPath, scenario.ScenarioInfo.Title + ".png");
            screenshot.SaveAsFile(screenshotLocation);
            return screenshotLocation;
        }
    }
}
