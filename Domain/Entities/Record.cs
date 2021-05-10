using System;
using Domain.Abstractions.Data;

namespace Domain.Entities
{
    public class Record : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int MesseurId { get; set; }
        public Masseur Messeur { get; set; }

        public int MassageTypeId { get; set; }
        public MassageType MassageType { get; set; }

        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Cancelled { get; set; }
    }
}
