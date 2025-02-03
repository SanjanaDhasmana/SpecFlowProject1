using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class DataDrivenTestingStepDefinitions
    {
        private IWebDriver driver;

        public DataDrivenTestingStepDefinitions(IWebDriver driver)
        {
               this.driver = driver;             
        }


        [Then(@"search for '([^']*)'")]
        public void ThenSearchFor(string searchKey)
        {
            driver.FindElement(By.XPath("//*[@id=\"center\"]/yt-searchbox/div[1]/form/input")).SendKeys(searchKey);
            driver.FindElement(By.XPath("//*[@id=\"center\"]/yt-searchbox/div[1]/form/input")).SendKeys(Keys.Enter);

            Thread.Sleep(2000);
        }


    }
}
