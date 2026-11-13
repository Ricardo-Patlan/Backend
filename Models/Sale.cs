using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Sale
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SaleId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    public int InventoryId { get; set; }

    [MaxLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string ClientName { get; set; } = string.Empty;

    [Column(TypeName = "date")]
    public DateTime SaleDate { get; set; }

    [ForeignKey("InventoryId")]
    public virtual Inventory Inventory { get; set; } = null!;
}
