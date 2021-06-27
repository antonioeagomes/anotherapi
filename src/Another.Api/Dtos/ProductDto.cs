using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Another.Api.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public Guid SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImageUpload { get; set; }
        public IFormFile ImageStreamUpload { get; set; }
        public decimal Price { get; set; }
        
        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        [ScaffoldColumn(false)]
        public string SupplierName { get; set; }
    }
}
