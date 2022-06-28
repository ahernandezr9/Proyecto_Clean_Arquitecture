using Clean_Arquitecture.Presenters;
using Clean_Arquitecture.Presenters.GetAllOrdersDTO;
using Clean_Arquitecture.UseCasesDTOs.GetAllOrders;
using Clean_Arquitecture.UseCasesPorts.GetAllOrders;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clean_Arquitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetOrdersByCustomerController
    {
        readonly IGetAllOrdersInputPort InputPort;
        readonly IGetAllOrdersOutputPort OutputPort;
        public GetOrdersByCustomerController(IGetAllOrdersInputPort inputPort, IGetAllOrdersOutputPort outputPort) =>
            (InputPort, OutputPort) = (inputPort, outputPort);

        [HttpPost("Get-Orders-By-Customer")]
        public async Task<GetAllOrdersOutput> GetAllOrdersByCustomer(GetAllOrdersParams input)
        {
            await InputPort.Handle(input);
            var Presenter = OutputPort as GetAllOrdersPresenter;
            return Presenter.Content;
        }
    }
}
