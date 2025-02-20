using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using SpecFlowProject1.Support;
using System;
using TechTalk.SpecFlow;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class APILoginTestingStepDefinitions
    {
        private RestResponse _response;
        private string _baseUrl = "https://dummyjson.com/auth/login";
        AuthenticationUsers authenticationUsers;

        public APILoginTestingStepDefinitions()
        {
            authenticationUsers = new AuthenticationUsers();
        }
        [Given(@"I send a login request with username ""([^""]*)"" and password ""([^""]*)""")]
        public void GivenISendALoginRequestWithUsernameAndPassword(string kminchelle, string p1)
        {
            var restclient = new RestClient(_baseUrl);
            var request = new RestRequest("", Method.Post);
            var requestBody = new
            {
                username = kminchelle,
                password = p1
            };
            request.AddJsonBody(requestBody);
            _response = restclient.Execute(request);

            authenticationUsers = JsonConvert.DeserializeObject<AuthenticationUsers>(_response.Content);
        }

        [Then(@"I should get a successful response")]
        public void ThenIShouldGetASuccessfulResponse()
        {
            Console.WriteLine(_response.Content);
            Assert.That((int)_response.StatusCode, Is.EqualTo(200));
        }

        [Then(@"I should see the role as ""([^""]*)""")]
        public void ThenIShouldSeeTheRoleAs(string admin)
        {
            string actualRole = authenticationUsers.email;
            Console.WriteLine(actualRole);
        }
    }
}
