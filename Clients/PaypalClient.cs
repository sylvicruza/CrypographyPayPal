using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using UniversityCateringService.Payloads;
using static UniversityCateringService.Payloads.CreateOrderRequest;
using Microsoft.Build.Framework;

namespace UniversityCateringService.Clients
{
    public sealed class PaypalClient
    {
        public string ClientId { get; }
        public string ClientSecret { get; }
        public string BaseUrl { get; }

        public PaypalClient(string clientId, string clientSecret, string baseUrl)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            BaseUrl = baseUrl;
        }


        private async Task<AuthResponse> Authenticate()
        {
            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));
            var content = new List<KeyValuePair<string, string>>
            {
                new("grant_type", "client_credentials")
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"{BaseUrl}/v1/oauth2/token"),
                Method = HttpMethod.Post,
                Headers =
                {
                    { "Authorization", $"Basic {auth}" }
                },
                Content = new FormUrlEncodedContent(content)
            };

            var httpClient = new HttpClient();
            var httpResponse = await httpClient.SendAsync(request);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(jsonResponse))
            {
                throw new JsonException("Empty response returned");
            }
            var response = JsonSerializer.Deserialize<AuthResponse>(jsonResponse);

            return response;
        }


        public async Task<CreateOrderResponse> CreateOrder(string value, string currency, string reference)
        {
            var auth = await Authenticate();
            string json = @"{
    ""intent"": ""CAPTURE"",
   
    ""purchase_units"": [
        {
            ""invoice_id"": ""INV073922"",
            ""reference_id"": ""INV073922"",
            ""items"": [
                {
                    ""name"": ""Coca Cola Original soft drink 330ml in glass"",
                    ""description"": ""Black Current"",
                    ""quantity"": ""1"",
                    ""unit_amount"": {
                        ""currency_code"": ""USD"",
                        ""value"": ""42.00""
                    }
                },
                {
                    ""name"": ""Pizza Blanket 47\"" Round Burritos Rortilla Blankets Soft Flannel Giant Food Rug"",
                    ""description"": ""Acsergery"",
                    ""quantity"": ""1"",
                    ""unit_amount"": {
                        ""currency_code"": ""USD"",
                        ""value"": ""58.00""
                    }
                }
            ],
            ""amount"": {
                ""currency_code"": ""USD"",
                ""value"": ""100.00"",
                ""breakdown"": {
                    ""item_total"": {
                        ""currency_code"": ""USD"",
                        ""value"": ""100.00""
                    }
                }
            }
        }
    ],
    ""application_context"": {
        ""return_url"": ""https://example.com/return"",
        ""cancel_url"": ""https://example.com/cancel""
    }
}";


            PayPalOrderRequest request = JsonSerializer.Deserialize<PayPalOrderRequest>(json);
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {auth.access_token}");

            var httpResponse = await httpClient.PostAsJsonAsync($"{BaseUrl}/v2/checkout/orders", request);

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(jsonResponse))
            {
                throw new JsonException("Empty response returned");
            }
            var response = JsonSerializer.Deserialize<CreateOrderResponse>(jsonResponse);

            return response;
        }

        public async Task<CaptureOrderResponse> CaptureOrder(string orderId)
        {
            var auth = await Authenticate();

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {auth.access_token}");

            var httpContent = new StringContent("", Encoding.Default, "application/json");

            var httpResponse = await httpClient.PostAsync($"{BaseUrl}/v2/checkout/orders/{orderId}/capture", httpContent);

            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(jsonResponse))
            {
                throw new JsonException("Empty response returned");
            }
            var response = JsonSerializer.Deserialize<CaptureOrderResponse>(jsonResponse);

            return response;
        }


    }

}
