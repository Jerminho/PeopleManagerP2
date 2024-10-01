using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Model;

namespace PeopleManager.Services
{
    public class PersonService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public PersonService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<PersonResult>> Find()
        {
            return await _dbContext.People
                .Select(p => new PersonResult
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    OrganizationId = p.OrganizationId,
                    OrganizationName = p.Organization != null ? p.Organization.Name : null
                })
                .ToListAsync();
        }

        //Get (by id)
        public async Task<PersonResult?> Get(int id)
        {
            return await _dbContext.People
                .Select(p => new PersonResult
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Email = p.Email,
                    OrganizationId = p.OrganizationId,
                    OrganizationName = p.Organization != null ? p.Organization.Name : null
                })
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //Create
        public async Task<PersonResult?> Create(PersonRequest request)
        {
            var person = new Person
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                OrganizationId = request.OrganizationId
            };

            _dbContext.People.Add(person);
            await _dbContext.SaveChangesAsync();

            return await Get(person.Id);
        }

        //Update
        public async Task<PersonResult?> Update(int id, PersonRequest request)
        {
            var person = await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return null;
            }

            person.FirstName = request.FirstName;
            person.LastName = request.LastName;
            person.Email = request.Email;
            person.OrganizationId = request.OrganizationId;

            await _dbContext.SaveChangesAsync();

            return await Get(id);
        }

        //Delete
        public async Task Delete(int id)
        {
            var person = await _dbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person is null)
            {
                return;
            }

            _dbContext.People.Remove(person);
            await _dbContext.SaveChangesAsync();
        }

    }
}
