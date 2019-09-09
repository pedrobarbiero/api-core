using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class Provider : Entity
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public ProviderType ProviderType { get; set; }
        public Address Address { get; set; }
        public bool Active { get; set; }
        public IEnumerable<Product> Products { get; set; }        

    }
}
