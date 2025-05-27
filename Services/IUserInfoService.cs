using UsersApi.Models;

namespace UsersApi.Services
{
    public interface IUserInfoService
    {
        Task SaveToFileAsync(List<UserInfo> userInfo);
    }
}
