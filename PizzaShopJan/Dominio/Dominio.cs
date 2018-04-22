using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Dominio
{
    public class CreatePizza
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public List<Guid> Ingredients { get; set; }

        [Required]
        public Byte[] File { get; set; }

        public string MIMEType { get; set; }
    }

    public class Pizza
    {
        private const Decimal beneficio = 5m;
        public Pizza()
        {
            this.Ingredients = new HashSet<Ingredient>();
            this.Comments = new HashSet<Comment>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Byte[] File { get; set; }
        public string MIMEType { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public Decimal TotalCost()
        {
            return Ingredients.Sum(x => x.Cost) + beneficio;
        }
        public List<String> GetIngredients()
        {
            return Ingredients.Select(x => x.Name).ToList();
        }
    }

    public class Comment
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
    }

    public class Ingredient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Decimal Cost { get; set; }
        public ICollection<Pizza> Pizzas { get; set; }
    }    
}
