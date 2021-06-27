using Another.Api.Dtos;
using Another.Business.Interfaces;
using Another.Business.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Another.Api.Controllers
{
    [Route("api/[controller]")]
    public class SuppliersController : BaseController
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        public SuppliersController(ISupplierRepository supplierRepository, 
            ISupplierService supplierService,
            IAddressRepository addressRepository,
            IMapper mapper,
            INotificator notificator) : base(notificator)
        {
            _supplierRepository = supplierRepository;
            _supplierService = supplierService;
            _addressRepository = addressRepository;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetAll()
        {
            var suppliers = _mapper.Map<IEnumerable<SupplierDto>>(await _supplierRepository.GetAll());
            return Ok(suppliers);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetById(Guid id)
        {
            var supplier = await GetSupplierById(id);

            if (supplier == null) return NotFound("Supplier not found");           

            return Ok(supplier);
        }

        [HttpPost]
        public async Task<ActionResult> Add(SupplierDto supplierDto)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var supplier = _mapper.Map<Supplier>(supplierDto);

            await _supplierService.Add(supplier);

            return CustomResponse(supplierDto);

        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, SupplierDto supplierDto)
        {
            if (id != supplierDto.Id)
            {
                NotifyError("Informed ID is different of suppliers");
                return CustomResponse();
            }
            
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.Update(_mapper.Map<Supplier>(supplierDto));

            return CustomResponse(supplierDto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<SupplierDto>> Delete(Guid id)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var supplier = await GetSupplierAddressById(id);

            if (supplier == null) return NotFound();

            await _supplierService.Remove(id);

            return CustomResponse(supplier);
            
        }

        [HttpGet("address/{id:guid}")]
        public async Task<AddressDto> GetAddressById(Guid id)
        {
            return _mapper.Map<AddressDto>(await _addressRepository.GetById(id));
        }

        [HttpPut("update-address/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, AddressDto addressDto)
        {
            if (id != addressDto.Id)
            {
                NotifyError("Ids are differents");
                return CustomResponse();
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _supplierService.UpdateAddress(_mapper.Map<Address>(addressDto));

            return CustomResponse(addressDto);
        }


        private async Task<SupplierDto> GetSupplierById(Guid id)
        {
            return _mapper.Map<SupplierDto>(await _supplierRepository.GetSupplierAddressProducts(id));
        }

        private async Task<SupplierDto> GetSupplierAddressById(Guid id)
        {
            return _mapper.Map<SupplierDto>(await _supplierRepository.GetSupplierAndAddress(id));
        }
    }
}
