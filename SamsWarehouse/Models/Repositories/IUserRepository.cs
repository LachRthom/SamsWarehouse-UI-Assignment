using System.Collections.Generic;

namespace SamsWarehouse.Models.Repositories
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        void AddUser(User user);
        bool IsEmailExists(string email);
    }
}
