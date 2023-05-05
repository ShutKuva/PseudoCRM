using System.Linq.Expressions;
using BusinessLogicLayer.Abstractions;
using Core;
using DataAccessLayer.Abstractions;

namespace BusinessLogicLayer.Services
{
    public class OrganizationService : IOrganizationService<Organization>
    {
        private readonly IRepository<Organization> _organizationRepository;

        public OrganizationService(IRepository<Organization> organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public Task CreateAsync(Organization entity)
        {
            return _organizationRepository.CreateAsync(entity);
        }

        public Task<Organization?> ReadAsync(Expression<Func<Organization, bool>> predicate, int skip, int page)
        {
            return _organizationRepository.ReadAsync(predicate, skip, page);
        }

        public Task<IEnumerable<Organization>> ReadCollectionAsync(Expression<Func<Organization, bool>> predicate, int skip, int take, int page)
        {
            return _organizationRepository.ReadCollectionAsync(predicate, skip, take, page);
        }
    }
}