using Clean_Arquitecture.UseCasesDTOs.PayOrder;
using System.Threading.Tasks;

namespace Clean_Arquitecture.UseCasesPorts.PayOrder
{
    public interface IPayOrderInputPort
    {
        Task Handle(PayOrderParams payOrders);
    }
}
