using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

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
        public HttpResponseMessage GetPersons()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(persons))
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a person by ID", Description = "Retrieves a specific person by their ID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public HttpResponseMessage GetPerson(int id)
        {
            var person = persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(person))
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new person", Description = "Creates a new person and returns the created person")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public HttpResponseMessage PostPerson(Person newPerson)
        {
            newPerson.Id = persons.Max(p => p.Id) + 1;
            persons.Add(newPerson);
            var response = new HttpResponseMessage(HttpStatusCode.Created)
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(newPerson))
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            response.Headers.Location = new System.Uri(Url.Action(nameof(GetPerson), new { id = newPerson.Id }));
            return response;
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update a person by ID", Description = "Updates a specific person's details by their ID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public HttpResponseMessage PutPerson(int id, Person updatedPerson)
        {
            var person = persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            person.Name = updatedPerson.Name;
            person.Surname = updatedPerson.Surname;
            person.Age = updatedPerson.Age;
            person.Gender = updatedPerson.Gender;

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(person))
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a person by ID", Description = "Deletes a specific person by their ID")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public HttpResponseMessage DeletePerson(int id)
        {
            var person = persons.FirstOrDefault(p => p.Id == id);
            if (person == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            persons.Remove(person);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
