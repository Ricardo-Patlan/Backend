using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class Supplier
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SupplierId { get; set; }

    [MaxLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string Country { get; set; } = string.Empty;

    [MaxLength(20)]
    [Column(TypeName = "varchar(20)")]
    public string Phone { get; set; } = string.Empty;

    public int BrandId { get; set; }

    [ForeignKey("BrandId")]
    public virtual Brand Brand { get; set; } = null!;
}
