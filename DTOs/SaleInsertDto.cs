using System;

namespace backend.DTOs;

public class SaleInsertDto
{
    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int InventoryId { get; set; }

    public string ClientName { get; set; } = string.Empty;

    public DateTime SaleDate { get; set; }
}
