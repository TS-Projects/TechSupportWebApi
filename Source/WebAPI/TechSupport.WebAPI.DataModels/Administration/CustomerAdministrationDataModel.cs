//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AutoMapper;
//using TechSupport.Data.Models;
//using TechSupport.WebApi.Common.Mapping;

//namespace TechSupport.WebAPI.DataModels.Administration
//{
//    public class CustomerAdministrationDataModel : IMapFrom<Customer>, IHaveCustomMappings
//    {
//        public int Id { get; set; }

//        public int CustomerCardId { get; set; }

//        public int CustomerCardFirstName { get; set; }

//        public string CustomerCardDescription { get; set; }

//        public string CustomerCardSummary { get; set; }

//        public decimal CustomerCardPrice { get; set; }

//        public DateTime? CustomerCardEndDate { get; set; }

//        public string UserName { get; set; }

//        public bool IsOfficial { get; set; }

//        public void CreateMappings(IConfiguration configuration)
//        {
//            configuration.CreateMap<Customer, CustomerAdministrationDataModel>()
//                .ForMember(m => m.UserName, opt => opt.MapFrom(c => c.User.UserName))
//                .ForMember(m => m., opt => opt.MapFrom(c => c.User.UserName));
//        }
//    }
//}
