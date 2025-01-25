using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject1.Pages
{
    public class ChannelPage
    {
        IWebDriver _driver;
        public ChannelPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string getTitle()
        {
            return _driver.Title;
        }
    }
}
