using BCrypt.Net;
using WebApiRestful.Data.Repositories.Component;
using WebApiRestful.Domain.Entities.Component;

namespace WebApiRestful.Service.Component
{
    public class UserService(IUnitOfWork repository) : IUserService
    {
        #region Property
        private readonly IUserRepository _repository = repository.Users;

        #endregion

        /// <summary>
        /// Check Login
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<User?> CheckLogin(string username, string password)
        {
            // Get Password of User
            var user = await this._repository.GetUserByUsernameAsync(username);

            // Verifying Password
            if (BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password, HashType.SHA512))
                return user;
            else
                return null;
        }

        /// <summary>
        /// Get User by Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetUser(string username)
        {
            return await _repository.GetUserByUsernameAsync(username);
        }
    }
}
