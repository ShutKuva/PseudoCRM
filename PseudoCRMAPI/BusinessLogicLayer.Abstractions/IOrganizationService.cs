using BusinessLogicLayer.Abstractions.Services;

namespace BusinessLogicLayer.Abstractions
{
    public interface IOrganizationService<TOrganization> : ICreateService<TOrganization>, IReadService<TOrganization>
    {

    }
}