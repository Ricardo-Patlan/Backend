using System;
using backend.DTOs;
using backend.services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;
[Route("api/[controller]")]
    [ApiController]
public class SaleController: ControllerBase
{
    
   
        private IValidator<SaleInsertDto> _saleInsertValidator;
        private IValidator<SaleUpdateDto> _saleUpdateValidator;
        private ICommonServices<SaleDto, SaleInsertDto, SaleUpdateDto> _saleService;

        public SaleController(
            IValidator<SaleInsertDto> saleInsertValidator,
            IValidator<SaleUpdateDto> saleUpdateValidator,
            [FromKeyedServices("saleService")]
            ICommonServices<SaleDto, SaleInsertDto, SaleUpdateDto> saleService)
        {
            _saleInsertValidator = saleInsertValidator;
            _saleUpdateValidator = saleUpdateValidator;
            _saleService = saleService;
        }

        // GET: api/Sale
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> Get()
        {
            return Ok(await _saleService.Get());
        }

        // GET: api/Sale/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SaleDto>> GetById(int id)
        {
            var sale = await _saleService.GetById(id);
            return sale == null ? NotFound() : Ok(sale);
        }

        // POST: api/Sale
        [HttpPost]
        public async Task<ActionResult<SaleDto>> Add(SaleInsertDto saleInsertDto)
        {
            var validationResult = await _saleInsertValidator.ValidateAsync(saleInsertDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var saleDto = await _saleService.Add(saleInsertDto);

            if (saleDto == null)
            {
                return BadRequest(new { message = "La venta ya fue realizada ." });
            }

            return CreatedAtAction(nameof(GetById), new { id = saleDto.SaleId }, saleDto);
        }

        // PUT: api/Sale/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<SaleDto>> Update(int id, SaleUpdateDto saleUpdateDto)
        {
            var validationResult = await _saleUpdateValidator.ValidateAsync(saleUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors
                    .Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var saleDto = await _saleService.Update(id, saleUpdateDto);
            return saleDto == null ? NotFound() : Ok(saleDto);
        }

        // DELETE: api/Sale/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<SaleDto>> Delete(int id)
        {
            var saleDto = await _saleService.Delete(id);
            return saleDto == null ? NotFound() : Ok(saleDto);
        }
    }


