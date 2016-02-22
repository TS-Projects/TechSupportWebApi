using System;
using System.Data.Entity;
using AutoMapper;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData;
using System.Web.OData.Routing;
using AutoMapper.QueryableExtensions;
using Elmah;
using TechSupport.Data;
using TechSupport.Data.Common.Repositories;
using TechSupport.Data.Models;
using TechSupport.Services.Data.Contracts;
using TechSupport.WebAPI.Controllers;
using TechSupport.WebAPI.DataModels.Administration.CustomerCards;
using TechSupport.WebAPI.Infrastructure;

namespace TechSupport.WebAPI.Controllers
{
    public class CustomerCardsController : ODataController
    {
        private readonly IRepository<CustomerCard> cutomerCards;

        public CustomerCardsController(IRepository<CustomerCard> cutomerCards)
        {
            this.cutomerCards = cutomerCards;
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        [EnableQuery]
        [ODataRoute]
        public IQueryable<CustomerCardAdministrationDataModel> Get()
        {
            var cards = this.cutomerCards
                .All()
                .ProjectTo<CustomerCardAdministrationDataModel>();

            return cards;
        }

        [ODataRoute]
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IHttpActionResult Post(CustomerCardAdministrationDataModel customerCards)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //customerCards.Id = Guid.NewGuid().ToString();
            var generatedId = GenerateRandomNumber(12);
            var existId = this.cutomerCards.All().Select(u => u.Id == generatedId).Any();

            while (true)
            {
                if (!existId)
                {
                    customerCards.Id = generatedId;
                    break;
                }
                generatedId = GenerateRandomNumber(12);
                existId = this.cutomerCards.All().Select(u => u.Id == generatedId).Any();
            }

            var dm = Mapper.Map<CustomerCard>(customerCards);

            this.cutomerCards.Add(dm);

            try

            {
                this.cutomerCards.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CardExists(customerCards.Id))
                {
                    return Conflict();
                }
                throw;
            }

            return Created(customerCards);
        }


        [ODataRoute]
        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public IHttpActionResult Put([FromODataUri] string key, CustomerCardAdministrationDataModel update)
        {
            //var customEx = new Exception("Hello I am testing Elmah", new NotSupportedException());
            //ErrorSignal.FromCurrentContext().Raise(customEx);
            //throw new Exception();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (key != update.Id)
            {
                return BadRequest();
            }

            if (this.cutomerCards.GetById(update.Id) == null)
            {
                return NotFound();
            }

            var dbModel = this.cutomerCards.GetById(update.Id);
            Mapper.Map<CustomerCardAdministrationDataModel, CustomerCard>(update, dbModel);
            this.cutomerCards.Update(dbModel);

            try
            {
                this.cutomerCards.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.Updated(update);
        }

        [ODataRoute]
        [Authorize(Roles = "Administrator")]
        [HttpDelete]
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            var card = this.cutomerCards
                .All()
                .FirstOrDefault(k => k.Id == key);

            if (card == null)
            {
                return NotFound();
            }
            this.cutomerCards.Delete(card);
            this.cutomerCards.SaveChanges();
            return StatusCode(HttpStatusCode.NoContent);
        }

        static string GenerateRandomNumber(int count)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                int number = StaticRandom.Instance.Next(10);
                builder.Append(number);
            }

            return builder.ToString();
        }

        private bool CardExists(string cardId)
        {
            return this.cutomerCards.All().Select(u => u.Id == cardId).Any();
        }
    }
}