using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.DTO.StockVehiclesUpdate;
namespace Airthwholesale.Bal.ILogic
{
    public interface IStockVehiclesUpdatesLogic
    {

        /// <summary>
        /// For getting Get Stock Vehicles Updates list
        /// </summary>
        /// <returns></returns>
        Task<JDPStockVehiclesUpdateAPIResponseDTO> GetStockVehiclesUpdates(JDPSoldVehiclesParametersDTO soldVehiclesParametersDTO);
    }
}
