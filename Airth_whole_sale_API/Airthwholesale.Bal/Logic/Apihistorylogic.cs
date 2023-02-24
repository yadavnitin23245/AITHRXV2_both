using Airthwholesale.Bal.ILogic;
using Airthwholesale.Data.Models;
using Airthwholesale.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.Logic
{
    public class Apihistorylogic : IApiHistoryResponseLogic
    {
        private readonly IRepository<JDPAPICallHistory> _JDPAPICallHistory;

        public Apihistorylogic(IRepository<JDPAPICallHistory> JDPAPICallHistory)
        {
            _JDPAPICallHistory = JDPAPICallHistory;

        }

        public async Task SaveApihistory(JDPAPICallHistory data)
        {
          await  _JDPAPICallHistory.JDP_InsertAsync(data);
        }
    }
}
