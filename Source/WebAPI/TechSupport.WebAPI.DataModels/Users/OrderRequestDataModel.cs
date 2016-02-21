namespace TechSupport.WebAPI.DataModels.Users
{
    using TechSupport.Data.Models;
    using TechSupport.WebApi.Common.Mapping;

    public class OrderRequestDataModel : IMapFrom<CustomerCard>
    {
        public string Id { get; set; }
    }
}
