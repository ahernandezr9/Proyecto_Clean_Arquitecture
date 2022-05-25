using Clean_Arquitecture.UseCasesDTOs.CreateOrder;
using System.Threading.Tasks;

namespace Clean_Arquitecture.UseCasesPorts.CreateOrder
{
    public interface ICreateOrderInputPort
    {
        Task Handle(CreateOrderParams order);
    }
}
