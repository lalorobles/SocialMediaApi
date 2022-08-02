using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Threading.Tasks;

namespace SocialMedia.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetLoginByCredentials(UserLogin userLogin)
        {
            return await _unitOfWork.UserRepository.GetLoginByCredentials(userLogin);
        }
        public async Task RegisterUser(User ser)
        {
            await _unitOfWork.UserRepository.Add(ser);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
