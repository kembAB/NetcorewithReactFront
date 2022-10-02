//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Internal;
//using MVCWebApp.Data;
//using MVCWebApp.Models.Person;
//using MVCWebApp.Models.Person.ViewModels;
//using System.Linq;
//using System.Text.Json;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;

//namespace MVCWebApp.Controllers
//{
//    public class ReactRemoteController : Controller
//    {
//        [Route("api/Person/{id}")]
//        [ApiController]
//        [RequireHttps]
//        public class PeopleAPIController : ControllerBase
//        {
//          public  readonly ApplicationDbContext _dbContext;

//            public PeopleAPIController(ApplicationDbContext context)
//            {
//                _dbContext = context;
//            }

//            [HttpGet]
//            //public JsonResult GetPeople(string? searchString = null)
//            //{

//            //    CreatePersonViewModel viewModel = new CreatePersonViewModel(_dbContext, searchString);
//            //    return new JsonResult(viewModel.SelectedPeopleData.ConvertAll(p => {
//            //        var s = p.StringifyValues;
//            //        return new { id = s[0], name = s[1], phonenumber = s[2], city = s[3], languages = s[4] };
//            //    }));

//                //return new JsonResult(await _dbContext.People.ToListAsync());
//            }

//            [HttpGet("{id}")]
//            public async Task<ActionResult<Person>> GetPerson(int id)
//            {
          
//        var person = await _dbContext.People.FindAsync(id);

//                if (person == null)
//                {
//                    return NotFound();
//                }

//                return person;
//            }

//            // PUT: api/PeopleAPI/5
//            // To protect from overposting attacks, enable the specific properties you want to bind to, for
//            // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
//            [HttpPut("{id}")]
//            public async Task<IActionResult> PutPerson(int id, Person person)
//            {
//                if (id != person.ID)
//                {
//                    return BadRequest();
//                }

//                _dbContext.Entry(person).State = EntityState.Modified;

//                try
//                {
//                    await _dbContext.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!PersonExists(id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }

//                return NoContent();
//            }

//            [HttpPost]
//            public async  Task<ActionResult<Person>> PostPerson(Person person)
//            {
//                //_dbContext.People.Add(person);
//                //await _dbContext.SaveChangesAsync();

//                 return CreatedAtAction("GetPerson", new { id = person.ID }, person);
//            }

//            [HttpDelete("{id}")]
//            public async Task<ActionResult<Person>> DeletePerson(int id)
//            {
//                var person = await _dbContext.People.FindAsync(id);
//                if (person == null)
//                {
//                    return NotFound();
//                }

//                _dbContext.People.Remove(person);
//                await _dbContext.SaveChangesAsync();

//                return person;
//            }

//            private bool PersonExists(int id)
//            {
//                return _dbContext.People.Any(e => e.ID == id);
//            }
//        }
//    }
//}