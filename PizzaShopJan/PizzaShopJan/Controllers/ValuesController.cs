using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Dominio;
using Infraestructura;
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
        [Route("ingredients")]
        public IEnumerable<object> GetIngredients()
        {
            return _logger.Ingredients();
        }

        // GET api/values/5
        [Route("pizzas")]
        public IEnumerable<object> GetPizza()
        {
            List<object> pizzasDTO = new List<object>();
            var pizzas = _logger.Pizzas();
            string url;
            foreach(var pizza in pizzas)
            {
                var type = pizza.GetType();
                var properties = type.GetProperties();
                url = "pizzas/" + properties[0].GetValue(pizza).ToString(); 
                pizzasDTO.Add(new { Name = properties[1].GetValue(pizza), Ingredients = properties[2].GetValue(pizza), URL = url});
            }            
            return pizzasDTO;
        }

        /*[Route("pizzas/id")]
        public IEnumerable<Pizza> GetPizza(int id)
        {
            return _logger.Pizzas();
        }*/

        // POST api/values   
        [Route("pizzas/add")]
        public void Post([FromBody]UploadRequestViewModel model)
        {
            MemoryStream file = new MemoryStream();
            CopyStream(model.File.InputStream, file);
            var createPizza = new CreatePizza() { Name = model.Name, Ingredients = model.Ingredients, File = file.ToArray(), MIMEType = model.File.ContentType };
            _logger.Write(createPizza);
        }

        // PUT api/values/5
        public void Put([FromBody]CreatePizza createPizza)
        {

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