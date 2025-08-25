using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Quiz.Shared.Models
{
    public class Role : IdentityRole
    {
        public Role(string name) : base(name) { }
    }
}
