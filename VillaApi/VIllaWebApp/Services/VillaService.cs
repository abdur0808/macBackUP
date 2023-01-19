using System;
using VillaWebApp.Models;
using VillaWebApp.Services.IServices;
using Utl = Villa_Utility.RequestActionType;

namespace VillaWebApp.Services
{
	public class VillaService :BaseService, IVillaService
	{
        private readonly IHttpClientFactory _httpClientFactory;
        private string villaUrl;
		public VillaService(IHttpClientFactory httpClientFactory, IConfiguration configuration):base(httpClientFactory)
		{
            this._httpClientFactory = httpClientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");

        }

        public Task<T> CreateAsync<T>(VillaDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ActionType = Utl.ActionType.POST,
                Data = dto,
                Url = villaUrl + "/api/VillaAPI"

            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ActionType = Utl.ActionType.DELETE,
                Url = villaUrl + "/api/VillaAPI/" + id

            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ActionType = Utl.ActionType.GET,
                Url = villaUrl + "/api/VillaAPI" 

            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ActionType = Utl.ActionType.GET,
                Url = villaUrl + "/api/VillaAPI/id:int?id=" + id

            });
        }

        public Task<T> UpdateAsync<T>(VillaDTO dto)
        {
            return SendAsync<T>(new APIRequest()
            {
                ActionType = Utl.ActionType.PUT,
                Data=dto,
                Url = villaUrl + "/api/VillaAPI/"+ dto.Id

            });
        }
    }
}

