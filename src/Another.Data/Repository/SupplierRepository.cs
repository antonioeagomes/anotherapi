using Another.Business.Interfaces;
using Another.Business.Models;
using Another.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Another.Data.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Supplier> GetSupplierAddressProducts(Guid id)
        {
            return await Context.Suppliers.AsNoTracking()
                    .Include(s => s.Address)
                    .Include(s => s.Products)
                    .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Supplier> GetSupplierAndAddress(Guid id)
        {
            return await Context.Suppliers.AsNoTracking()
                    .Include(s => s.Address)
                    .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
