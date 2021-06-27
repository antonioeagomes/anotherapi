using Another.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Another.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId);
        Task<IEnumerable<Product>> GetProductsSuppliers();
        Task<Product> GetProductAndSupplier(Guid id);
    }
}
