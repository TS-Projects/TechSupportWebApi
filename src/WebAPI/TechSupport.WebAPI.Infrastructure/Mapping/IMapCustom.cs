namespace TechSupport.WebAPI.Infrastructure.Mapping
{
    using AutoMapper;

    public interface IMapCustom
    {
        void CreateMappings(IConfiguration configuration);
    }
}