using RssAgregator.Models.Results;
using RssAgregator.Models.Services;
using System.Net.Http;

namespace RssAgregator.BAL.Interfaces.Services
{
    public interface IUserService
    {
        GenericResult<bool> UserNameExists(string name);

        GenericResult<bool> EmailExists(string email);

        GenericResult<string> Login(UserModel userModel, HttpResponseMessage responce, HttpRequestMessage request);

        GenericResult<string> CreateUpdate(UserModel userModel, HttpResponseMessage responce, HttpRequestMessage request);

        GenericResult<UserModel> GetUserData(HttpRequestMessage request);
    }
}