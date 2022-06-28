using Clean_Arquitecture.Presenters;
using Clean_Arquitecture.Presenters.PayOrderDTO;
using Clean_Arquitecture.UseCasesDTOs.PayOrder;
using Clean_Arquitecture.UseCasesPorts.PayOrder;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Clean_Arquitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentByOrderController
    {
        readonly IPayOrderInputPort InputPort;
        readonly IPayOrderOutputPort OutputPort;

        public PaymentByOrderController(IPayOrderInputPort inputPort, IPayOrderOutputPort outputPort)
        {
            InputPort = inputPort;
            OutputPort = outputPort;
        }

        [HttpPost("Payment-By-Order")]
        public async Task<PayOrderOutput> PayByOrder(PayOrderParams input)
        {
            await InputPort.Handle(input);
            var Presenter = OutputPort as PayOrderPresenter;
            return Presenter.Content;
            //return "OK ENTRO";
        }
    }
}
