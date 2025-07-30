using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quiz.Shared.Models
{
    public class Quizz
    {
        [Key]
        public Guid Id { get; set; }        
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<string> Tags { get; set; } = new List<string>();
        public TimeSpan? Duration { get; set; }        
        public int PassesNumber { get; set; }
        public float AverageResult { get; set; }

        public string AuthorId { get; set; }
        public virtual User Author { get; set; }

        public virtual ICollection<Question> Questions { get; set; } = new HashSet<Question>();

        public Quizz() { }

        public Quizz(Guid id, string authorId, string name, string description, DateTime createdDate, List<string> tags, TimeSpan duration)
        {
            this.Id = id;
            this.AuthorId = authorId;
            this.Name = name;
            this.Description = description;
            this.CreatedDate = createdDate;
            this.Tags = tags;
            this.Duration = duration;
        }
    }
}
