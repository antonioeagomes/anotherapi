using Another.Business.Interfaces;
using Another.Business.Models;
using Another.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Another.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Product> GetProductAndSupplier(Guid id)
        {
            return await Context.Products.AsNoTracking().Include(s => s.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId)
        {
            return await Get(p => p.SupplierId == supplierId);
        }

        public async Task<IEnumerable<Product>> GetProductsSuppliers()
        {
            return await Context.Products.AsNoTracking().Include(s => s.Supplier)
                .OrderBy(p => p.Name).ToListAsync();
        }
    }
}
