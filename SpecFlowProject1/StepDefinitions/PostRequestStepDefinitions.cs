using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using RestSharp;
using SpecFlowProject1.Support;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public sealed class PostRequestStepDefinitions
    {
        RestClient client;
        RestRequest request;
        RestResponse response;
        private ISpecFlowOutputHelper outputHelper;

        public PostRequestStepDefinitions(ISpecFlowOutputHelper outputHelper)
        {
            this.outputHelper = outputHelper;
        }

        [Given(@"The user sends a POST request to URL ""([^""]*)""")]
        public async Task GivenTheUserSendsAPOSTRequestToURL(string uri)
        {
            client = new RestClient(uri);
            request = new RestRequest("", Method.Post);
            request.AddJsonBody(new { name = "morpheus", job = "leader" });

            response = await client.ExecuteAsync(request);
        }

        [Then(@"user should get a success message")]
        public void ThenUserShouldGetASuccessMessage()
        {
            Assert.That(response.IsSuccessStatusCode);   
        }


    }
}
