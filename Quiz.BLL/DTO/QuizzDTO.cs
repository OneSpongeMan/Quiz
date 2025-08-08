using System.ComponentModel.DataAnnotations;

namespace Quiz.BLL.DTO
{
    public class QuizzDTO
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<string> Tags { get; set; } = new List<string>();
        public TimeSpan? Duration { get; set; }
        public int PassesNumber { get; set; }

        public required string AuthorId { get; set; }
        public virtual UserDTO? Author { get; set; }
        public virtual ICollection<QuestionDTO>? Questions { get; set; } = new HashSet<QuestionDTO>();
        
    }
}
