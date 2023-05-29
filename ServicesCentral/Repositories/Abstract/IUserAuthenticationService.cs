using System;
using System.Net.NetworkInformation;
using ServicesCentral.Models.DTO;

namespace ServicesCentral.Repositories.Abstract
{
	public interface IUserAuthenticationService
	{
        Task<Status> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(RegistrationModel model);
    }
}

