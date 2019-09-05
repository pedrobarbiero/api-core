using System;

namespace Business.Models
{
    public class Entity
    {
        public Entity()
        {
            Id = new Guid();
        }
        Guid Id { get; set; }
    }
}
