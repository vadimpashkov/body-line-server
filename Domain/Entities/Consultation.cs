using System;
using Domain.Abstractions.Data;

namespace Domain.Entities
{
    public class Consultation : IEntity
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreation { get; set; }
    }
}