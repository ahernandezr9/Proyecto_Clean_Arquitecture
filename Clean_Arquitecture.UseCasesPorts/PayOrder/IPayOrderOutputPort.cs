using Clean_Arquitecture.UseCasesDTOs.PayOrder;
using System.Threading.Tasks;

namespace Clean_Arquitecture.UseCasesPorts.PayOrder
{
    public interface IPayOrderOutputPort
    {
        Task Handle(PayOrderOutputPort output);
    }
}
