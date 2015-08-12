//using AutoMapper.QueryableExtensions;
//using System.Linq;
//using System.Web.Http;
//using System.Web.Http.Cors;
//using System.Web.OData;
//using TechSupport.Data;
//using TechSupport.Data.Models;
//using TechSupport.WebAPI.Controllers;
//using DataModel = TechSupport.WebAPI.Api.Administration.DataModels.CustomerCardAdministrationDataModel;

//namespace TechSupport.WebAPI.Api.Administration.Controllers
//{
//    [EnableCors("*", "*", "*")]
//    public class CustomerCardController : BaseOdataController
//    {
//        public CustomerCardController(ITechSupportData data)
//            : base(data)
//        {
//        }

//        public CustomerCardController(ITechSupportData data, User userProfile)
//            : base(data, userProfile)
//        {
//        }

//        [AllowAnonymous]
//        [HttpGet]
//        [EnableQuery]
//        //[ResponseType(typeof(DataModel))]
//        public IQueryable<DataModel> Get()
//        {
//            var customers = this.Data
//                .CustomerCards
//                .All()
//                .AsQueryable()
//                .Project()
//                .To<DataModel>();

//            return customers;
//        }
//    }
//}