using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class ExamplesDataDrivenTestingStepDefinitions
    {
        private IWebDriver driver;

        public ExamplesDataDrivenTestingStepDefinitions(IWebDriver driver)
        {
               this.driver = driver;    
        }

        [Then(@"Search with (.*)")]
        public void ThenSearchWithSpecflowByTestersTalk(string search)
        {
            Console.WriteLine("----Inside MEthod");
            driver.FindElement(By.XPath("//*[@id=\"center\"]/yt-searchbox/div[1]/form/input")).SendKeys(search);
            driver.FindElement(By.XPath("//*[@id=\"center\"]/yt-searchbox/div[1]/form/input")).SendKeys(Keys.Enter);

            Thread.Sleep(2000);
        }


    }
}
