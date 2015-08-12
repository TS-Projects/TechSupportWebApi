using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TechSupport.WebAPI.Api.Administration.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        // GET: api/Test
        [Route("")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Test
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }
}