using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Data.Models;
using Airthwholesale.Repository.Repository;
using AirthwholesaleAPI.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Airthwholesale.Bal.Logic
{
    public class CommonLogic : ICommonLogic
    {

        #region Private properties



        private readonly IRepository<CountryDTO> _countryDTORepository;

        private readonly IRepository<StateDTO> _stateDTORepository;

        private readonly IRepository<CityDTO> _cityDTORepository;


        #endregion

        #region CTOR's
        public CommonLogic(
         IRepository<CountryDTO> countryDTORepository, IRepository<StateDTO> stateDTORepository,
         IRepository<CityDTO> cityDTORepository


            )
        {
            _countryDTORepository = countryDTORepository;
            _stateDTORepository = stateDTORepository;
            _cityDTORepository = cityDTORepository;
        }
        #endregion

        #region Interface  Methods

        public List<CountryDTO> GetCountryList()
        {
            try
            {
                string procName = SPROC_Names.UspGetCountryList.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _countryDTORepository.ExecuteWithJsonResult(procName, "CountryDTO", ParamsArray);

                return resultData != null ? resultData.ToList() : new List<CountryDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<StateDTO> GetStateListByCountryId(int Countryid)
        {
            try
            {
                string procName = SPROC_Names.UspGetStateListByCountryId.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@Countryid", Value = Countryid, DbType = System.Data.DbType.String };
                var resultData = _stateDTORepository.ExecuteWithJsonResult(procName, "StateDTO", ParamsArray);

                return resultData != null ? resultData.ToList() : new List<StateDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<CityDTO> GetCityListByCountryId(int Countryid, int StateId)
        {
            try
            {
                string procName = SPROC_Names.UspGetCityListByCountryId.ToString();
                var ParamsArray = new SqlParameter[4];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@Countryid", Value = Countryid, DbType = System.Data.DbType.String };
                ParamsArray[3] = new SqlParameter() { ParameterName = "@StateId", Value = StateId, DbType = System.Data.DbType.String };
                var resultData = _cityDTORepository.ExecuteWithJsonResult(procName, "CityDTO", ParamsArray);

                return resultData != null ? resultData.ToList() : new List<CityDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
