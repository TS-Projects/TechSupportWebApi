using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TechSupport.Data.Models;

namespace TechSupport.WebAPI.Api.CustomerCards.DataModels
{
    public class CustomerDataModel
    {
        public CustomerDataModel(Customer participant, bool official)
        {
            this.Contest = participant.CustomerCards.AsQueryable().Select(c => Mapper.Map<Customer, CustomerCardDataModel>(participant));
            this.ContestIsCompete = official;
        }

        public IEnumerable<CustomerCardDataModel> Contest { get; set; }

        public bool ContestIsCompete { get; set; }
    }
}