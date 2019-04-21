using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;

        public DatingRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T: class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T: class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _context.Users.Include(u => u.Photos).ToListAsync();
        }

        public async Task<User> GetUser(int userId)
        {
            return await _context.Users.Include(u => u.Photos).SingleOrDefaultAsync(u => u.Id == userId);
        }
        
        public async Task<Photo> GetPhoto(int photoId)
        {
            return await _context.Photos.SingleOrDefaultAsync(p => p.Id == photoId);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(p => p.UserId == userId).SingleOrDefaultAsync(p => p.IsMain);
        }
    }
}