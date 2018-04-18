using System;
using System.Collections.Generic;
using System.IO;
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
        public ValuesController(ILogger logger)
        {
            _logger = logger;
        }
        // GET api/values
        /*public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }*/
        public IEnumerable<Ingredient> Get()
        {
            return _logger.Ingredients();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values        
        public void Post([FromBody]UploadRequestViewModel model)
        {
            MemoryStream file = new MemoryStream();
            CopyStream(model.File.InputStream, file);
            var createPizza = new CreatePizza() { Name = model.Name, Ingredients = model.Ingredients, File = file.ToArray() };
            _logger.Write(createPizza);
        }

        // PUT api/values/5
        public void Put([FromBody]CreatePizza createPizza)
        {
            //_updater.Modify(createPizza);
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[16 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _logger.Dispose();
            base.Dispose(disposing);
        }
    }
}