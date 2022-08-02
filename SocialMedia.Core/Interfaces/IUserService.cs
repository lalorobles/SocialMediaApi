using SocialMedia.Core.Entities;
using System.Threading.Tasks;

namespace SocialMedia.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetLoginByCredentials(UserLogin userLogin);
        Task RegisterUser(User user);
    }
}