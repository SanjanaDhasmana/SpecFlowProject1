using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowProject1.Support;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class PostRequestStepDefinitions
    {
        HttpClient httpClient;
        HttpResponseMessage response;
        string responseBody;
        private ISpecFlowOutputHelper outputHelper;

        public PostRequestStepDefinitions(ISpecFlowOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
            httpClient = new HttpClient();
        }

        [Given(@"The user sends a POST request to URL ""([^""]*)""")]
        public async Task GivenTheUserSendsAPOSTRequestToURL(string uri)
        {
            POSTData postData = new POSTData()
            {
                name = "morpheus",
                job = "leader"
            };

          string data=  JsonConvert.SerializeObject(postData);
            var contentData = new StringContent(data);

            response = await httpClient.PostAsync(uri, contentData);
            responseBody = await response.Content.ReadAsStringAsync();
            outputHelper.WriteLine("----"+responseBody);

        }

        [Then(@"user should get a success message")]
        public void ThenUserShouldGetASuccessMessage()
        {
            Assert.That(response.IsSuccessStatusCode);   
        }


    }
}
