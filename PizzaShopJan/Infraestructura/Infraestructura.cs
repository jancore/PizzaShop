using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Dominio;

namespace Infraestructura
{
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
        List<object> Ingredients();
        List<object> Pizzas();
        object Pizza(Guid IdPizza);
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
            foreach (var IdIngredient in createPizza.Ingredients)
            {
                ingredients.Add(_repository.FindIngredient(IdIngredient));
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

        public List<object> Ingredients()
        {
            return _repository.GetIngredients();
        }

        public List<object> Pizzas()
        {
            return _repository.GetPizzas();
        }

        public object Pizza(Guid IdPizza)
        {
            return _repository.GetPizza(IdPizza);
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
            this.Pizza.Select(c => new
            {
                Id = c.Id,
                Name = c.Name,
                Ingredients = c.Ingredients.Select(i => new { i.Id, i.Name }).ToList()
            }
            ).ToList();
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
        Ingredient FindIngredient(Guid IdIngrediente);
        object GetPizza(Guid IdPizza);
        List<object> GetIngredients();
        List<object> GetPizzas();
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

        public Ingredient FindIngredient(Guid IdIngrediente)
        {
            var set = _repositoryPizza.IDbSet(typeof(Ingredient));
            var ingrediente = (Ingredient)set.Find(IdIngrediente);
            return ingrediente;
        }

        public List<object> GetIngredients()
        {
            List<object> ingredientes = new List<object>();
            var set = _repositoryPizza.IDbSet(typeof(Ingredient));
            foreach (Ingredient ingredient in set)
            {
                ingredientes.Add(new { Id = ingredient.Id, Name = ingredient.Name });
            }
            return ingredientes;
        }

        public List<object> GetPizzas()
        {
            List<object> pizzas = new List<object>();
            var set = _repositoryPizza.IDbSet(typeof(Pizza));
            foreach (Pizza pizza in set)
            {
                pizzas.Add(new
                {
                    Id = pizza.Id,
                    Name = pizza.Name,
                    Ingredients = pizza.GetIngredients(),
                    File = pizza.File,
                    MIME = pizza.MIMEType,
                    TotalCost = pizza.TotalCost()
                });
            }
            return pizzas;
        }

        public object GetPizza(Guid IdPizza)
        {
            var set = _repositoryPizza.IDbSet(typeof(Pizza));
            var pizza = (Pizza)set.Find(IdPizza);
            return new
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Ingredients = pizza.GetIngredients(),
                File = pizza.File,
                MIME = pizza.MIMEType,
                TotalCost = pizza.TotalCost()
            };
        }
    }
}
