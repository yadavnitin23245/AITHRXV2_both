using Airthwholesale.Bal.DTO;
using Airthwholesale.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.ILogic
{
    public interface ICommonLogic
    {
        
        List<CountryDTO> GetCountryList();

        List<StateDTO> GetStateListByCountryId(int Countryid);

        List<CityDTO> GetCityListByCountryId(int Countryid, int StateId);


    }
}
