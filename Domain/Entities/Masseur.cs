using Domain.Abstractions.Data;

namespace Domain.Entities
{
    public class Masseur : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public string Occupation { get; set; }
        public string Description { get; set; }

        public void Update(string occupation, string description, string firstName, string lastName) 
        {
            Occupation = occupation;
            Description = description;

            User.Update(firstName, lastName);
        }
    }
}