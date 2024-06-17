using System.Linq;

namespace SamsWarehouse.Models.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WarehouseDBContext _context;

        public UserRepository(WarehouseDBContext context)
        {
            _context = context;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool IsEmailExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
