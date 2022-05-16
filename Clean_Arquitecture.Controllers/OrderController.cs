using Clean_Arquitecture.Presenters;
using Clean_Arquitecture.UseCases.CreateOrder;
using Clean_Arquitecture.UseCasesDTOs.CreateOrder;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Clean_Arquitecture.UseCasesPorts.CreateOrder;
using System;
using System.Threading.Tasks;

namespace Clean_Arquitecture.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController
    {
        readonly ICreateOrderInputPort InputPort;
        readonly ICreateOrderOutputPort OutputPort;
        public OrderController(ICreateOrderInputPort inputPort, ICreateOrderOutputPort outputPort) =>
            (InputPort, OutputPort) = (inputPort, outputPort);

        [HttpPost("create-order")]
        public async Task<string> CreateOrder(CreateOrderParams orderParams)
        {
            await InputPort.Handle(orderParams);
            var Presenter = OutputPort as CreateOrderPresenter;
            return Presenter.Content;
        }
    }
}
