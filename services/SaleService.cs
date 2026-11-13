using backend.DTOs;
using backend.Models;
using backend.Repository;

namespace backend.services;

public class SaleService : ICommonServices<SaleDto, SaleInsertDto, SaleUpdateDto>
{
    private readonly IRepository<Sale> _saleRepository;

    public SaleService(IRepository<Sale> saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<IEnumerable<SaleDto>> Get()
    {
        var sales = await _saleRepository.Get();
        return sales.Select(ToDto);
    }

    public async Task<SaleDto> GetById(int id)
    {
        var sale = await _saleRepository.GetById(id);

        if (sale == null)
        {
            return null;
        }

        return ToDto(sale);
    }

    public async Task<SaleDto> Add(SaleInsertDto saleInsertDto)
    {
        var sale = new Sale
        {
            Quantity = saleInsertDto.Quantity,
            UnitPrice = saleInsertDto.UnitPrice,
            InventoryId = saleInsertDto.InventoryId,
            ClientName = saleInsertDto.ClientName,
            SaleDate = saleInsertDto.SaleDate
        };

        await _saleRepository.Add(sale);
        await _saleRepository.Save();

        return ToDto(sale);
    }

    public async Task<SaleDto> Update(int id, SaleUpdateDto saleUpdateDto)
    {
        var sale = await _saleRepository.GetById(id);

        if (sale == null)
        {
            return null;
        }

        sale.Quantity = saleUpdateDto.Quantity;
        sale.UnitPrice = saleUpdateDto.UnitPrice;
        sale.InventoryId = saleUpdateDto.InventoryId;
        sale.ClientName = saleUpdateDto.ClientName;
        sale.SaleDate = saleUpdateDto.SaleDate;

        _saleRepository.Update(sale);
        await _saleRepository.Save();

        return ToDto(sale);
    }

    public async Task<SaleDto> Delete(int id)
    {
        var sale = await _saleRepository.GetById(id);

        if (sale == null)
        {
            return null;
        }

        var saleDto = ToDto(sale);

        _saleRepository.Delete(sale);
        await _saleRepository.Save();

        return saleDto;
    }

    // Obtener ventas por inventario
    public async Task<IEnumerable<SaleDto>> GetByInventoryId(int inventoryId)
    {
        if (_saleRepository is SaleRepository repo)
        {
            var sales = await repo.GetByInventoryId(inventoryId);
            return sales.Select(ToDto);
        }

        var all = await _saleRepository.Get();
        return all.Where(s => s.InventoryId == inventoryId).Select(ToDto);
    }

    private static SaleDto ToDto(Sale sale) => new SaleDto
    {
        SaleId = sale.SaleId,
        Quantity = sale.Quantity,
        UnitPrice = sale.UnitPrice,
        InventoryId = sale.InventoryId,
        ClientName = sale.ClientName,
        SaleDate = sale.SaleDate
    };
}
