using System;

namespace backend.DTOs;

public class SaleUpdateDto
{
    public int SaleId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int InventoryId { get; set; }

    public string ClientName { get; set; } = string.Empty;

    public DateTime SaleDate { get; set; }
}
