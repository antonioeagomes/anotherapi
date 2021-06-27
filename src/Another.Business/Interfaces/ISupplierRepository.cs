using Another.Business.Models;
using System;
using System.Threading.Tasks;

namespace Another.Business.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> GetSupplierAndAddress(Guid id);
        Task<Supplier> GetSupplierAddressProducts(Guid id);
    }
}
