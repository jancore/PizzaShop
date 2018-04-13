using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PizzaShopJan.Models
{
    public class PizzasBindingModel
    {        
        [Display(Name = "ID")]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Nombre de Pizza")]
        public string Name { get; set; }
    }
}