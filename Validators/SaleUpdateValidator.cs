using backend.DTOs;
using FluentValidation;

namespace backend.Validators;

public class SaleUpdateValidator : AbstractValidator<SaleUpdateDto>
{
    public SaleUpdateValidator()
    {
        RuleFor(x => x.SaleId).GreaterThan(0).WithMessage("El id debe ser mayor a 0");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");
        RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("El precio unitario debe ser mayor a 0");
        RuleFor(x => x.InventoryId).GreaterThan(0).WithMessage("El inventario es obligatorio");
        RuleFor(x => x.ClientName).NotEmpty().WithMessage("El nombre del cliente es obligatorio")
            .MaximumLength(100).WithMessage("El nombre del cliente no debe pasar de 100 caracteres");
        RuleFor(x => x.SaleDate).NotEmpty().WithMessage("La fecha de venta es obligatoria");
    }
}
