using Clean_Arquitecture.UseCases.Common.Ports;
using Clean_Arquitecture.UseCasesDTOs.CreateOrder;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.UseCases.CreateOrder
{
    public class CreateOrderInputPort : IInputPort<CreateOrderParams, int>
    {
        public CreateOrderParams RequestData { get; }

        public IOutputPort<int> OutputPort { get; }

        public CreateOrderInputPort(CreateOrderParams requestData, IOutputPort<int> outputPort) => (RequestData, OutputPort) = (requestData, outputPort);
    }
}
