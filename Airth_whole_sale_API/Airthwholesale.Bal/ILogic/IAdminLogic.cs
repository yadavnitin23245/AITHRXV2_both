using Airthwholesale.Bal.DTO;
using Airthwholesale.Bal.Model;
using Airthwholesale.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airthwholesale.Bal.ILogic
{
    public interface IAdminLogic
    {


        #region Groups
        /// <summary>
        ///Get All Groups
        /// </summary>
        /// <returns></returns>

        List<DGroupDTO> GetAllGroups();

        /// <summary>
        /// Add Groups
        /// </summary>
        /// <returns></returns>
        Task<string> AddGroups(DGroupDTO obj);

        /// <summary>
        /// Update Users
        /// </summary>
        /// <returns></returns>
        Task<string> UpdateGroups(DGroupDTO obj);

        /// <summary>
        /// Delete Users
        /// </summary>
        /// <returns></returns>
        Task<string> DeleteGroups(int id);

        /// <summary>
        ///Get Group by ID
        /// </summary>
        /// <returns></returns>
        List<DGroupDTO> GetGroupByIds(string id);

        #endregion

        #region user Roles

        ///// <summary>
        /////Get All Roles
        ///// </summary>
        ///// <returns></returns>

        //List<AspNetRolesDTO> GetAllRoles();

        ///// <summary>
        ///// Add Roles
        ///// </summary>
        ///// <returns></returns>
        //Task<string> AddRoles(AspNetRolesDTO obj);

        ///// <summary>
        ///// Update Roles
        ///// </summary>
        ///// <returns></returns>
        //Task<string> UpdateRoles(AspNetRolesDTO obj);

        ///// <summary>
        ///// Delete Roles
        ///// </summary>
        ///// <returns></returns>
        //Task<string> DeleteRoles(int id);

        ///// <summary>
        /////Get Roles by ID
        ///// </summary>
        ///// <returns></returns>
        //List<AspNetRolesDTO> GetRolesByIds(string id);
        #endregion

        #region users

        ///// <summary>
        /////Get All users
        ///// </summary>
        ///// <returns></returns>

        //List<AspNetUsersDTO> GetAllUsers();

        ///// <summary>
        ///// Add Users
        ///// </summary>
        ///// <returns></returns>
        //Task<string> AddUsers(AspNetUsersDTO obj);

        ///// <summary>
        ///// Update Users
        ///// </summary>
        ///// <returns></returns>
        //Task<string> UpdateUsers(AspNetUsersDTO obj);
           
        ///// <summary>
        ///// Delete Users
        ///// </summary>
        ///// <returns></returns>
        //Task<string> DeleteUsers(string id);

        ///// <summary>
        /////Get Users by ID
        ///// </summary>
        ///// <returns></returns>
        //List<AspNetUsersDTO> GetUsersByIds(string id);

        #endregion

        #region Dealers
        /// <summary>
        ///Get All Dealers
        /// </summary>
        /// <returns></returns>

        List<DealersListDTO> GetAllDealers();

        /// <summary>
        /// Add Dealers
        /// </summary>
        /// <returns></returns>
        Task<string> AddDealers(DealersListDTO obj);

        /// <summary>
        /// Update Dealers
        /// </summary>
        /// <returns></returns>
        Task<string> UpdateDealers(DealersListDTO obj);

        /// <summary>
        /// Delete Dealers
        /// </summary>
        /// <returns></returns>
        Task<string> DeleteDealers(int id);

        /// <summary>
        ///Get Dealers by ID
        /// </summary>
        /// <returns></returns>
        List<DealersListDTO> GetDealersByIds(string id);

        #endregion

        #region Subscription
        /// <summary>
        ///Get All Subscription
        /// </summary>
        /// <returns></returns>

        List<SubscriptionDTO> GetAllSubscription();

        #endregion

        #region AithrPositions
        /// <summary>
        ///Get All Positions
        /// </summary>
        /// <returns></returns>

        List<AithrPositionsDTO> GetAllPositions();

        /// <summary>
        /// Add Positions
        /// </summary>
        /// <returns></returns>
        Task<string> AddPositions(AithrPositionsDTO obj);

        /// <summary>
        /// Update Users
        /// </summary>
        /// <returns></returns>
        Task<string> UpdatePositions(AithrPositionsDTO obj);

        /// <summary>
        /// Delete Positions
        /// </summary>
        /// <returns></returns>
        Task<string> DeletePositions(int id);

        /// <summary>
        ///Get Positions by ID
        /// </summary>
        /// <returns></returns>
        List<AithrPositionsDTO> GetPositionsByIds(string id);

        #endregion

    }
}
