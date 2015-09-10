namespace TechSupport.Services.Logic.Contracts
{
    using TechSupport.Services.Common;

    public interface IMappingService : IService
    {
        T Map<T>(object source);
    }
}
