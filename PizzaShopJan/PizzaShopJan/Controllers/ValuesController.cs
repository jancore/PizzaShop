using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Dominio;
using PizzaShopJan.Models;

namespace PizzaShopJan.Controllers
{
    [AllowAnonymous]
    public class ValuesController : ApiController
    {
        readonly ILogger _logger;
        readonly IUpdater _updater;
        public ValuesController(ILogger logger, IUpdater updater)
        {
            _logger = logger;
            _updater = updater;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]CreatePizza createPizza)
         {
             _logger.Write(createPizza);
         }

        // PUT api/values/5
        public void Put([FromBody]CreatePizza createPizza)
        {
            _updater.Modify(createPizza);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            _logger.Dispose();
            base.Dispose(disposing);
        }
    }
}