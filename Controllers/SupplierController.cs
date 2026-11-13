using System;
using backend.DTOs;
using backend.services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
[Route("api/[controller]")]
    [ApiController]
public class SupplierController: ControllerBase
{
    
   
        private IValidator<SupplierInsertDto> _supplierInsertValidator;
        private IValidator<SupplierUpdateDto> _supplierUpdateValidator;
        private ICommonServices<SupplierDTo, SupplierInsertDto, SupplierUpdateDto> _supplierService;

        public SupplierController(
            IValidator<SupplierInsertDto> supplierInsertValidator,
            IValidator<SupplierUpdateDto> supplierUpdateValidator,
            [FromKeyedServices("supplierService")]
            ICommonServices<SupplierDTo, SupplierInsertDto, SupplierUpdateDto> supplierService)
        {
            _supplierInsertValidator = supplierInsertValidator;
            _supplierUpdateValidator = supplierUpdateValidator;
            _supplierService = supplierService;
        }

        // GET: api/Supplier
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDTo>>> Get()
        {
            return Ok(await _supplierService.Get());
        }

        // GET: api/Supplier/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SupplierDTo>> GetById(int id)
        {
            var supplier = await _supplierService.GetById(id);
            return supplier == null ? NotFound() : Ok(supplier);
        }

        // POST: api/Supplier
        [HttpPost]
        public async Task<ActionResult<SupplierDTo>> Add(SupplierInsertDto supplierInsertDto)
        {
            var validationResult = await _supplierInsertValidator.ValidateAsync(supplierInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var supplierDto = await _supplierService.Add(supplierInsertDto);

            if (supplierDto == null)
            {
                return BadRequest(new { message = "La venta ya fue realizada ." });
            }

            return CreatedAtAction(nameof(GetById), new { id = supplierDto.SupplierId }, supplierDto);
        }

        // PUT: api/Supplier/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<SupplierDTo>> Update(int id, SupplierUpdateDto supplierUpdateDto)
        {
            var validationResult = await _supplierUpdateValidator.ValidateAsync(supplierUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var supplierDto = await _supplierService.Update(id, supplierUpdateDto);
            return supplierDto == null ? NotFound() : Ok(supplierDto);
        }

        // DELETE: api/Supplier/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<SupplierDTo>> Delete(int id)
        {
            var supplierDto = await _supplierService.Delete(id);
            return supplierDto == null ? NotFound() : Ok(supplierDto);
        }
    }


