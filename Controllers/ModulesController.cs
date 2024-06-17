using Microsoft.AspNetCore.Mvc;
using PolyglotAPI.Data.Models;
using PolyglotAPI.Data.Repos;


namespace PolyglotAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleRepository _moduleRepository;
        private readonly ILogger<ModulesController> _logger;

        public ModulesController(IModuleRepository moduleRepository, ILogger<ModulesController> logger)
        {
            _moduleRepository = moduleRepository;
            _logger = logger;
        }

        // GET: api/Modules
        [HttpGet]
        public ActionResult<IEnumerable<Module>> GetModules()
        {
            _logger.LogInformation("Getting all modules");
            var modules = _moduleRepository.GetAllAsync();
            return Ok(modules);
        }

        // GET: api/Modules/5
        [HttpGet("{id}")]
        public ActionResult<Module> GetModule(int id)
        {
            _logger.LogInformation($"Getting module with ID: {id}");
            var module = _moduleRepository.GetByIdAsync(id);
            if (module == null)
            {
                _logger.LogWarning($"Module with ID: {id} not found");
                return NotFound();
            }
            return Ok(module);
        }

        // POST: api/Modules
        [HttpPost]
        public ActionResult<Module> AddModule(Module module)
        {
            _logger.LogInformation("Adding a new module");
            _moduleRepository.AddAsync(module);
            return CreatedAtAction(nameof(GetModule), new { id = module.Id }, module);
        }

        // PUT: api/Modules/5
        [HttpPut("{id}")]
        public IActionResult UpdateModule(int id, Module module)
        {
            if (id != module.Id)
            {
                _logger.LogWarning($"Module ID mismatch: {id} != {module.Id}");
                return BadRequest();
            }
            _logger.LogInformation($"Updating module with ID: {id}");
            _moduleRepository.UpdateAsync(module);
            return NoContent();
        }

        // DELETE: api/Modules/5
        [HttpDelete("{id}")]
        public IActionResult DeleteModule(int id)
        {
            _logger.LogInformation($"Deleting module with ID: {id}");
            _moduleRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
