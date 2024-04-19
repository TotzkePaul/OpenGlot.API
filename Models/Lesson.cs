namespace PolyglotAPI.Models
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public string DifficultyLevel { get; set; }
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();

        public Lesson(int lessonId, string title, string language, string difficultyLevel, List<Exercise> exercises)
        {
            LessonId = lessonId;
            Title = title;
            Language = language;
            DifficultyLevel = difficultyLevel;
            Exercises = exercises;
        }
    }

    public class Content
    {
        public int ContentId { get; set; }
        public string ContentType { get; set; } // Text, Image, Audio, Video

        protected Content(int contentId, string contentType)
        {
            ContentId = contentId;
            ContentType = contentType;
        }
    }


    public class Exercise
    {
        public int ExerciseId { get; set; }
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public Content Content { get; set; }

        public Exercise(int exerciseId, string question, List<string> options, string correctAnswer, Content content)
        {
            ExerciseId = exerciseId;
            Question = question;
            Options = options;
            CorrectAnswer = correctAnswer;
            Content = content;
        }
    }

}
