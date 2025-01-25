using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject1.Pages
{
    public class SearchPage
    {
        IWebDriver _driver;
        public SearchPage(IWebDriver driver)
        {
            _driver = driver;
        }

        IWebElement searchTextBox => _driver.FindElement(By.XPath("//input[@name=\"search_query\" and @role=\"combobox\"]"));

        public ResultPage SearchText(string text)
        {
            searchTextBox.SendKeys(text);
            searchTextBox.SendKeys(Keys.Enter);

            return new ResultPage(_driver);
        }
    }
}
