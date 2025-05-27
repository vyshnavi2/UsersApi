using System.Text.Json;
using UsersApi.Models;

namespace UsersApi.Services
{
    public class UserInfoService : IUserInfoService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<UserInfoService> _logger;

        public UserInfoService(IConfiguration config, ILogger<UserInfoService> logger)
        {
            _config = config;
            _logger = logger;
        }
        public async Task SaveToFileAsync(List<UserInfo> userInfo)
        {
            try
            {
                string baseDir = _config["StorageConfig:BaseDirectory"] ?? "C:\\Temp";
                string targetDir = Path.Combine(baseDir, "Users", "IN");

                Directory.CreateDirectory(targetDir);

                string filename = $"UserInfo_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                string fullPath = Path.Combine(targetDir, filename);

                var json = JsonSerializer.Serialize(userInfo, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(fullPath, json);

                _logger.LogInformation($"User Info saved to {fullPath}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save user info");
                throw new ApplicationException("An error occurred while saving user info.", ex);
            }
        }
    }
}
