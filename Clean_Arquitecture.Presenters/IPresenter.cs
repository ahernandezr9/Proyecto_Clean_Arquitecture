using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Arquitecture.Presenters
{
    public interface IPresenter<FormatDataType>
    {
        public FormatDataType Content { get; }
    }
}
