using DogHouseService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogHouseService.Domain.Interfaces
{
    public interface IDogRepository
    {
        Task<IEnumerable<Dog>> GetAllAsync();
        Task<Dog> GetByIdAsync(int id);
        Task AddAsync(Dog dog);
        Task<bool> ExistsByNameAsync(string name);
        Task UpdateAsync(Dog dog);
        Task DeleteAsync(Dog dog);
    }
}
