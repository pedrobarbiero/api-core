using System;

namespace Business.Models
{
    public class Entity
    {
        public Entity()
        {
            Id = new Guid();
        }
        public Guid Id { get; set; }
    }
}
