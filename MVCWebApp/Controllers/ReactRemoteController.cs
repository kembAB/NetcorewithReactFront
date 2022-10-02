using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MVCWebApp.Data;
using MVCWebApp.Models.Person;
using MVCWebApp.Models.Person.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVCWebApp.Controllers
{
    public class ReactRemoteController : Controller
    {
        JsonSerializerOptions jsonSerializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve

        };


        ApplicationDbContext dbContext;
        public ReactRemoteController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }



       
        [AllowAnonymous]
        [Route("api/Person/{id}")]
        public JsonResult GetPeople()
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
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await dbContext.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.ID)
            {
                return BadRequest();
            }

            dbContext.Entry(person).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {


            return CreatedAtAction("GetPerson", new { id = person.ID}, person);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await dbContext.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            dbContext.People.Remove(person);
            await dbContext.SaveChangesAsync();

            return person;
        }

        private bool PersonExists(int id)
        {
            return dbContext.People.Any(e => e.ID == id);
        }
    }
}
