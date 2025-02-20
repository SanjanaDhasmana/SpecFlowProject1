using Allure.Net.Commons;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using BoDi;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using SpecFlowProject1.Utility;
using System.Configuration;
using System.Net.NetworkInformation;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowProject1.Hooks
{
    [Binding]
    public sealed class Hooks : ExtentReport
    {
        private readonly IObjectContainer Container;
        private readonly ScenarioContext _scenarioContext;
        IWebDriver driver;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        //private static readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
        //    .SetBasePath(Directory.GetCurrentDirectory())
        //    .AddJsonFile("node-config.json")
        //    .Build();

        public Hooks(IObjectContainer container, ScenarioContext scenarioContext)
        {
            Container = container;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            logger.Info("==== Test Execution Started ====");
            // ExtentReportInit();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            Console.WriteLine("Running after Test run");
           // ExtentReportTearDown();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            Console.WriteLine("Running before feature");
           // _feature = _extentReports.CreateTest(featureContext.FeatureInfo.Title);
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            logger.Info("==== Test Execution Finished ====");
        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario(ScenarioContext scenarioContext)
        {
            logger.Info($"Starting Scenario: {scenarioContext.ScenarioInfo.Title}");

            string browser = Environment.GetEnvironmentVariable("BROWSER") ?? "Chrome";

            if (browser.Equals("Chrome", StringComparison.OrdinalIgnoreCase))
            {
                ChromeOptions chromeOptions = new ChromeOptions();
                driver = new ChromeDriver(chromeOptions);
            }
            else if (browser.Equals("Firefox", StringComparison.OrdinalIgnoreCase))
            {
                var firefoxOptions = new FirefoxOptions();
                driver = new FirefoxDriver(firefoxOptions);
            }
            else
            {
                throw new Exception("Unsupported browser: " + browser);
            }

            //driver = new RemoteWebDriver(new Uri("http://localhost:4444/"), options);

           // driver = WebDriverFactory.CreateWebDriver("chrome");
            Container.RegisterInstanceAs<IWebDriver>(driver);
            driver.Navigate().GoToUrl("https://www.youtube.com");
            driver.Manage().Window.Maximize();

            //_scenario = _feature.CreateNode(scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            logger.Info($"Scenario Completed: {scenarioContext.ScenarioInfo.Title}");

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
            //string stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();
            //string stepName = scenarioContext.StepContext.StepInfo.Text;

            //var driver = Container.Resolve<IWebDriver>();


            ////When Scenario passed
            //if (scenarioContext.TestError == null)
            //{
            //    if (stepType == "Given")
            //    {
            //        _scenario.CreateNode<Given>(stepName);
            //    }
            //    else if (stepType == "When")
            //    {
            //        _scenario.CreateNode<When>(stepName);
            //    }
            //    else if (stepType == "Then")
            //    {
            //        _scenario.CreateNode<Then>(stepName);
            //    }
            //}

            //When Scenario fails
            if (scenarioContext.TestError != null)
            {
                logger.Error($"Step Failed: {scenarioContext.StepContext.StepInfo.Text}");
                logger.Error($"Error Message: {scenarioContext.TestError.Message}");
                AllureApi.AddAttachment("Screenshot", "image/png", addScreenShot(driver, scenarioContext));

                //if (stepType == "Given")
                //{
                //    _scenario.CreateNode<Given>(stepName).Fail(scenarioContext.TestError.Message,
                //        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, scenarioContext)).Build());
                //}
                //else if (stepType == "When")
                //{
                //    _scenario.CreateNode<When>(stepName).Fail(scenarioContext.TestError.Message,
                //        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, scenarioContext)).Build());
                //}
                //else if (stepType == "Then")
                //{
                //    _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                //        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, scenarioContext)).Build());
                //}
                //else if (stepType == "And")
                //{
                //    _scenario.CreateNode<Then>(stepName).Fail(scenarioContext.TestError.Message,
                //        MediaEntityBuilder.CreateScreenCaptureFromPath(addScreenShot(driver, scenarioContext)).Build());
                //}
            }
        }
    }
}