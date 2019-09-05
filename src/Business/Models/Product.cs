using System;

namespace Business.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public Guid ProviderId { get; set; }
        public Provider Provider { get; set; }
        
    }

}
