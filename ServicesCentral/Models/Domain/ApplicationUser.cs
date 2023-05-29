using System;
using Microsoft.AspNetCore.Identity;

namespace ServicesCentral.Models.Domain
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
	}
}

