using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Common.Auth;
using Micro.Common.Exeptions;
using Micro.Services.Identity.Domain.Repositories;
using Micro.Services.Identity.Domain.Services;

namespace Micro.Services.Identity.Services
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
        }
        public async Task RegisterAsync(string email, string password, string name)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
            {
                throw new MicroExeption("email_in_use",
                    $"Email :'{email}' is already in use");
            }

            user = new Domain.Models.User(email, name);
            user.SetPassword(password, _encrypter);
            await _userRepository.AddAsync(user);
        }

        public async Task<JsonWebToken> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);
            if (user == null)
            {
                throw new MicroExeption("invalid_credentials",
                    $"Invalid User");
            }

            if (!user.ValidatePassword(password, _encrypter))
            {
                throw new MicroExeption("invalid_credentials",
                    $"Invalid User");
            }

            return _jwtHandler.Create(user.Id);
        }
    }
}
