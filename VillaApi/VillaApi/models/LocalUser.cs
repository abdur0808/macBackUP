using System;
namespace VillaAPI.models
{
	public class LocalUser
	{
		public int Id { get; set; }
		public String UserName { get; set; }
		public String Name { get; set; }
		public string Password { get; set; }
		public string Role { get; set; }
	}
}

