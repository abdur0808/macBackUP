using System;
using System.Text;
using VillaWebApp.Models;
using VillaWebApp.Services.IServices;
using Newtonsoft.Json;
using VUtl = Villa_Utility.RequestActionType;

namespace VillaWebApp.Services
{
	public class BaseService : IBaseService
	{
        public APIResponse responseModel { get ; set; }

        //for calling we using IHttpClientFactory that is already registerd.

        public IHttpClientFactory httpClient { get; set; }

        public BaseService(IHttpClientFactory httpClientFactory)
		{
            this.httpClient = httpClientFactory;
            this.responseModel = new();

        }

        
        public async Task<T> SendAsync<T>(APIRequest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("VillaAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(apiRequest.Url);
                // Data will not be null in the POST/PUT Http calls.
                if(apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        Encoding.UTF8, "application/json");
                }
                switch (apiRequest.ActionType)
                {
                    case VUtl.ActionType.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case VUtl.ActionType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case VUtl.ActionType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    case VUtl.ActionType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;

                }
                HttpResponseMessage httpResponseMessage = null;
                httpResponseMessage = await client.SendAsync(message);
                var apiContent = await httpResponseMessage.Content.ReadAsStringAsync();
                var _apiResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return _apiResponse;
            }
            catch (Exception ex)
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var res = JsonConvert.SerializeObject(dto);
                var _apiResponse = JsonConvert.DeserializeObject<T>(res);
                return _apiResponse;
            }
        }
    }
}

