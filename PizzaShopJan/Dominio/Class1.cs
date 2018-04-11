using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Pizza
    {
        public List<string> ingredientes = new List<string>() { };
        public List<string> comments = new List<string>() { };
        public string Name { get; set; }
    }

    public interface IRepository
    {
        void Add(Pizza pizza);
        void Update(Pizza pizza);
    }
}
