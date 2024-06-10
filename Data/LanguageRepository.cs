using Microsoft.EntityFrameworkCore;
using System;
using static PolyglotAPI.Models.Structure;

namespace PolyglotAPI.Data
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDbContext _context;

        public LanguageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Language> GetAll()
        {
            return _context.Languages.Include(l => l.Courses).ThenInclude(c => c.Modules).ThenInclude(m => m.Lessons).ThenInclude(l => l.Questions).ToList();
        }

        public Language? GetById(int id)
        {
            return _context.Languages.Include(l => l.Courses)
                .ThenInclude(c => c.Modules)
                .ThenInclude(m => m.Lessons)
                .ThenInclude(l => l.Questions)
                .FirstOrDefault(l => l.Id == id);
        }

        public void Add(Language language)
        {
            _context.Languages.Add(language);
            _context.SaveChanges();
        }

        public void Update(Language language)
        {
            _context.Languages.Update(language);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var language = _context.Languages.Find(id);
            if (language != null)
            {
                _context.Languages.Remove(language);
                _context.SaveChanges();
            }
        }
    }

    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAll()
        {
            return _context.Courses.Include(c => c.Modules).ThenInclude(m => m.Lessons).ThenInclude(l => l.Questions).ToList();
        }

        public Course? GetById(int id)
        {
            return _context.Courses.Include(c => c.Modules).ThenInclude(m => m.Lessons).ThenInclude(l => l.Questions).FirstOrDefault(c => c.Id == id);
        }

        public void Add(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void Update(Course course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var course = _context.Courses.Find(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
            }
        }
    }
    public class ModuleRepository : IModuleRepository
    {
        private readonly ApplicationDbContext _context;

        public ModuleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Module> GetAll()
        {
            return _context.Modules.Include(m => m.Lessons).ToList();
        }

        public Module? GetById(int id)
        {
            return _context.Modules.Include(m => m.Lessons).FirstOrDefault(m => m.Id == id);
        }

        public void Add(Module module)
        {
            _context.Modules.Add(module);
            _context.SaveChanges();
        }

        public void Update(Module module)
        {
            _context.Modules.Update(module);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var module = _context.Modules.Find(id);
            if (module != null)
            {
                _context.Modules.Remove(module);
                _context.SaveChanges();
            }
        }
    }

}
