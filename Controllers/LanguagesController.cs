using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PolyglotAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LanguagesController : ControllerBase
    {
        public class Language
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
        }

        private readonly ILogger<LanguagesController> _logger;

        private readonly List<Language>  languages = new List<Language>
            {
                new Language { Id = 1, Name = "Spanish", Code = "ES" },
                new Language { Id = 2, Name = "Chinese", Code = "ZH" },
                new Language { Id = 3, Name = "English", Code = "EN" },
                new Language { Id = 4, Name = "Japanese", Code = "JA" },
                new Language { Id = 5, Name = "Arabic", Code = "AR" },
                new Language { Id = 6, Name = "Portuguese", Code = "PT" },
                new Language { Id = 7, Name = "Russian", Code = "RU" }
                
            };

        public LanguagesController(ILogger<LanguagesController> logger)
        {
            _logger = logger;
        }

        // Write mock endpoints and fields that handles multiple spoken languages


        [HttpGet]
        
        public ActionResult<IEnumerable<Language>> GetLanguages()
        {
            return Ok(languages);
        }

        
        [HttpGet("{id}")]
        public ActionResult<Language> GetLanguage(int id)
        {
            var language = languages.FirstOrDefault(x => x.Id == id);

            if (language == null)
            {
                return NotFound();
            }

            return Ok(language);
        }
    }
}
