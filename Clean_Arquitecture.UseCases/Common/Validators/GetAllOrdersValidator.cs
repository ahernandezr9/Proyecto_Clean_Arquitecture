using Clean_Arquitecture.UseCasesDTOs.GetAllOrders;
using FluentValidation;

namespace Clean_Arquitecture.UseCases.Common.Validators
{
    public class GetAllOrdersValidator : AbstractValidator<GetAllOrdersParams>
    {
        public GetAllOrdersValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty().WithMessage("Debe proporcionar el identificador del cliente.");
        }
    }
}
