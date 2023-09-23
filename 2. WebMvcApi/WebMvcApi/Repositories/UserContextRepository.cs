using Microsoft.EntityFrameworkCore;

using WebMvcApi.Models;
using WebMvcApi.DBContexts;

namespace WebMvcApi.Repositories {
    public class UserContextRepository : AbstractRepository<User> {
        private readonly UserContext _context;

        public UserContextRepository(UserContext context) {
            _context = context;
        }

        public override IEnumerable<User> GetItems() {
            return _context.Users.ToList();
        }

        public override User? GetItem(int id) {
            return _context.Users.Find(id); ;
        }

        public override void Add(User item) {
            _context.Add(item);
        }

        public override void Update(User item) {
            _context.Entry(item).State = EntityState.Modified;
        }

        public override void Delete(int id) {
            var item = GetItem(id);
            if (item != null)
                _context.Users.Remove(item);
        }

        public override void Save() {
            _context.SaveChangesAsync();
        }
    }
}
