using static PolyglotAPI.Models.Structure;

namespace PolyglotAPI.Data
{
    public interface ILanguageRepository
    {
        IEnumerable<Language> GetAll();
        Language? GetById(int id);
        void Add(Language language);
        void Update(Language language);
        void Delete(int id);
    }

    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();
        Course? GetById(int id);
        void Add(Course course);
        void Update(Course course);
        void Delete(int id);
    }

    public interface IModuleRepository
    {
        IEnumerable<Module> GetAll();
        Module? GetById(int id);
        void Add(Module module);
        void Update(Module module);
        void Delete(int id);
    }

}
