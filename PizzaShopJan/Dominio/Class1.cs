using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Dominio
{
    public class Pizza
    {
        public Pizza()
        {
            this.Ingredients = new HashSet<Ingredient>();
            this.Comments = new HashSet<Comment>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Ingredient> Ingredients { get; set; }
        public virtual ICollection<Comment> Comments {get; set;} 
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

    public interface ILogger : IDisposable
    {
        void Write(Pizza pizza);
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

        public void Write(Pizza pizza)
        {
            _repository.Write(pizza);
            _unitOfWork.SaveChanges();
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
    }
}
