using System;
using System.ComponentModel.DataAnnotations;

namespace Another.Api.Dtos
{
    public class AddressDto
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string ZipCode { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
