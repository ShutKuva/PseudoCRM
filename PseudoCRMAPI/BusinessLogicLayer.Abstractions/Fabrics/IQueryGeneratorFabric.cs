using BusinessLogicLayer.Abstractions.Database.Generators.QueryGenerators;
using Core.Database.Enums;

namespace BusinessLogicLayer.Abstractions.Fabrics
{
    public interface IFabric<TParameter, TResult>
    {
        public TResult GetGenerator(TParameter provider);
    }
}
