using Microsoft.AspNetCore.Mvc;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

namespace Quiz.API.Controllers
{
    public class RoleController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
