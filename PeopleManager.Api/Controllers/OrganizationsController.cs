using Microsoft.AspNetCore.Mvc;
using PeopleManager.Dto.Requests;
using PeopleManager.Services;

namespace PeopleManager.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrganizationsController(OrganizationService organizationService) : ControllerBase
    {
        private readonly OrganizationService _organizationService = organizationService;

        //Find (more) GET
        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var organizations = await _organizationService.Find();
            return Ok(organizations);
        }

        //Get (one) GET
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var organization = await _organizationService.Get(id);
            return Ok(organization);
        }

        //Create POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrganizationRequest request)
        {
            var createdOrganization = await _organizationService.Create(request);
            return Ok(createdOrganization);
        }

        //Update PUT
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] OrganizationRequest request)
        {
            var updatedOrganization = await _organizationService.Update(id, request);
            return Ok(updatedOrganization);
        }

        //Delete DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _organizationService.Delete(id);
            return Ok();
        }

    }
}
