using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowProject1.Support;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class GetRequestStepDefinitions
    {
        private IWebDriver driver;
        HttpClient httpClient;
        HttpResponseMessage response;
        string responseBody;
        private ISpecFlowOutputHelper outputHelper;

        public GetRequestStepDefinitions(ISpecFlowOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            httpClient = new HttpClient();
        }

        [Given(@"The user sends a GET request with URL ""([^""]*)""")]
        public async Task GivenTheUserSendsAGETRequestWithURL(string uri)
        {
            response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            responseBody = await response.Content.ReadAsStringAsync();
            //outputHelper.WriteLine(responseBody);

            var deSerializedData=JsonConvert.DeserializeObject<JSONData>(responseBody);
            outputHelper.WriteLine("after deserialization page is "+deSerializedData.page.ToString());
            foreach (var item in deSerializedData.data) 
            {
                outputHelper.WriteLine(item.last_name);
                outputHelper.WriteLine(item.first_name);
                outputHelper.WriteLine(item.avatar);
            }
             
        }


        [Then(@"Request should be a success with (.*) Status Code")]
        public void ThenRequestShouldBeASuccessWithStatusCode(int p0)
        {
            Assert.That(response.IsSuccessStatusCode);
        }

    }
}
