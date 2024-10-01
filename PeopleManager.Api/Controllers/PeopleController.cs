using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
using PeopleManager.Services;

namespace PeopleManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PeopleController(PersonService personService) : ControllerBase
    {
        private readonly PersonService _personService = personService;

        //Find (more) GET
        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var people = await _personService.Find();
            return Ok(people);
        }

        //Get (one) GET
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var person = await _personService.Get(id);
            return Ok(person);
        }

        //Create POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]PersonRequest request)
        {
            var createdPerson = await _personService.Create(request);
            return Ok(createdPerson);
        }

        //Update PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody]PersonRequest request)
        {
            var updatedPerson = await _personService.Update(id, request);
            return Ok(updatedPerson);
        }

        //Delete DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            await _personService.Delete(id);
            return Ok();
        }

    }
}
