using System;
using VillaWebApp.Models;

namespace VillaWebApp.Services.IServices
{
	// So in the Base service Interface, you will need two things.
	// Request & Response, so you have created a generic interface & class.
	public interface IBaseService
	{
		APIResponse responseModel { get; set; }

		Task<T> SendAsync<T>(APIRequest apiRequest);
	}

}
