using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.Helpers;
using Airthwholesale.Bal.ILogic;
using Airthwholesale.Bal.Model;
using Airthwholesale.Data;
using Airthwholesale.Data.Models;
using Airthwholesale.Repository.Repository;
using AirthwholesaleAPI.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data.SqlClient;

namespace Airthwholesale.Bal.Logic
{
    public class AdminLogic : IAdminLogic
    {

        #region Private properties
        private AirthwholesaleDbContext _context;

        private IJwtUtils _jwtUtils;
        private readonly AppSettingsDTO _appSettings;
        private readonly IRepository<DGroupDTO> _groupDTORepository;
        private readonly IRepository<DGroup> _groupRepository;

        private readonly IRepository<AspNetRolesDTO> _rolesDTORepository;
        private readonly IRepository<AspNetRoles> _rolesRepository;

        //private readonly IRepository<AspNetUsersDTO> _usersDTORepository;
        //private readonly IRepository<AspNetUsers> _userRepository;

        private readonly IRepository<DealersListDTO> _dealersDTORepository;
        private readonly IRepository<DealersList> _dealersRepository;

        private readonly IRepository<SubscriptionDTO> _SubscriptionDTORepository;


        private readonly IRepository<AithrPositions> _aithrPositionsRepository;
        private readonly IRepository<AithrPositionsDTO> _aithrPositionsDTORepository;

        #endregion

        #region CTOR's
        public AdminLogic(

              IRepository<DGroup> groupRepository,
              IRepository<DGroupDTO> groupDTORepository,
              IJwtUtils jwtUtils,
              IOptions<AppSettingsDTO> appSettings,
              AirthwholesaleDbContext context,

               //IRepository<AspNetRoles> rolesRepository,
               //IRepository<AspNetRolesDTO> rolesDTORepository,
               //IRepository<AspNetUsersDTO> usersDTORepository,
               //IRepository<AspNetUsers> userRepository,
               IRepository<DealersListDTO> dealersDTORepository,
               IRepository<DealersList> dealersRepository,
               IRepository<SubscriptionDTO> SubscriptionDTORepository,
               IRepository<AithrPositions> aithrPositionsRepository,
               IRepository<AithrPositionsDTO> aithrPositionsDTORepository
            )
        {

            _groupDTORepository = groupDTORepository;
            _groupRepository = groupRepository;
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
            //_rolesRepository = rolesRepository;
            //_rolesDTORepository = rolesDTORepository;
            //_usersDTORepository = usersDTORepository;
            //_userRepository = userRepository;
            _dealersDTORepository = dealersDTORepository;
            _dealersRepository = dealersRepository;
            _SubscriptionDTORepository = SubscriptionDTORepository;
            _aithrPositionsRepository = aithrPositionsRepository;
            _aithrPositionsDTORepository = aithrPositionsDTORepository;
        }

        #endregion

        #region Interface  Methods


        #region   MethodsforGroups

