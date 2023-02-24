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
    public class exceptionHandlingLogic : IexceptionHandlingLogic
    {
        private readonly IRepository<JDPExceptionLoggingToDataBase> _exceptiondata;


        public exceptionHandlingLogic(IRepository<JDPExceptionLoggingToDataBase> exceptiondata)
        {
            _exceptiondata = exceptiondata;
        }

        public string InsertException(JDPExceptionLoggingToDataBase exception)
        {

            _exceptiondata.Insert(exception);
            return "done";


        }
    }
}
