using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class Fearure1StepDefinitions
    {
        private IWebDriver driver;

        public Fearure1StepDefinitions(IWebDriver driver)
        {
               this.driver = driver;    
        }

        [Given(@"Open the browser")]
        public void GivenOpenTheBrowser()
        {
        }

        [When(@"Enter the URL")]
        public void WhenEnterTheURL()
        {

            //driver.Navigate().GoToUrl("https://www.youtube.com");
            //Thread.Sleep(2000);
        }

        [Then(@"search for the tester talks")]
        public void ThenSearchForTheTesterTalks()
        {
            driver.FindElement(By.XPath("//*[@id=\"center\"]/yt-searchbox/div[1]/form/input")).SendKeys("Testers Talk");
            driver.FindElement(By.XPath("//*[@id=\"center\"]/yt-searchbox/div[1]/form/input")).SendKeys(Keys.Enter);

            Thread.Sleep(2000);
        }

    }
}
