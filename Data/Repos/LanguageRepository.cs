using Microsoft.EntityFrameworkCore;
using PolyglotAPI.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PolyglotAPI.Data.Repos
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAllAsync();
        Task<Language?> GetByIdAsync(int id);
        Task AddAsync(Language language);
        Task UpdateAsync(Language language);
        Task DeleteAsync(int id);
        Task<IEnumerable<Course>> GetCoursesByLanguageIdAsync(int languageId);
    }


    public class LanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDbContext _context;

        public LanguageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Language>> GetAllAsync()
        {
            return await _context.Languages.ToListAsync();
        }

        public async Task<Language?> GetByIdAsync(int id)
        {
            return await _context.Languages
                                 .Include(l => l.Courses)
                                 .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddAsync(Language language)
        {
            await _context.Languages.AddAsync(language);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Language language)
        {
            _context.Languages.Update(language);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var language = await _context.Languages.FindAsync(id);
            if (language != null)
            {
                _context.Languages.Remove(language);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Course>> GetCoursesByLanguageIdAsync(int languageId)
        {
            return await _context.Courses
                                 .Where(c => c.LanguageId == languageId)
                                 .ToListAsync();
        }
    }

}
