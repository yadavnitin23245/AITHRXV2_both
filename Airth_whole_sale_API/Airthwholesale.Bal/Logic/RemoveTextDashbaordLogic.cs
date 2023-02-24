using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.Helpers;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Data;
using Airthwholesale.Data.Models;
using Airthwholesale.Repository.Repository;
using AirthwholesaleAPI.Common.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.Logic
{
    public class RemoveTextDashbaordLogic : IRemoveTextDashbaordLogic
    {
        private readonly IRepository<DashboardFilesDetailsViewModelDTO> _DashboardFilesDetailsViewModelDTO;
        private readonly IRepository<CleanImagesDetailsDTO> _CleanImagesDetailsDTO;
        private readonly IRepository<JDPListOfPhotos> _JDPListOfPhotos;

        private readonly JDPAPIDbContext _JDPcontext;

        public RemoveTextDashbaordLogic(JDPAPIDbContext JDPcontext,
            IRepository<DashboardFilesDetailsViewModelDTO> DashboardFilesDetailsViewModelDTO,
            IRepository<CleanImagesDetailsDTO> cleanImagesDetailsDTO,
IRepository<JDPListOfPhotos> JDPListOfPhotos
            )
        {
            _DashboardFilesDetailsViewModelDTO = DashboardFilesDetailsViewModelDTO;
            _JDPcontext = JDPcontext;
            _CleanImagesDetailsDTO = cleanImagesDetailsDTO;
            _JDPListOfPhotos= JDPListOfPhotos;
        }
        public DashboardFilesDetailsViewModelDTO GetRemovtextDashboard(string opcode)
        {
            try
            {
                string procName = SPROC_Names.UspGetRemoveTextDashboard.ToString();
                var ParamsArray = new SqlParameter[1];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@Opcode", Value = "1", DbType = System.Data.DbType.String };
                
                var resultData = _DashboardFilesDetailsViewModelDTO.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "removetextDashboard", ParamsArray).FirstOrDefault();
                return resultData;
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        public List<CleanImagesDetailsDTO> GetCleanImagesDetails(string opcode)
        {
            try
            {
                string procName = SPROC_Names.UspGetCleanImagesDetails.ToString();
                var ParamsArray = new SqlParameter[1];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@opcode", Value = "", DbType = System.Data.DbType.String };
               
                var resultData = _CleanImagesDetailsDTO.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "CleanImagesDetailsDTO", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<CleanImagesDetailsDTO>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }

        public List<JDPListOfPhotos> GetimagesForClean(string opcode)
        {
            try
            {
                string procName = SPROC_Names.UspGet_UncleanImagefor_clean.ToString();
                var ParamsArray = new SqlParameter[1];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@opcode", Value = "", DbType = System.Data.DbType.String };

                var resultData = _JDPListOfPhotos.ExecuteWithJsonResult_FROM_JDPSERVER(procName, "Image_for_clean", ParamsArray);
                return resultData != null ? resultData.ToList() : new List<JDPListOfPhotos>();
            }
            catch (AppException ex)
            {
                throw;
            }
        }
    }
}
