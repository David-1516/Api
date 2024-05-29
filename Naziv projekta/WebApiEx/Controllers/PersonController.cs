using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApiEx.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private static List<Person> persons = new List<Person>
        {
            new Person { Id = 1, Name = "Jana", Surname = "Janić", Age = 30, Gender = "Female" },
            new Person { Id = 2, Name = "Marko", Surname = "Kojić", Age = 25, Gender = "Reket" }
        };


      
        [HttpGet]
        [SwaggerOperation(Summary = "Get all persons", Description = "Retrieves a list of all persons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            return Ok(persons);
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a person by ID", Description = "Retrieves a specific person by their ID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Person> GetPerson(int id)
        {
            var person = persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Create a new person", Description = "Creates a new person and returns the created person")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<Person> PostPerson(Person newPerson)
        {
            newPerson.Id = persons.Max(p => p.Id) + 1; 
            persons.Add(newPerson);
            return CreatedAtAction(nameof(GetPerson), new { id = newPerson.Id }, newPerson);
        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a person by ID", Description = "Updates a specific person's details by their ID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a person by ID", Description = "Deletes a specific person by their ID")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
