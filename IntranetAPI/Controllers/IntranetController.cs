using IntranetAPI.DBContext;
using IntranetAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace IntranetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IntranetController : ControllerBase
    {
        public bazadoprojektu12Context DbContext;

        public IntranetController() 
        {
            DbContext = new bazadoprojektu12Context();
        }

        [HttpGet]
        [Route("/GetUserById")]
        public IActionResult GetUserById([FromQuery]int id)
        {
            var user = DbContext.VUsersWithPriviliges.FirstOrDefault(x => x.Id == id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpGet]
        [Route("/GetUserToLogin")]
        public IActionResult GetUserToLogin([FromQuery]string email, [FromQuery]string password)
        {
            var user = DbContext.Users.Where(x => x.Email == email && x.Password == password);
            if (user.Count() == 0)
            {
                return Ok(new ErrorModel{ Message = "Nie znaleziono takiego użytkownika, bądź hasło jest nieprawidłowe", StatusCode = 404 });
            }

            var userFound = user.FirstOrDefault();
            if (userFound != null)
                return Ok(new { Id = userFound.Id });
            else
                return NotFound(new ErrorModel{ Message = "Nie ma takiego użytkownika", StatusCode = 400});
        }

        [HttpGet]
        [Route("/GetNews")]
        public IActionResult GetNews() 
        {
            var news = DbContext.News.ToList();

            return Ok(news);
        }
    }
}
