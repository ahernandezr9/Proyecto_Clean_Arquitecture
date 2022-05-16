using Clean_Arquitecture.UseCasesDTOs.CreateOrder;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.UseCases.Common.Validators
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderParams>
    {
        public CreateOrderValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty().WithMessage("Debe proporcionar el identificador del cliente.");
            RuleFor(c => c.ShipAddress).NotEmpty().WithMessage("Debe proporcionar la direccion de envío.");
            RuleFor(c => c.ShipCity).NotEmpty().MinimumLength(3).WithMessage("Debe proporcionar al menos 3 caracteres del nombre de la ciudad.");
            RuleFor(c => c.ShipCountry).NotEmpty().MinimumLength(3).WithMessage("Debe proporcionar al menos 3 caracteres del nombre del pais.");
            RuleFor(c => c.OrderDetails).Must(d => d != null && d.Any()).WithMessage("Debe especificarse los productos de la orden.");
        }
    }
}
