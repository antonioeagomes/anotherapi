using Another.Business.Interfaces;
using Another.Business.Models;
using Another.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Another.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(AppDbContext context) : base(context) { }

        public async Task<Address> GetAddressBySupplier(Guid supplierId)
        {
            return await Context.Addresses.AsNoTracking()
                .FirstOrDefaultAsync(f => f.SupplierId == supplierId);
        }
    }
}
