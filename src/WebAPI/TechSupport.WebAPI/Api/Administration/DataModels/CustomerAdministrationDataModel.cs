using AutoMapper;
using TechSupport.Data.Models;
using TechSupport.WebAPI.DataModels;
using TechSupport.WebAPI.Infrastructure.Mapping;

namespace TechSupport.WebAPI.Api.Administration.DataModels
{
    public class CustomerAdministrationDataModel : AdministrationDataModel, IMapFrom<Customer>, IMapCustom
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int CustomerCardId { get; set; }

        public string UserName { get; set; }

        public bool IsOfficial { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Customer, CustomerAdministrationDataModel>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(c => c.User.UserName)); ;
        }
    }
}