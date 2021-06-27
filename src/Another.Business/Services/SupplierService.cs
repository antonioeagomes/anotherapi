using Another.Business.Interfaces;
using Another.Business.Models;
using Another.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Another.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;
        public SupplierService(INotificator notificator,
            ISupplierRepository supplierRepository,
            IAddressRepository addressRepository) : base(notificator)
        {
            _addressRepository = addressRepository;
            _supplierRepository = supplierRepository;
        }

        public async Task Add(Supplier supplier)
        {
            if (!Validate(new SupplierValidation(), supplier)
                || !Validate(new AddressValidation(), supplier.Address)) return;

            if (_supplierRepository.Get(f => f.Document == supplier.Document).Result.Any())
            {
                Notify("Supplier already exists");
                return;
            }

            await _supplierRepository.Add(supplier);
            
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
            _addressRepository?.Dispose();
        }

        public async Task Remove(Guid id)
        {
            if (_supplierRepository.GetSupplierAddressProducts(id).Result.Products.Any())
            {
                Notify("You must remove its products first!");
                return;
            }

            var address = await _addressRepository.GetAddressBySupplier(id);

            if (address != null)
            {
                await _addressRepository.Remove(address.Id);
            }

            await _supplierRepository.Remove(id);
            
        }

        public async Task Update(Supplier supplier)
        {
            if (!Validate(new SupplierValidation(), supplier)) return;

            if (_supplierRepository.Get(f => f.Document == supplier.Document && f.Id != supplier.Id).Result.Any())
            {
                Notify("There is already a supplier with this document.");
                return;
            }

            await _supplierRepository.Update(supplier);
            return;
        }

        public async Task UpdateAddress(Address address)
        {
            if (!Validate(new AddressValidation(), address)) return;

            await _addressRepository.Update(address);
        }
    }
}
