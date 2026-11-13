using System;

namespace backend.DTOs;

public class SupplierInsertDto
{
        public int SupplierId { get; set; }

    public string Name { get; set; }

    public string Country  { get; set; }
      public string Phone  { get; set; }

    public int BrandId { get; set; }


}
