using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Quiz.Shared.Models
{
    public class Role : IdentityRole
    {
        [Key]
        public int Id { get; set; }
        public string Name {  get; set; }

        public Role() { }

        public Role(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
