using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolyglotAPI.Data.Models;
using PolyglotAPI.Data.Repos;


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
        public async Task<ActionResult<IEnumerable<Language>>> GetLanguages()
        {
            _logger.LogInformation("Getting all languages");
            var languages = await _languageRepository.GetAllAsync();
            return Ok(languages);
        }

        // GET: api/Languages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Language>> GetLanguage(int id)
        {
            _logger.LogInformation($"Getting language with ID: {id}");
            var language = await _languageRepository.GetByIdAsync(id);
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
            _languageRepository.AddAsync(language);
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
            _languageRepository.UpdateAsync(language);
            return NoContent();
        }

        // DELETE: api/Languages/5
        [HttpDelete("{id}")]
        public IActionResult DeleteLanguage(int id)
        {
            _logger.LogInformation($"Deleting language with ID: {id}");
            _languageRepository.DeleteAsync(id);
            return NoContent();
        }
    }

}