        /// <summary>
        /// Add Groups 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public async Task<string> AddGroups(DGroupDTO obj)
        {
            try
            {
                var _groupModel = await _groupRepository.Where(x => x.GroupName == obj.GroupName).ToListAsync();
                if (_groupModel.Count > 0)
                {
                    return "AlreadyExist";
                }
                DGroup groupObj = MapDTOToModel(obj);
                await _groupRepository.InsertAsync(groupObj);
                await _groupRepository.SaveChangesAsync();
                return groupObj.id.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Update Groups 
        /// </summary>
        /// <param name="Groups"></param>
        /// <returns></returns>
        public async Task<string> UpdateGroups(DGroupDTO model)
        {
            try
            {
                DGroup Obj = this.MapDTOToModel(model);
                var entity = await _groupRepository.Where(i => i.id == Obj.id).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.GroupName = Obj.GroupName;
                    entity.UpdatedBy = Obj.UpdatedBy;
                    entity.UpdateDate = Obj.UpdateDate;

                    await _groupRepository.UpdateAsync(entity);
                    await _groupRepository.SaveChangesAsync();

                    return entity.id.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return "NotFound";
        }


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteGroups(int id)
        {
            try
            {
                var m = await _groupRepository.Where(u => u.id == id).FirstOrDefaultAsync();
                if (m != null)
                {
                    await _groupRepository.DeleteAsync(m);
                    return "Deleted";
                }
                else
                    return "notfound";
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// Get ALL Groups
        /// </summary>
        /// <returns></returns>
        public List<DGroupDTO> GetAllGroups()
        {
            try
            {
                string procName = SPROC_Names.UspGetAllGroups.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _groupDTORepository.ExecuteWithJsonResult(procName, "GroupList", ParamsArray);

                return resultData != null ? resultData.ToList() : new List<DGroupDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DGroupDTO> GetGroupByIds(string id)
        {
            try
            {
                string procName = SPROC_Names.UspGetGroupById.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@id", Value = id, DbType = System.Data.DbType.String };
                var resultData = _groupDTORepository.ExecuteWithJsonResult(procName, "GroupList", ParamsArray);

                return resultData != null ? resultData.ToList() : new List<DGroupDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


        #region  Methods for Roles

        ///// <summary>
        ///// AddRoles 
        ///// </summary>
        ///// <param name="Role"></param>
        ///// <returns></returns>
        //public async Task<string> AddRoles(AspNetRolesDTO obj)
        //{
        //    try
        //    {
        //        var _roleModel = await _rolesRepository.Where(x => x.Name == obj.Name).ToListAsync();
        //        if (_roleModel.Count > 0)
        //        {
        //            return "AlreadyExist";
        //        }
        //        AspNetRoles roleObj = MapRoleDTOToModel(obj);
        //        await _rolesRepository.InsertAsync(roleObj);
        //        await _rolesRepository.SaveChangesAsync();
        //        return roleObj.Id.ToString();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// Update Roles 
        ///// </summary>
        ///// <param name="Roles"></param>
        ///// <returns></returns>
        //public async Task<string> UpdateRoles(AspNetRolesDTO model)
        //{
        //    try
        //    {
        //        AspNetRoles Obj = this.MapRoleDTOToModel(model);
        //        var entity = await _rolesRepository.Where(i => i.Id == Obj.Id).FirstOrDefaultAsync();
        //        if (entity != null)
        //        {
        //            entity.Name = Obj.Name;

        //            await _rolesRepository.UpdateAsync(entity);
        //            await _rolesRepository.SaveChangesAsync();

        //            return entity.Id.ToString();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return "NotFound";
        //}

        ///// <summary>
        ///// Delete roles
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task<string> DeleteRoles(int id)
        //{
        //    try
        //    {
        //        var m = await _rolesRepository.Where(u => u.Id == id.ToString()).FirstOrDefaultAsync();
        //        if (m != null)
        //        {
        //            await _rolesRepository.DeleteAsync(m);
        //            return "Deleted";
        //        }
        //        else
        //            return "notfound";
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}



        ///// <summary>
        ///// Get ALL Roles
        ///// </summary>
        ///// <returns></returns>
        //public List<AspNetRolesDTO> GetAllRoles()
        //{
        //    try
        //    {
        //        string procName = SPROC_Names.UspGetAllRoles.ToString();
        //        var ParamsArray = new SqlParameter[2];
        //        ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
        //        ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
        //        var resultData = _rolesDTORepository.ExecuteWithJsonResult(procName, "AspNetRolesDTO", ParamsArray);

        //        return resultData != null ? resultData.ToList() : new List<AspNetRolesDTO>();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public List<AspNetRolesDTO> GetRolesByIds(string id)
        //{
        //    try
        //    {
        //        string procName = SPROC_Names.UspGetRolesById.ToString();
        //        var ParamsArray = new SqlParameter[3];
        //        ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
        //        ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
        //        ParamsArray[2] = new SqlParameter() { ParameterName = "@id", Value = id, DbType = System.Data.DbType.String };
        //        var resultData = _rolesDTORepository.ExecuteWithJsonResult(procName, "AspNetRolesDTO", ParamsArray);

        //        return resultData != null ? resultData.ToList() : new List<AspNetRolesDTO>();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        #endregion

        #region users
        ///// <summary>
        ///// AddUsers 
        ///// </summary>
        ///// <param name="User"></param>
        ///// <returns></returns>
        //public async Task<string> AddUsers(AspNetUsersDTO obj)
        //{
        //    try
        //    {
        //        var _userModel = await _userRepository.Where(x => x.Email == obj.Email).ToListAsync();
        //        if (_userModel.Count > 0)
        //        {
        //            return "AlreadyExist";
        //        }
        //        AspNetUsers userObj = MapUsersDTOToModel(obj);
        //        await _userRepository.InsertAsync(userObj);
        //        await _userRepository.SaveChangesAsync();
        //        return userObj.Id.ToString();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// Update Users 
        ///// </summary>
        ///// <param name="users"></param>
        ///// <returns></returns>
        //public async Task<string> UpdateUsers(AspNetUsersDTO model)
        //{
        //    try
        //    {
        //        AspNetUsers Obj = this.MapUsersDTOToModel(model);
        //        var entity = await _userRepository.Where(i => i.Email == Obj.Email).FirstOrDefaultAsync();
        //        if (entity != null)
        //        {
        //            entity.UserName = Obj.UserName;
        //            entity.NormalizedUserName = Obj.UserName;
        //            entity.Email = Obj.Email;
        //            entity.NormalizedEmail = Obj.Email;                   
        //            entity.PhoneNumber = Obj.PhoneNumber;
        //            entity.LockoutEnabled = Obj.LockoutEnabled;
        //            entity.AccessFailedCount = Obj.AccessFailedCount;
        //            entity.Userrole = Obj.Userrole;
        //            entity.firstName = Obj.firstName;
        //            entity.lastName = Obj.lastName;
        //            entity.phone = Obj.PhoneNumber;

        //            await _userRepository.UpdateAsync(entity);
        //            await _userRepository.SaveChangesAsync();

        //            return entity.Id.ToString();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return "NotFound";
        //}

        ///// <summary>
        ///// Delete Users
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public async Task<string> DeleteUsers(string id)
        //{
        //    try
        //    {
        //        var m = await _userRepository.Where(u => u.Id == id.ToString()).FirstOrDefaultAsync();
        //        if (m != null)
        //        {
        //            await _userRepository.DeleteAsync(m);
        //            return "Deleted";
        //        }
        //        else
        //            return "notfound";
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        ///// <summary>
        ///// Get ALL Users
        ///// </summary>
        ///// <returns></returns>
        //public List<AspNetUsersDTO> GetAllUsers()
        //{
        //    try
        //    {
        //        string procName = SPROC_Names.UspGetAllUsersDetails.ToString();
        //        var ParamsArray = new SqlParameter[2];
        //        ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
        //        ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
        //        var resultData = _usersDTORepository.ExecuteWithJsonResult(procName, "AspNetUsersDTO", ParamsArray);

        //        return resultData != null ? resultData.ToList() : new List<AspNetUsersDTO>();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public List<AspNetUsersDTO> GetUsersByIds(string id)
        //{
        //    try
        //    {
        //        string procName = SPROC_Names.UspGetUsersById.ToString();
        //        var ParamsArray = new SqlParameter[3];
        //        ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
        //        ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
        //        ParamsArray[2] = new SqlParameter() { ParameterName = "@id", Value = id, DbType = System.Data.DbType.String };
        //        var resultData = _usersDTORepository.ExecuteWithJsonResult(procName, "AspNetUsersDTO", ParamsArray);

        //        return resultData != null ? resultData.ToList() : new List<AspNetUsersDTO>();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        #endregion

        #region Dealers


        public async Task<string> AddDealers(DealersListDTO obj)
        {
            try
            {
                var _dealerModel = await _dealersRepository.Where(x => x.DealerName == obj.DealerName).ToListAsync();
                if (_dealerModel.Count > 0)
                {
                    return "AlreadyExist";
                }
                DealersList dealerObj = MapDealersDTOToModel(obj);
                await _dealersRepository.InsertAsync(dealerObj);
                await _dealersRepository.SaveChangesAsync();
                return dealerObj.id.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Dealers 
        /// </summary>
        /// <param name="Dealers"></param>
        /// <returns></returns>
        public async Task<string> UpdateDealers(DealersListDTO model)
        {
            try
            {
                DealersList Obj = this.MapDealersDTOToModel(model);
                var entity = await _dealersRepository.Where(i => i.DealerName == Obj.DealerName).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.DealerName = Obj.DealerName;
                    entity.DealerCity = Obj.DealerCity;
                    entity.DealerRegion = Obj.DealerRegion;
                    entity.DealerAddress = Obj.DealerAddress;
                    entity.PostalCode = Obj.PostalCode;
                    entity.ActiveUntil = Obj.ActiveUntil;
                    entity.ActiveStart = Obj.ActiveStart;
                    entity.DealerPhone = Obj.DealerPhone;
                    entity.DGroupId = Obj.DGroupId;
                    entity.GstNumber = Obj.GstNumber;
                    entity.DealerEmail = Obj.DealerEmail;
                    entity.DealerEmail = Obj.DealerEmail;
                    entity.UpdatedBy = Obj.UpdatedBy;
                    entity.UpdateDate = Obj.UpdateDate;
                  
                    await _dealersRepository.UpdateAsync(entity);
                    await _dealersRepository.SaveChangesAsync();

                    return entity.id.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return "NotFound";
        }

        public async Task<string> DeleteDealers(int id)
        {
            try
            {
                var m = await _dealersRepository.Where(u => u.id == id).FirstOrDefaultAsync();
                if (m != null)
                {
                    await _dealersRepository.DeleteAsync(m);
                    return "Deleted";
                }
                else
                    return "notfound";
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<DealersListDTO> GetAllDealers()
        {
            try
            {
                string procName = SPROC_Names.UspGetAllDealers.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _dealersDTORepository.ExecuteWithJsonResult(procName, "DealersListDTO", ParamsArray);

                return resultData != null ? resultData.ToList() : new List<DealersListDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public List<DealersListDTO> GetDealersByIds(string id)
        {
            try
            {
                string procName = SPROC_Names.UspGetDealersByIds.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@id", Value = id, DbType = System.Data.DbType.String };
                var resultData = _dealersDTORepository.ExecuteWithJsonResult(procName, "DealersListDTO", ParamsArray);

                return resultData != null ? resultData.ToList() : new List<DealersListDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion


        #region Subscription

        public List<SubscriptionDTO> GetAllSubscription()
        {
            try
            {
                string procName = SPROC_Names.UspGetAllSubscription.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _SubscriptionDTORepository.ExecuteWithJsonResult(procName, "SubscriptionDTO", ParamsArray);

                return resultData != null ? resultData.ToList() : new List<SubscriptionDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region AithrPositions

        /// <summary>
        /// Add Groups 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public async Task<string> AddPositions(AithrPositionsDTO obj)
        {
            try
            {
                var _postionModel = await _aithrPositionsRepository.Where(x => x.PositionsName == obj.PositionsName).ToListAsync();
                if (_postionModel.Count > 0)
                {
                    return "AlreadyExist";
                }
                AithrPositions postionObj = MapPostionDTOToModel(obj);
                await _aithrPositionsRepository.InsertAsync(postionObj);
                await _aithrPositionsRepository.SaveChangesAsync();
                return postionObj.id.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Update Positions
        /// </summary>
        /// <param name="Positions"></param>
        /// <returns></returns>
        public async Task<string> UpdatePositions(AithrPositionsDTO model)
        {
            try
            {
                AithrPositions Obj = this.MapPostionDTOToModel(model);
                var entity = await _aithrPositionsRepository.Where(i => i.id == Obj.id).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.PositionsName = Obj.PositionsName;
                    entity.UpdatedBy = Obj.UpdatedBy;
                    entity.UpdateDate = Obj.UpdateDate;

                    await _aithrPositionsRepository.UpdateAsync(entity);
                    await _aithrPositionsRepository.SaveChangesAsync();

                    return entity.id.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return "NotFound";
        }


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeletePositions(int id)
        {
            try
            {
                var m = await _aithrPositionsRepository.Where(u => u.id == id).FirstOrDefaultAsync();
                if (m != null)
                {
                    await _aithrPositionsRepository.DeleteAsync(m);
                    return "Deleted";
                }
                else
                    return "notfound";
            }
            catch (Exception)
            {
                throw;
            }
        }



        /// <summary>
        /// Get ALL Positions
        /// </summary>
        /// <returns></returns>
        public List<AithrPositionsDTO> GetAllPositions()
        {
            try
            {
                string procName = SPROC_Names.UspGetAllPositions.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _aithrPositionsDTORepository.ExecuteWithJsonResult(procName, "AithrPositionsDTO", ParamsArray);

                return resultData != null ? resultData.ToList() : new List<AithrPositionsDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AithrPositionsDTO> GetPositionsByIds(string id)
        {
            try
            {
                string procName = SPROC_Names.UspGetPositionsByIds.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@id", Value = id, DbType = System.Data.DbType.String };
                var resultData = _aithrPositionsDTORepository.ExecuteWithJsonResult(procName, "AithrPositionsDTO", ParamsArray);

                return resultData != null ? resultData.ToList() : new List<AithrPositionsDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #endregion

        #region Private Methods

        private DealersList MapDealersDTOToModel(DealersListDTO obj)
        {
            return new DealersList()
            {
                DealerName = obj.DealerName,
                DealerCity = obj.DealerCity,
                DealerRegion = obj.DealerRegion,
                DealerAddress = obj.DealerAddress,
                PostalCode = obj.PostalCode,
                ActiveUntil = obj.ActiveUntil,
                ActiveStart = obj.ActiveStart,
                DealerPhone = obj.DealerPhone,
                DGroupId = obj.DGroupId,
                GstNumber = obj.GstNumber,
                DealerEmail = obj.DealerEmail,
                IstermsAccepted = obj.IstermsAccepted,
                TermAcceptedDate = obj.TermAcceptedDate,
                Aggrementlink = obj.Aggrementlink,
                SystemIP = obj.SystemIP,
                IsTrial = obj.IsTrial,
                IsActive = obj.IsActive,
                CreatedDate = obj.CreatedDate,
                CreatedBy = obj.CreatedBy

            };
        }

        private AspNetUsers MapUsersDTOToModel(AspNetUsersDTO obj)
        {
            return new AspNetUsers()
            {
                UserName = obj.UserName,
                NormalizedUserName = obj.UserName,
                Email= obj.Email,
                NormalizedEmail = obj.Email,
                PasswordHash= obj.PasswordHash,
                PhoneNumber= obj.PhoneNumber,
                LockoutEnabled= obj.LockoutEnabled,
                AccessFailedCount= obj.AccessFailedCount,
                Userrole=obj.Userrole,
                firstName= obj.firstName,
                lastName = obj.lastName,
                phone = obj.PhoneNumber

            };
        }

        private AspNetRoles MapRoleDTOToModel(AspNetRolesDTO obj)
        {
            return new AspNetRoles()
            {
                Name = obj.Name,
                NormalizedName = obj.Name
            };
        }

        private AithrPositions MapPostionDTOToModel(AithrPositionsDTO obj)
        {
            return new AithrPositions()
            {
                PositionsName = obj.PositionsName,
                CreatedBy = obj.CreatedBy,
                IsActive = obj.IsActive,
                CreatedDate = obj.CreatedDate
            };
        }

        private DGroup MapDTOToModel(DGroupDTO obj)
        {
            return new DGroup()
            {
                id= obj.id,
                GroupName = obj.GroupName,
                Createdby = obj.Createdby,
                IsActive = obj.IsActive,
                CreatedDate = obj.CreatedDate
            };
        }
        #endregion
    }
}
