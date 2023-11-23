using AutoMapper;
using BCrypt.Net;
using TodoAPI.Component.DomainLayer.Models.DTO;
using TodoAPI.Component.DomainLayer.Models.Entities;
using TodoAPI.Component.RepositoryLayer.Repository;
using TodoAPI.Core.Common.Helpers;

namespace TodoAPI.Component.ServiceLayer.Services
{
    public class LoginService(LoginRepository repository, IMapper mapper)
    {
        #region Property
        private readonly LoginRepository _repository = repository;
        private readonly IMapper _mapper = mapper;

        #endregion

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task<string> Login(UserLogin model)
        {
            // Validate
            if (model.UserName == null && model.Password == null)
                return string.Empty;

            // Ｍap model to new user object
            var user = this._mapper.Map<User>(model);

            // Get Password of User
            var passwordHash = await this._repository.GetPassword(user.UserName ?? string.Empty);

            // Verifying Password
            if (BCrypt.Net.BCrypt.EnhancedVerify(model.Password, passwordHash, HashType.SHA512))
                return this._repository.Login(user);    // Get token
            else
                return string.Empty;
        }
    }
}