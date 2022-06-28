using Clean_Arquitecture.Entities.Interfaces;
using Clean_Arquitecture.Entities.POCOEntities;
using Clean_Arquitecture.Entities.Specifications;
using Clean_Arquitecture.Repositories.EFCore.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Clean_Arquitecture.Repositories.EFCore.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        readonly Clean_ArquitectureContext Context;
        public PaymentRepository(Clean_ArquitectureContext context) => Context = context;

        public void Create(Payment payment)
        {
            Context.Add(payment);
        }

        public IEnumerable<Payment> GetOrdersBySpecification(Specification<Payment> specification)
        {
            var ExpressionDelegate = specification.Expression.Compile();
            return Context.Payments.Include(i => i.OrderId).Where(ExpressionDelegate);
        }
    }
}
