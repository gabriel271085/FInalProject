using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace FInalProject
{
    [Binding]
    public class GetProductTestStepDefinitions
    {
        RestClient client = new RestClient("http://demostore.gatling.io/api/");
        RestRequest request = new RestRequest("product/{id}", Method.Get);
        RestResponse response;

        [Given(@"An endpoint with id value (.*)")]
        public void GivenAnEndpointWithIdValue(int idnumber)
        {
            request.AddUrlSegment("id", "17");
        }

        [When(@"A GET request is sent")]
        public void WhenAGETRequestIsSent()
        {
            response = client.Execute(request);
        }

        [Then(@"A valid response code is expected")]
        public void ThenAValidResponseCodeIsExpected()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
