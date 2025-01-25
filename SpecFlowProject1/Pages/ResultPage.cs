using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SpecFlowProject1.Pages
{
    public class ResultPage
    {

        IWebDriver _driver  ;
        public ResultPage(IWebDriver driver)
        {
            _driver = driver;
        }
        IWebElement channelTextBox => _driver.FindElement(By.LinkText("Testers Talk"));

        public ChannelPage ClickOnChannel()
        {
            channelTextBox.Click();
            return new ChannelPage(_driver);
        }

    }
}
