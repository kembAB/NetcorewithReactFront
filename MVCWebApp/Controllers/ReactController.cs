using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCWebApp.Data;
using MVCWebApp.Models.Person;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MVCWebApp.Controllers
{
    public class ReactController : Controller
    {
        JsonSerializerOptions jsonSerializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve

        };


        ApplicationDbContext dbContext;
        public ReactController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("api/Person/{id}")]
        public JsonResult GetAll()
        {
         
            var dbResult = dbContext.People.Include(p => p.City).Include(p => p.City.Country).Include(p => p.PersonLanguages);

         
            List<Person> PeopleCleanedUp = new List<Person>();
            foreach (Person p in dbResult)
            {
                PeopleCleanedUp.Add(new Person(p));
            }

            var jsoned = Json(PeopleCleanedUp, jsonSerializerOptions);
            return jsoned;
        }

        [AllowAnonymous]
        [Route("api/Person/{id}")]
        public JsonResult GetByid(int id)
        {
            var dbResult = dbContext.People.Include("PersonLanguage.PersonLanguages").Include("City").Where(p => p.ID == id);
            
            List<Person> PeopleCleanedUp = new List<Person>();
            foreach (Person p in dbResult)
            {
                PeopleCleanedUp.Add(new Person(p));
            }

            var jsonData = Json(PeopleCleanedUp, jsonSerializerOptions);
            return jsonData;
        }

    }
}

