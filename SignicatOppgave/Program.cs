using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using SignicatOppgave.Models;

namespace SignicatOppgave1
{
    class Program
    {


        // TASK 1: Creating an identification session
        static void Main(string[] args)
        {
            // Access token
            string token = getToken();

            // Create a REST Client Connection
            var client = new RestClient("https://api.idfy.io/identification/v2/");

            // Create a POST request
            var request = new RestRequest("sessions", Method.POST);

            // Necessary Headers, which includes access token for authorization
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            // Necessary Json data
            string postdata = "{\n  \"flow\": \"redirect\",\n  \"allowedProviders\": [\n    \"no_bankid_netcentric\",\n    \"no_bankid_mobile\"\n  ],\n  \"include\": [\n    \"name\",\n    \"date_of_birth\"\n  ],\n  \"redirectSettings\": {\n    \"successUrl\": \"https://developer.signicat.io/landing-pages/identification-success.html\",\n    \"abortUrl\": \"https://developer.signicat.io/landing-pages/something-wrong.html\",\n    \"errorUrl\": \"https://developer.signicat.io/landing-pages/something-wrong.html\"\n  }\n}";
            request.AddJsonBody(postdata);

            // Get the response
            IRestResponse response = client.Execute(request);

            // Checking if the response status is successful (200 or 201)
            if (response.IsSuccessful)
            {

                // Deserialize json data
                var obj = JsonConvert.DeserializeObject<MainRoot>(response.Content);
                // Console.WriteLine(JsonConvert.SerializeObject(obj, Formatting.Indented));

                // Print out response
                Console.WriteLine("{" + "\n  Url: " + obj.url + "\n  RequestId: " + obj.id + "\n}");
                getData(obj.id, token);
            }
        }



        // TASK 2: Downloading the identification session
        public static void getData(dynamic id, string token)
        {

            // Create a REST Client Connection
            var client = new RestClient("https://api.signicat.io/identification/v2/");

            // Create a GET request
            var request = new RestRequest("sessions/{id}", Method.GET);
            request.AddUrlSegment("id", id);

            // Header which includes access token for authorization
            request.AddHeader("Authorization", "Bearer " + token);
            request.AddHeader("Content-Type", "application/json");

            // Get the response
            IRestResponse response = client.Execute(request);
            Console.ReadLine();

            //Deserialize json data
            var obj1 = JsonConvert.DeserializeObject<MainRoot>(response.Content);

            // Print out formated response and serialize json data
            Console.WriteLine(JsonConvert.SerializeObject(obj1, Formatting.Indented));
        }



        //Extra method used for generating access token
        public static string getToken()
        {
            string url = "https://api.signicat.io/oauth/connect/token";
            string client_id = "t5801bef7522547b685b79ee49a350208";
            string client_secret = "6MtlO92PEgg9zS8q6yLDetfbBKZibZ5T";

            //Request token
            var restclient = new RestClient(url);

            // Create a POST request
            RestRequest request = new RestRequest() { Method = Method.POST };

            // Neccessary headers and parameters
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", client_id);
            request.AddParameter("client_secret", client_secret);
            request.AddParameter("grant_type", "client_credentials");

            // Get the response 
            var response = restclient.Execute(request);

            // Deserialize json data
            var token = JsonConvert.DeserializeObject<TokenRoot>(response.Content);
            return token.access_token;
        }
    }

}
