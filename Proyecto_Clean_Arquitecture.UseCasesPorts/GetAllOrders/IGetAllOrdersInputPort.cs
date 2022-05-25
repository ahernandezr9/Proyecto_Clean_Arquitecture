using Clean_Arquitecture.UseCasesDTOs.GetAllOrders;
using System.Threading.Tasks;

namespace Clean_Arquitecture.UseCasesPorts.GetAllOrders
{
    public interface IGetAllOrdersInputPort
    {
        Task Handle(GetAllOrdersParams getAllOrders);
    }
}
