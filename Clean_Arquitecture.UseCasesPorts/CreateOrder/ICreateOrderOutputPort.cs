using System.Threading.Tasks;

namespace Clean_Arquitecture.UseCasesPorts.CreateOrder
{
    public interface ICreateOrderOutputPort
    {
        Task Handle(int orderId);
    }
}
