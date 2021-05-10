using Domain.Abstractions.Data;

namespace Domain.Entities
{
    public class Photos : IEntity
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public string Alt { get; set; }
    }
}