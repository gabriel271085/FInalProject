using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace FInalProject
{
    [Binding]
    public class GetProductsByCategoryStepDefinitions
    {
        RestClient client = new RestClient("http://demostore.gatling.io/api/");
        RestRequest request = new RestRequest("product", Method.Get);
        RestResponse response;

        [Given(@"An endpoint with the query parameter '([^']*)' set to '([^']*)'")]
        public void GivenAnEndpointWithTheQueryParameterSetTo(string category, string p1)
        {
            request.AddQueryParameter("category","5");
        }

        [When(@"I send a get request")]
        public void WhenISendAGetRequest()
        {
            response = client.Execute(request);
        }

        [Then(@"The response should have a status code of '([^']*)'")]
        public void ThenTheResponseShouldHaveAStatusCodeOf(string oK)
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
