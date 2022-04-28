using Clean_Arquitecture.Entities.Interfaces;
using Clean_Arquitecture.Entities.POCOEntities;
using Clean_Arquitecture.Entities.Specifications;
using Clean_Arquitecture.Repositories.EFCore.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.Repositories.EFCore.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        readonly Clean_ArquitectureContext Context;
        public OrderRepository(Clean_ArquitectureContext context) =>
            Context = context;
        public void Create(Order order)
        {
            Context.Add(order);
        }

        public IEnumerable<Order> GetOrdersBySpecification(Specification<Order> specification)
        {
            var ExpressionDelegate = specification.Expression.Compile();
            return Context.Orders.Where(ExpressionDelegate);
        }
    }
}
