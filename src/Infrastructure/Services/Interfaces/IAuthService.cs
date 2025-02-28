using Domain.Models;
using Domain.Models.Authentication.Login;

namespace Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponseModel> Authenticate(string username, string password);
    Task<ApiResponse> Register(string username, string fullname, string password);
    Task<ApiResponse> ChangePassword(string username, string currentPassword, string newPassword, CancellationToken cancellationToken);

}
