using System;
using static Villa_Utility.RequestActionType;

namespace VillaWebApp.Models
{
	public class APIRequest
	{
		public ActionType ActionType { get; set; } = ActionType.GET;
		public string Url { get; set; }
		public object Data { get; set; }
	}
}

