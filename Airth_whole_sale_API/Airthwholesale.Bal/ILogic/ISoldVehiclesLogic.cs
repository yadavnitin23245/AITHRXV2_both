using Airthwholesale.Bal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.ILogic
{
    public interface ISoldVehiclesLogic
    {
        /// <summary>
        /// For getting Get Sold Vehicles list By Date 
        /// </summary>
        /// <returns></returns>
        Task<JDPSoldVehicleListDTO> GetSoldVehiclesByDate(JDPSoldVehiclesParametersDTO soldVehiclesParametersDTO);


        string  UpdateSoldpricebyAPIIndatabase(string xmlapiresponse);
    }
}
