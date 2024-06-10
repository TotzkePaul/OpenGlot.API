using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolyglotAPI.Models;
using static PolyglotAPI.Models.Structure;


namespace PolyglotAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LessonsController : ControllerBase
    {
        private readonly ILogger<LessonsController> _logger;

        private readonly List<Lesson> lessons = new List<Lesson>
        {
            new Lesson { Id = 1, Name = "Greetings" },
            new Lesson { Id = 2, Name = "Numbers" },
            new Lesson { Id = 3, Name = "Colors" },
            new Lesson { Id = 4, Name = "Days of the Week" },
            new Lesson { Id = 5, Name = "Months of the Year" },
            new Lesson { Id = 6, Name = "Seasons" },
            new Lesson { Id = 7, Name = "Weather" },
            new Lesson { Id = 8, Name = "Directions" },
            new Lesson { Id = 9, Name = "Time" },
            new Lesson { Id = 10, Name = "Family" },
            new Lesson { Id = 11, Name = "Body Parts" },
            new Lesson { Id = 12, Name = "Clothing" },
            new Lesson { Id = 13, Name = "Food" },
            new Lesson { Id = 14, Name = "Animals" },
            new Lesson { Id = 15, Name = "Jobs" },
            new Lesson { Id = 16, Name = "Transportation" },
            new Lesson { Id = 17, Name = "Hobbies" },
            new Lesson { Id = 18, Name = "Sports" },
            new Lesson { Id = 19, Name = "Music" },
            new Lesson { Id = 20, Name = "Movies" },
            new Lesson { Id = 21, Name = "Books" },
            new Lesson { Id = 22, Name = "Technology" },
            new Lesson { Id = 23, Name = "Travel" },
            new Lesson { Id = 24, Name = "Shopping" },
            new Lesson { Id = 25, Name = "Health" },
            new Lesson { Id = 26, Name = "Emergencies" },
            new Lesson { Id = 27, Name = "School" },
            new Lesson { Id = 28, Name = "Work" },
            new Lesson { Id = 29, Name = "Home" },
            new Lesson { Id = 30, Name = "Nature" },
            new Lesson { Id = 31, Name = "Environment" },
            new Lesson { Id = 32, Name = "Culture" }
        };

        public LessonsController(ILogger<LessonsController> logger)
        {
            _logger = logger;
        }

        // Write mock endpoints and fields that handles multiple lessons

        [HttpGet]
        
        public ActionResult<IEnumerable<Lesson>> GetLessons()
        {
            return Ok(lessons);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<IEnumerable<Lesson>> GetLessonsByLanguage(int id)
        {
            return Ok(lessons.Where(x => x.Id == id));
        }

    }
}
