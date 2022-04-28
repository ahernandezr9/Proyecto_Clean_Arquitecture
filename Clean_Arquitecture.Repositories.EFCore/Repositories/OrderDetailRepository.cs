using Clean_Arquitecture.Entities.Interfaces;
using Clean_Arquitecture.Entities.POCOEntities;
using Clean_Arquitecture.Repositories.EFCore.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.Repositories.EFCore.Repositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        readonly Clean_ArquitectureContext Context;
        public OrderDetailRepository(Clean_ArquitectureContext context) =>
            Context = context;
        public void Create(OrderDetail orderDetail)
        {
            Context.Add(orderDetail);
        }
    }
}
