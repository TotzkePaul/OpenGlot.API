using Microsoft.EntityFrameworkCore;
using PolyglotAPI.Data.Models;

namespace PolyglotAPI.Data.Repos
{
    public interface IModuleRepository
    {
        Task<IEnumerable<Module>> GetAllAsync();
        Task<Module> GetByIdAsync(int id);
        Task AddAsync(Module module);
        Task UpdateAsync(Module module);
        Task DeleteAsync(int id);
    }

    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDbContext _context;

        public ModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Module>> GetAllAsync()
        {
            return await _context.Modules.ToListAsync();
        }

        public async Task<Module> GetByIdAsync(int id)
        {
            return await _context.Modules
                                 .Include(m => m.Lessons)
                                 .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddAsync(Module module)
        {
            await _context.Modules.AddAsync(module);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Module module)
        {
            _context.Modules.Update(module);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            if (module != null)
            {
                _context.Modules.Remove(module);
                await _context.SaveChangesAsync();
            }
        }
    }
}
