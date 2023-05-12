using System.Linq.Expressions;
using BusinessLogicLayer.Abstractions;
using Core;
using DataAccessLayer.Abstractions;

namespace BusinessLogicLayer.Services
{
    public class OrganizationService : IOrganizationService<Organization>
    {
        private readonly IRepository<Organization> _organizationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationService(IRepository<Organization> organizationRepository, IUnitOfWork unitOfWork)
        {
            _organizationRepository = organizationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(Organization entity)
        {
            await _organizationRepository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
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