using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using exam2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace exam2.Controllers
{
    public class HomeController : Controller
    {
        private Context dbContext;
							public HomeController(Context context)
							{
								dbContext = context;
							}

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<User> AllUsers = dbContext.Users.ToList();
            
            return View();
        }

         //REGISTRATION METHOD
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
    
            if(ModelState.IsValid)
            {
                
                if (!dbContext.Users.Any(u => u.Email == user.Email))
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                    HttpContext.Session.SetInt32("UserId",user.UserId);
                    return RedirectToAction("Display"); 

                    
                }
                
                
                ModelState.AddModelError("Email", "Email already in use!");

                return View("Index");
                

            }
            else{
                return View("Index");
            }
            
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUser userSubmission)
        {
        if(ModelState.IsValid)
        {
            
            var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.Email);
            
            if(userInDb == null)
            {
                
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View("Index");
            }
            
            
            var hasher = new PasswordHasher<LoginUser>();
            
            
            var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.Password);
            
            
                if(result == 0)
                {
                ModelState.AddModelError("Email", "Invalid Email/Password");
                return View("Index");
                }
                else{
                    HttpContext.Session.SetInt32("UserId",userInDb.UserId);
                    return RedirectToAction("Display");
                }

            }
            else{
                return View("Index");
            }
        }

        [HttpGet("display")] //This will be what the app redirects to.
        public IActionResult Display()
        {
            {
            User userInDb = dbContext.Users.FirstOrDefault(u => u.UserId == (int) HttpContext.Session.GetInt32("UserId"));
            List<Hobby> AllHobbies = dbContext.Hobbies
            .Include( a => a.Creator)
            .Include(a => a.Enthusiasts)
            .ToList();

            if(userInDb == null)
            {
            return RedirectToAction("Logout");
            }
            else
            {
                ViewBag.User = userInDb;
                return View(AllHobbies);
            }
        }
        }

        [HttpGet]
        [Route("createpage")]
        public IActionResult CreateNewPage()
        {
            
            return View("CreateHobby");
        }

        [HttpPost]
        [Route("createhobby")]

        public IActionResult CreateHobby(Hobby newHobby)
        {
            if(ModelState.IsValid)
            {
                newHobby.UserId = (int)HttpContext.Session.GetInt32("UserId");
                dbContext.Hobbies.Add(newHobby);
                dbContext.SaveChanges();
                
                return Redirect("/Display");
            }
            else
            {
                return View("CreateHobby");
            }
        }

        [HttpGet("{hobbyId}")]
        public IActionResult Show(int hobbyId)
        {
            Hobby display = dbContext.Hobbies.Include(a => a.Enthusiasts)
            .ThenInclude(p => p.User)
            .Include(c => c.Creator)
            .FirstOrDefault(l => l.HobbyId == hobbyId);
            ViewBag.User = dbContext.Users.FirstOrDefault(u => u.UserId == (int) HttpContext.Session.GetInt32("UserId"));
        
            return View(display);
        }

        [HttpGet("join/{hobbyId}/{userId}")]
        public IActionResult JoinHobby(int hobbyId, int userId)
        {
            Enthusiast join = new Enthusiast();
            join.UserId = userId;
            join.HobbyId = hobbyId;
            dbContext.Enthusiasts.Add(join);
            dbContext.SaveChanges();
            return Redirect($"/{hobbyId}");
        }

        [HttpGet("cancel/{hobbyId}")]
        public IActionResult DeleteHobby(int hobbyId)
        {
            Hobby cancel = dbContext.Hobbies.FirstOrDefault( e => e.HobbyId == hobbyId);
            dbContext.Hobbies.Remove(cancel);
            dbContext.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpGet("leave/{hobbyId}/{userId}")]
        public IActionResult Leave(int hobbyId, int userId)
        {
            Enthusiast remove = dbContext.Enthusiasts.FirstOrDefault( t => t.UserId == userId && t.HobbyId == hobbyId );
            dbContext.Enthusiasts.Remove(remove);
            dbContext.SaveChanges();
            return RedirectToAction("Home");
        }

        [HttpGet("logout")]
        public IActionResult Logout()   
        {
        HttpContext.Session.Clear();
        return View("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        
    }
}
