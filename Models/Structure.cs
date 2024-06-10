namespace PolyglotAPI.Models
{
    public class Structure
    {
        public class Language
        {
            public int Id { get; set; }
            public string Name { get; set; }
            
            public List<Course> Courses { get; set; }
        }

        public class Course
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string DifficultyLevel { get; set; }
            public List<Module> Modules { get; set; }
        }

        public class Module
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Rank { get; set; }
            public List<Lesson> Lessons { get; set; }
        }

        public class Lesson
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int LanguageId { get; set; }
            public string DifficultyLevel { get; set; }
            public string Level { get; set; }
            public List<Question> Questions { get; set; }
        }

        public enum QuestionType
        {
            Match,
            Listen,
            Translate,
            FillInTheBlank,
            TrueFalse,
            MultipleChoice,
            ReorderSentence
        }

        public class Question
        {
            public int Id { get; set; }
            public string Content { get; set; }
            public QuestionType QuestionType { get; set; }

            public List<Option> Options { get; set; }
            public List<Answer> Answers { get; set; }
        }

        public class Option
        {
            public int Id { get; set; }
            public string Text { get; set; } // Text content of the option
            public string ImageUrl { get; set; } // URL to the image
            public string AudioUrl { get; set; } // URL to the audio
        }

        public class Answer
        {
            public int Id { get; set; }
            public List<string> CorrectAnswers { get; set; } // List of alternative correct answers
        }
    }
}
