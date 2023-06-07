using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using TechTalk.SpecFlow;

namespace FInalProject
{

    public class AuthenticatePutCreate
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    [Binding]
    public class CreateProductStepDefinitions
    {
        private static string authToken;
        RestClient client = new RestClient("http://demostore.gatling.io/api/");
        RestRequest requestToken = new RestRequest("authenticate", Method.Post);
        RestRequest request = new RestRequest("product", Method.Post);
        private RestResponse responseToken;
        RestResponse response;


        [Given(@"I have an authentication endpoint to the site")]
        public void GivenIHaveAnAuthenticationEndpointToTheSite()
        {
            
            requestToken.AddHeader("Content-Type", "application/json");
            var body = new AuthenticatePut { username = "admin", password = "admin" };
            var jsonBody = JsonConvert.SerializeObject(body);

            requestToken.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

            responseToken = client.Execute(requestToken);

            Assert.That(responseToken.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var jsonObject = JObject.Parse(responseToken.Content);
            authToken = jsonObject["token"].ToString();
        }

        [Given(@"endpoint to create a product")]
        public void GivenEndpointToCreateAProduct()
        {
            request.AddHeader("Authorization", "Bearer " + authToken);

            var productData = new
            {
                name = "Mojix Project",
                slug = "Mojix Project",
                description = "<p>Some casual black &amp; blue glasses</p>",
                price = "14.99",
                categoryId = "5"
            };
            var jsonBody = JsonConvert.SerializeObject(productData);
            request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);

        }

        [When(@"A POST request is sent")]
        public async Task WhenAPOSTRequestIsSent()
        {
            response = await client.ExecuteAsync(request);
        }

        [Then(@"valid response code is expected")]
        public void ThenValidResponseCodeIsExpected()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}
    