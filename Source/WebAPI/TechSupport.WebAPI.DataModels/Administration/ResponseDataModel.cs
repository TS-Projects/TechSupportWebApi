﻿namespace TechSupport.WebAPI.DataModels.Administration
{
    using TechSupport.Data.Models;
    using TechSupport.WebApi.Common.Mapping;

    public class ResponseDataModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Phone { get; set; }

        public string About { get; set; }
    }
}
