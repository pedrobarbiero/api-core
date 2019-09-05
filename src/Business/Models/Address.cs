using System;

namespace Business.Models
{
    public class Address : Entity
    {
        public Guid ProviderId { get; set; }
        public string Place { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string ZipCode { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public Provider Provider { get; set; }




    }
}
