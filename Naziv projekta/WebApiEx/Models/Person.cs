using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace WebApiEx
{
    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string?Surname { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }
    }

}
