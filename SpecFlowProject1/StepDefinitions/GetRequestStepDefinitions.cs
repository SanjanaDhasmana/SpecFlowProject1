using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using RestSharp;
using SpecFlowProject1.Support;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class GetRequestStepDefinitions
    {
        private IWebDriver driver;
        RestClient client;
        RestRequest request;
        RestResponse response;

        ISpecFlowOutputHelper outputHelper;

        public GetRequestStepDefinitions(ISpecFlowOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Given(@"The user sends a GET request with URL ""([^""]*)""")]
        public async Task GivenTheUserSendsAGETRequestWithURL(string uri)
        {
            client = new RestClient(uri);
            request = new RestRequest("", Method.Get);
            response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var data = JsonConvert.DeserializeObject<JSONData>(response.Content);
                if (data != null)
                {
                    outputHelper.WriteLine($"Page: {data.page}");
                    outputHelper.WriteLine($"Per page: {data.per_page}");
                    outputHelper.WriteLine($"Total Pages: {data.total_pages}");
                }
            }
            else
            {
                Console.WriteLine($"Error: {response.ErrorMessage}");
            }
        }

        [Then(@"Request should be a success with (.*) Status Code")]
        public void ThenRequestShouldBeASuccessWithStatusCode(int p0)
        {
            Assert.That(response.IsSuccessStatusCode);
        }

    }
}
