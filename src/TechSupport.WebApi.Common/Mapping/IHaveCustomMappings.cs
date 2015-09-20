namespace TechSupport.WebApi.Common.Mapping
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration condifuration);
    }
}
