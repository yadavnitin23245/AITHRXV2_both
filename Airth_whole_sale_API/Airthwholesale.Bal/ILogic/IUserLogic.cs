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
    public interface IUserLogic
    {
        /// <summary>
        /// Get ALL Users
        /// </summary>
        /// <returns></returns>
        List<AppUserDTO> GetAll();

        /// <summary>
        /// Add Users
        /// </summary>
        /// <returns></returns>
        Task<string> AddUser(UserDTO userDTO);

        /// <summary>
        /// Update Users
        /// </summary>
        /// <returns></returns>
        Task<string> UpdateUser(UserDTO obj);

        /// <summary>
        /// Delete Users
        /// </summary>
        /// <returns></returns>
        Task<string> DeleteUser(int id);

        Task<string> SaveBusinessInfo(BusinessInfo obj);

        Task<string> SaveAddress(Address obj);

        List<AppUserDTO> GetUserDetailByIds(string UserName);


       Task<string> Update(AppUserDTO obj);

        //for jwt

        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        //AuthenticateResponse RefreshToken(string token, string ipAddress);
        //void RevokeToken(string token, string ipAddress);
        IEnumerable<User> GetAlldata();
        AppUser GetById(string id);
        Task<string> Delete(string UserName);

    }
}
