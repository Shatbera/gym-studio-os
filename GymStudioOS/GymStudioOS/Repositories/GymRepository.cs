using GymStudioOS.Data;
using GymStudioOS.Models.Gym.Data;
using Microsoft.EntityFrameworkCore;

namespace GymStudioOS.Repositories
{
    public class GymRepository : IRepository<Gym>
    {
        private readonly ApplicationDbContext _context;

        public GymRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Gym entity)
        {
            await _context.Gyms.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Gym>> GetAllAsync()
        {
            return await _context.Gyms.ToListAsync();
        }

        public async Task<Gym> GetByIdAsync(int id)
        {
            var gym = await _context.Gyms.FindAsync(id);
            if (gym == null)
                throw new KeyNotFoundException($"Gym with ID {id} not found.");
            return gym;
        }

        public async Task UpdateAsync(Gym entity)
        {
            _context.Gyms.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
