using DogHouseService.Domain.Entities;
using DogHouseService.Domain.Interfaces;
using DogHouseService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DogHouseService.Infrastructure.Repositories
{
    public class DogRepository : IDogRepository
    {
        private readonly AppDbContext _context;

        public DogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dog>> GetAllAsync()
        {
            return await _context.Dogs.ToListAsync();
        }

        public async Task<Dog> GetByIdAsync(int id)
        {
            return await _context.Dogs.FindAsync(id);
        }

        public async Task AddAsync(Dog dog)
        {
            await _context.Dogs.AddAsync(dog);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Dogs.AnyAsync(d => d.Name == name);
        }

        public async Task UpdateAsync(Dog dog)
        {
            _context.Dogs.Update(dog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Dog dog)
        {
            _context.Dogs.Remove(dog);
            await _context.SaveChangesAsync();
        }
    }
}
