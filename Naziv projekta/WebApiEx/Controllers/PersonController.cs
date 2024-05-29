using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiEx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private static List<Person> persons = new List<Person>
        {
            new Person { Id = 1, Name = "John", Surname = "Doe", Age = 30, Gender = "Male" },
            new Person { Id = 2, Name = "Jane", Surname = "Doe", Age = 25, Gender = "Female" }
        };

        // GET: api/Person
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            return Ok(persons);
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson(int id)
        {
            var person = persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        // POST: api/Person
        [HttpPost]
        public ActionResult<Person> PostPerson(Person newPerson)
        {
            newPerson.Id = persons.Max(p => p.Id) + 1; // Auto-increment ID
            persons.Add(newPerson);
            return CreatedAtAction(nameof(GetPerson), new { id = newPerson.Id }, newPerson);
        }

        // PUT: api/Person/5
        [HttpPut("{id}")]
        public ActionResult<Person> PutPerson(int id, Person updatedPerson)
        {
            var person = persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            person.Name = updatedPerson.Name;
            person.Surname = updatedPerson.Surname;
            person.Age = updatedPerson.Age;
            person.Gender = updatedPerson.Gender;
            return Ok(person);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public ActionResult DeletePerson(int id)
        {
            var person = persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            persons.Remove(person);
            return NoContent();
        }
    }
}
