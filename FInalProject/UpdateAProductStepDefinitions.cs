using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace FInalProject
{
   
    [Binding]
    public class UpdateAProductStepDefinitions
    {
        private static string authToken;
        RestClient client = new RestClient("http://demostore.gatling.io/api/");
        RestRequest requestToken = new RestRequest("authenticate", Method.Post);
        RestRequest request = new RestRequest("product", Method.Post);
        private RestResponse responseToken;
        RestResponse response;

        [Given(@"A valid authentication endpoint to the site")]
        public void GivenAValidAuthenticationEndpointToTheSite()
        {
            var body = new AuthenticatePut { username = "admin", password = "admin" };
            var jsonBody = JsonConvert.SerializeObject(body);

            requestToken.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

            responseToken = client.Execute(requestToken);

            Assert.That(responseToken.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var jsonObject = JObject.Parse(responseToken.Content);
            authToken = jsonObject["token"].ToString();
        }

        [Given(@"An endpoint and a body information to update a product")]
        public void GivenAnEndpointAndABodyInformationToUpdateAProduct()
        {
            request.AddHeader("Authorization", "Bearer " + authToken);
            request.AddUrlSegment("id", "17");

            var productData = new
            {
                name = "White Glasses",
                description = "Purple Glasses",
                image = "purple-glasses.jpg",
                price = "59.99",
                categoryId = 7
            };

            var jsonBody = JsonConvert.SerializeObject(productData);
            request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

        }

        [When(@"A post request is sent")]
        public async Task WhenAPostRequestIsSent()
        {
            response = await client.ExecuteAsync(request);
        }

        [Then(@"A valid http response code is expected")]
        public void ThenAValidHttpResponseCodeIsExpected()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
