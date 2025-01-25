using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using BoDi;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SpecFlowProject1.Utility;
using System.Configuration;

namespace SpecFlowProject1.Hooks
{
    [Binding]
    public sealed class Hooks : ExtentReport
    {
        private readonly IObjectContainer Container;
        private readonly IConfiguration _configuration;
        IWebDriver driver;
        public Hooks(IObjectContainer container)
        {
            Container = container;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            NUnit.Framework.TestContext.Progress.WriteLine("Running before Test run");
            Console.WriteLine("Running before Test run");
            ExtentReportInit();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Running after Test run");
            ExtentReportTearDown();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Console.WriteLine("Running before feature");
            _feature = _extentReports.CreateTest(featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            Console.WriteLine("Running after feature");
        }


        [BeforeScenario("@TestersTalks")]
        public void BeforeScenarioWithTag()
        {
            Console.WriteLine("Running inside Tag hooks in Specflow");
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running before scenario");
            //string browser = scenarioContext.ScenarioInfo.Tags[1];
            //driver = WebDriverFactory.CreateWebDriver(browser);

            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://www.youtube.com");
            Thread.Sleep(2000);
            driver.Manage().Window.Maximize();
            Container.RegisterInstanceAs<IWebDriver>(driver);
            _scenario = _feature.CreateNode(scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Console.WriteLine("Running after scenario");
            var driver = Container.Resolve<IWebDriver>();
            if (driver != null)
            {
                driver.Quit();
            }
        }

        [AfterStep]
        public void AfterStep(ScenarioContext scenarioContext)
        {
            Console.WriteLine("Running after Step");
            string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            string stepName = scenarioContext.StepContext.StepInfo.Text;

            var driver = Container.Resolve<IWebDriver>();


            //When Scenario passed
            if (scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName);
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName);
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName);
                }
            }

            //When Scenario fails
            if (scenarioContext.TestError != null)
            {

                if (stepType == "Given")
                {
                    _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, scenarioContext)).Build());
                }
                else if (stepType == "When")
                {
                    _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, scenarioContext)).Build());
                }
                else if (stepType == "Then")
                {
                    _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, scenarioContext)).Build());
                }
                else if (stepType == "And")
                {
                    _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, scenarioContext)).Build());
                }
            }
        }
    }
}