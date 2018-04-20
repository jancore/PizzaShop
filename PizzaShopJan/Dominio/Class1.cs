using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Data.Entity;

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

    public class PizzaShopContextInitializer : CreateDatabaseIfNotExists<PizzaShowContext>
    {
        protected override void Seed(PizzaShowContext context)
        {
            List<Ingredient> ingredients = new List<Ingredient>
            {
                new Ingredient {Id=Guid.NewGuid(), Name="Peperoni", Cost=0.3M},
                new Ingredient {Id=Guid.NewGuid(), Name="Mozzarella", Cost=0.5M},
                new Ingredient {Id=Guid.NewGuid(), Name="Champiñones", Cost=0.2M},
                new Ingredient {Id=Guid.NewGuid(), Name="Bacon", Cost=0.7M},
                new Ingredient {Id=Guid.NewGuid(), Name="Piña", Cost=-0.3M},
                new Ingredient {Id=Guid.NewGuid(), Name="Pollo", Cost=0.6M},
                new Ingredient {Id=Guid.NewGuid(), Name="Tomate", Cost=0.25M},
                new Ingredient {Id=Guid.NewGuid(), Name="Nata", Cost=0.4M},
                new Ingredient {Id=Guid.NewGuid(), Name="Ternera", Cost=0.8M},
                new Ingredient {Id=Guid.NewGuid(), Name="Salsa Barbacoa", Cost=0.3M},
                new Ingredient {Id=Guid.NewGuid(), Name="Parmesano", Cost=0.2M},
                new Ingredient {Id=Guid.NewGuid(), Name="Blue Cheese", Cost=0.5M},
                new Ingredient {Id=Guid.NewGuid(), Name="Gorgonzzolla", Cost=0.4M},
                new Ingredient {Id=Guid.NewGuid(), Name="Chocolate", Cost=0.3M}
            };

            // add data into context and save to db
            foreach (Ingredient i in ingredients)
            {
                context.Ingredient.Add(i);
            }
            context.SaveChanges();

        }
    }

    public interface ILogger : IDisposable
    {
        void Write(CreatePizza createPizza);
        List<Ingredient> Ingredients();
        List<Pizza> Pizzas();
    }

    public class Logger : ILogger
    {
        readonly IRepository _repository;
        readonly IUnitOfWork _unitOfWork;
        public Logger(IRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public void Write(CreatePizza createPizza)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            foreach(var IdIngredient in createPizza.Ingredients)
            {
                ingredients.Add(_repository.Find(IdIngredient));
            }
            var pizza = new Pizza()
            {
                Id = Guid.NewGuid(),
                Name = createPizza.Name,
                Ingredients = ingredients,
                File = createPizza.File,
                MIMEType = createPizza.MIMEType
            };
            _repository.Write(pizza);
            _unitOfWork.SaveChanges();
        }

        public List<Ingredient> Ingredients()
        {
            return _repository.GetIngredients();
        }

        public List<Pizza> Pizzas()
        {
            return _repository.GetPizzas();
        }
    }

    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
    }

    public interface IRepositoryPizza
    {
        DbSet IDbSet(Type type);
    }

    public class PizzaShowContext : DbContext, IUnitOfWork, IRepositoryPizza
    {
        public PizzaShowContext() : base("PizzasEntities")
        {

        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (NotSupportedException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IDbSet<Pizza> Pizza { get; set; }
        public IDbSet<Ingredient> Ingredient { get; set; }

        public DbSet IDbSet(Type type)
        {
            return this.Set(type);
        }
    }

    public interface IRepository
    {
        void Write(Pizza pizza);
        Ingredient Find(Guid IdIngrediente);
        List<Ingredient> GetIngredients();
        List<Pizza> GetPizzas();
    }

    public class Repository : IRepository
    {
        readonly IRepositoryPizza _repositoryPizza;
        public Repository(IRepositoryPizza repositoryPizza)
        {
            _repositoryPizza = repositoryPizza;
        }

        public void Write(Pizza pizza)
        {
            var set = _repositoryPizza.IDbSet(typeof(Pizza));
            set.Add(pizza);
        }

        public Ingredient Find(Guid IdIngrediente)
        {
            var set = _repositoryPizza.IDbSet(typeof(Ingredient));
            var ingrediente = (Ingredient) set.Find(IdIngrediente);
            return ingrediente;
        }

        public List<Ingredient> GetIngredients()
        {
            List<Ingredient> ingredientes = new List<Ingredient>();
            var set = _repositoryPizza.IDbSet(typeof(Ingredient));
            foreach(var ingredient in set)
            {
                ingredientes.Add((Ingredient) ingredient);
            }
            return ingredientes;
        }

        public List<Pizza> GetPizzas()
        {
            List<Pizza> pizzas = new List<Pizza>();
            var set = _repositoryPizza.IDbSet(typeof(Pizza));
            foreach (var pizza in set)
            {
                pizzas.Add((Pizza) pizza);
            }
            return pizzas;
        }
    }
}
