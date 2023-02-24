using Airthwholesale.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.ILogic
{
    public interface IApiHistoryResponseLogic
    {

        Task SaveApihistory(JDPAPICallHistory data);

    }
}
