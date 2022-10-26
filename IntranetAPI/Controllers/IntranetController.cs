using IntranetAPI.DBContext;
using IntranetAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509;

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

        [HttpGet]
        [Route("/GetPriviliges")]
        public IActionResult GetPriviliges() 
        {
            var priviliges = DbContext.Priviliges.OrderBy(x => x.Id).ToList();

            return Ok(priviliges);
        }

        [HttpPost]
        [Route("/AddNewUser")]
        public IActionResult AddNewUser([FromForm]NewUserModel model) 
        {
            var newUser = new DBContext.User
            {
                Email = model.Email,
                Password = model.Password,
                Name = model.Username,
                Surname = model.Surname,
                PriviligesId = model.Priviliges
            };

            DbContext.Users.Add(newUser);
            DbContext.SaveChanges();

            if (newUser.Id != 0)
            {
                return Ok(new { Message = "Dodano nowego użytkownika", StatusCode = 200 });
            }
            else
            {
                return Ok(new ErrorModel { Message = "Wystąpił bład przy dodawaniu nowego użytkownika", StatusCode = 400 });
            }
        }

        [HttpPost]
        [Route("/ChangePassword")]
        public IActionResult ChangePassword([FromForm]ChangePasswordModel model) 
        {
            var user = DbContext.Users.Where(x => x.Id == model.Id).FirstOrDefault();

            if (user == null)
            {
                return Ok(new { Message = "Nie znaleziono takiego użytkownika", StatusCode = 405 });
            }

            if (user.Password != model.OldPassword)
            {
                return Ok(new { Message = "Stare hasło jest niepoprawne", StatusCode = 405 });
            }

            user.Password = model.NewPassword;
            DbContext.SaveChanges();

            return Ok(new { Message = "Udało się zmienić hasło", StatusCode = 200 });
        }
    }
}
