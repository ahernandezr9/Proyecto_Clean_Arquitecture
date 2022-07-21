using Clean_Arquitecture.Entities.POCOEntities;
using Clean_Arquitecture.Entities.Specifications;
using System.Collections.Generic;

namespace Clean_Arquitecture.Entities.Interfaces
{
    public interface IPaymentRepository
    {
        void Create(Payment payment);
        IEnumerable<Payment> GetPaymentsBySpecification(Specification<Payment> specification);
    }
}
