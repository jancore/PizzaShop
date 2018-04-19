using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Dominio;

namespace PizzaShopJan.Models
{
    public class UploadRequestViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public List<Guid> Ingredients { get; set; }

        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}