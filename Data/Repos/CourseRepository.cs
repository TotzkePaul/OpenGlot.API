using Microsoft.EntityFrameworkCore;
using PolyglotAPI.Data.Models;

namespace PolyglotAPI.Data.Repos
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course?> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);
        Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId);
    }

    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var courses = await _context.Courses
                .Include(l => l.Language)
                //.Include(c => c.Modules)
                .ToListAsync();
            return courses;
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses
                                 .Include(l => l.Language)
                                 .Include(c => c.Modules)
                                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await GetByIdAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId)
        {
            return await _context.Modules
                                 .Where(m => m.CourseId == courseId)
                                 .ToListAsync();
        }
    }
}
