using System;
using Domain.Abstractions.Data;

namespace Domain.Entities
{
    public class MassageType : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        internal void Update(string name, int price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }
    }
}