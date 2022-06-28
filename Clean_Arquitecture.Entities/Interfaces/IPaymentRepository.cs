using Clean_Arquitecture.Entities.POCOEntities;
using Clean_Arquitecture.Entities.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.Entities.Interfaces
{
    public interface IPaymentRepository
    {
        void Create(Payment payment);
        IEnumerable<Payment> GetOrdersBySpecification(Specification<Payment> specification);
    }
}
