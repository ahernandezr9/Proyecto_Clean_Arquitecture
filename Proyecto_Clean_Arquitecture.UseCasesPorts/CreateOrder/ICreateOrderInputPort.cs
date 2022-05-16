using Clean_Arquitecture.UseCasesDTOs.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Clean_Arquitecture.UseCasesPorts.CreateOrder
{
    public interface ICreateOrderInputPort
    {
        Task Handle(CreateOrderParams order);
    }
}
