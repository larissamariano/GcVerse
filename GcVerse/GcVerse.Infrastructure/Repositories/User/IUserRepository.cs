using GcVerse.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcVerse.Infrastructure.Repositories.User
{
    public interface IUserRepository : IBaseRepository<BaseUser>
    {
        Task<BaseUser> IsLoginValid(string email, string password);

    }
}
