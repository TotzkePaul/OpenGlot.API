using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolyglotAPI.Data;
using static PolyglotAPI.Models.Structure;

namespace PolyglotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ILogger<LanguagesController> _logger;

        public LanguagesController(ILanguageRepository languageRepository, ILogger<LanguagesController> logger)
        {
            _languageRepository = languageRepository;
            _logger = logger;
        }

        // GET: api/Languages
        [HttpGet]
        public ActionResult<IEnumerable<Language>> GetLanguages()
        {
            _logger.LogInformation("Getting all languages");
            return Ok(_languageRepository.GetAll());
        }

        // GET: api/Languages/5
        [HttpGet("{id}")]
        public ActionResult<Language> GetLanguage(int id)
        {
            _logger.LogInformation($"Getting language with ID: {id}");
            var language = _languageRepository.GetById(id);
            if (language == null)
            {
                _logger.LogWarning($"Language with ID: {id} not found");
                return NotFound();
            }
            return Ok(language);
        }

        // POST: api/Languages
        [HttpPost]
        public ActionResult<Language> AddLanguage(Language language)
        {
            _logger.LogInformation("Adding a new language");
            _languageRepository.Add(language);
            return CreatedAtAction(nameof(GetLanguage), new { id = language.Id }, language);
        }

        // PUT: api/Languages/5
        [HttpPut("{id}")]
        public IActionResult UpdateLanguage(int id, Language language)
        {
            if (id != language.Id)
            {
                _logger.LogWarning($"Language ID mismatch: {id} != {language.Id}");
                return BadRequest();
            }
            _logger.LogInformation($"Updating language with ID: {id}");
            _languageRepository.Update(language);
            return NoContent();
        }

        // DELETE: api/Languages/5
        [HttpDelete("{id}")]
        public IActionResult DeleteLanguage(int id)
        {
            _logger.LogInformation($"Deleting language with ID: {id}");
            _languageRepository.Delete(id);
            return NoContent();
        }
    }

}
