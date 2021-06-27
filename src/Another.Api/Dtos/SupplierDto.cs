using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Another.Api.Dtos
{
    public class SupplierDto
    {
        
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Must have a name")]
        public string Name { get; set; }
        public string Document { get; set; }
        public int SupplierType { get; set; }
        public AddressDto Address { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
