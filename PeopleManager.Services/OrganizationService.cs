using Microsoft.EntityFrameworkCore;
using PeopleManager.Core;
using PeopleManager.Dto.Requests;
using PeopleManager.Dto.Results;
using PeopleManager.Model;

namespace PeopleManager.Services
{
    public class OrganizationService
    {
        private readonly PeopleManagerDbContext _dbContext;

        public OrganizationService(PeopleManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //Find
        public async Task<IList<OrganizationResult>> Find()
        {
            return await _dbContext.Organizations
                .Select(o => new OrganizationResult
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    NumberOfMembers = o.Members.Count
                })
                .ToListAsync();
        }

        //Get (by id)
        public async Task<OrganizationResult?> Get(int id)
        {
            return await _dbContext.Organizations
                .Select(o => new OrganizationResult
                {
                    Id = o.Id,
                    Name = o.Name,
                    Description = o.Description,
                    NumberOfMembers = o.Members.Count
                })
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //Create
        public async Task<OrganizationResult?> Create(OrganizationRequest request)
        {
            var organization = new Organization
            {
                Name = request.Name,
                Description = request.Description
            };

            _dbContext.Organizations.Add(organization);
            await _dbContext.SaveChangesAsync();

            return await Get(organization.Id);
        }

        //Update
        public async Task<OrganizationResult?> Update(int id, OrganizationRequest request)
        {
            var organization = _dbContext.Organizations
                .FirstOrDefault(p => p.Id == id);

            if (organization is null)
            {
                return null;
            }

            organization.Name = request.Name;
            organization.Description = request.Description;

            await _dbContext.SaveChangesAsync();

            return await Get(id);
        }

        //Delete
        public async Task Delete(int id)
        {
            var organization = await _dbContext.Organizations
                .FirstOrDefaultAsync(p => p.Id == id);

            if (organization is null)
            {
                return;
            }

            _dbContext.Organizations.Remove(organization);
            await _dbContext.SaveChangesAsync();
        }

    }
}
