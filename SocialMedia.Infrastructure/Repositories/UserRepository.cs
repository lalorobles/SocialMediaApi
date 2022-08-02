using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities; 
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(SocialMediaContext socialMediaContext) : base(socialMediaContext) { }

        public async Task<User> GetLoginByCredentials(UserLogin login)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Email == login.Email);
        }
         
    }
}
