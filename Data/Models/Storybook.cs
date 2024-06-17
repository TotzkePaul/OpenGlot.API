namespace PolyglotAPI.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class InteractiveStorybook
    {
        [Key]
        public int StorybookId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Language { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<StoryChoice> Choices { get; set; }
    }

    public class StoryChoice
    {
        [Key]
        public int ChoiceId { get; set; }
        public int StorybookId { get; set; }
        public InteractiveStorybook Storybook { get; set; }
        public string ChoiceText { get; set; }
        public string Outcome { get; set; }
    }
}
