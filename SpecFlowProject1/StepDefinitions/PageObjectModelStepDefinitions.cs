using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowProject1.Pages;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class PageObjectModelStepDefinitions
    {
        private IWebDriver driver;
        SearchPage sp;
        ResultPage rp;
        ChannelPage cp;
        public PageObjectModelStepDefinitions(IWebDriver driver)
        {
            this.driver = driver;
        }

        [Given(@"Enter the youTube URL")]
        public void GivenEnterTheYouTubeURL()
        {
            driver.Url = "https://www.youtube.com";

            Thread.Sleep(2000);
        }

        [When(@"Search for Testers Talk")]
        public void WhenSearchForTestersTalk()
        {
            sp = new SearchPage(driver);
            rp = sp.SearchText("Tester Talk");
            Thread.Sleep(2000);
        }

        [When(@"Navigate to Channel")]
        public void WhenNavigateToChannel()
        {
            //rp = new ResultPage(driver);
            cp = rp.ClickOnChannel();
            Thread.Sleep(2000);
        }

        [Then(@"Verify the title of page")]
        public void ThenVerifyTheTitleOfPage()
        {
            Assert.That(cp.getTitle(), Is.EqualTo("Testers Talk - YouTube"));

        }
    }
}
