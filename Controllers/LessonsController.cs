using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PolyglotAPI.Models;


namespace PolyglotAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LessonsController : ControllerBase
    {
        private readonly ILogger<LessonsController> _logger;

        private readonly List<Lesson> lessons = new List<Lesson>
{
    new Lesson(
        lessonId: 1,
        title: "Basic Greetings",
        language: "Spanish",
        difficultyLevel: "Beginner",
        exercises: new List<Exercise>
        {
            new Exercise(
                exerciseId: 1,
                question: "How do you say 'Hello' in Spanish?",
                options: new List<string> { "Hola", "Adiós", "Gracias" },
                correctAnswer: "Hola",
                content: null // Assuming content is optional or to be filled in later
            ),
            new Exercise(
                exerciseId: 2,
                question: "Which one of these is 'Goodbye'?",
                options: new List<string> { "Hola", "Adiós", "Gracias" },
                correctAnswer: "Adiós",
                content: null
            )
        }
    ),
    new Lesson(
        lessonId: 2,
        title: "Numbers 1-10",
        language: "Spanish",
        difficultyLevel: "Beginner",
        exercises: new List<Exercise>
        {
            new Exercise(
                exerciseId: 3,
                question: "How do you say 'Eight' in Spanish?",
                options: new List<string> { "Seis", "Siete", "Ocho" },
                correctAnswer: "Ocho",
                content: null
            ),
            new Exercise(
                exerciseId: 4,
                question: "What is 'Uno' in English?",
                options: new List<string> { "One", "Two", "Three" },
                correctAnswer: "One",
                content: null
            )
        }
    )
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
            return Ok(lessons.Where(x => x.LessonId == id));
        }

    }
}
