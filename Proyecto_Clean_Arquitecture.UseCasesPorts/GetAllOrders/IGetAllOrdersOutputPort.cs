using Clean_Arquitecture.UseCasesDTOs.GetAllOrders;
using System.Threading.Tasks;

namespace Clean_Arquitecture.UseCasesPorts.GetAllOrders
{
    public interface IGetAllOrdersOutputPort
    {
        Task Handle(GetAllOrdersOutputPort output);
    }
}
