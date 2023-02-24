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
    public class UserLogic : IUserLogic
    {

        #region Private properties
        private AirthwholesaleDbContext _context;

        private IJwtUtils _jwtUtils;
        private readonly AppSettingsDTO _appSettings;
        private readonly IRepository<UserDTO> _userDTORepository;


        private readonly IRepository<AppUserDTO> _appUserDTORepository;
        private readonly IRepository<User> _userRepository;

        private readonly IRepository<AppUser> _appUser;

        private readonly IRepository<Address> _Address;
        private readonly IRepository<BusinessInfo> _BusinessInfo;
        #endregion

        #region CTOR's
        public UserLogic(
           
            IRepository<User> userRepository,
            IRepository<Address> Address,
            IRepository<BusinessInfo> BusinessInfo,
            IRepository<AppUserDTO> appUserDTORepository,
             IJwtUtils jwtUtils,
            IOptions<AppSettingsDTO> appSettings,
              AirthwholesaleDbContext context

            )
        {
     
            _userRepository = userRepository;
            _Address = Address;
            _BusinessInfo = BusinessInfo;
            _appUserDTORepository = appUserDTORepository;
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;
        }
        public UserLogic(
             IRepository<User> userRepository,
            IRepository<Address> Address,
            IRepository<BusinessInfo> BusinessInfo,
            IRepository<AppUserDTO> appUserDTORepository,
             IJwtUtils jwtUtils,
            IOptions<AppSettingsDTO> appSettings,
              AirthwholesaleDbContext context,
           IRepository<UserDTO> userDTORepository,
                
   
             IRepository<AppUser> appUser


           )
        {

            _userRepository = userRepository;
            _Address = Address;
            _BusinessInfo = BusinessInfo;
            _appUserDTORepository = appUserDTORepository;
            _context = context;
            _jwtUtils = jwtUtils;
            _appSettings = appSettings.Value;

            _userDTORepository = userDTORepository;
       
       
             _appUser = appUser;
        }
        #endregion

        #region Interface  Methods



        /// <summary>
        /// Add User 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public async Task<string> AddUser(UserDTO userDTOObj)
        {
            try
            {
                var _userModel = await _userRepository.Where(x => x.Email == userDTOObj.Email).ToListAsync();
                if (_userModel.Count > 0)
                {
                    return "UserAlreadyExist";
                }
                User usersObj = MapDTOToModel(userDTOObj);
                await _userRepository.InsertAsync(usersObj);
                await _userRepository.SaveChangesAsync();
                return usersObj.UserId.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Update User 
        /// </summary>
        /// <param name="User"></param>
        /// <returns></returns>
        public async Task<string> UpdateUser(UserDTO model)
        {
            try
            {
                User Obj = this.MapDTOToModel(model);
                var entity = await _userRepository.Where(i => i.Email == Obj.Email).FirstOrDefaultAsync();
                if (entity != null)
                {
                    entity.DisplayName = Obj.DisplayName;
                    entity.UserName = Obj.UserName;
                    entity.Password = Obj.Password;

                    await _userRepository.UpdateAsync(entity);
                    await _userRepository.SaveChangesAsync();

                    return entity.UserId.ToString();
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
        public async Task<string> Delete(string UserName)
        {
            try
            {
                var m = await _appUser.Where(u => u.Email == UserName).FirstOrDefaultAsync();
                if (m != null)
                {
                    await _appUser.DeleteAsync(m);
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
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> DeleteUser(int id)
        {
            try
            {
                var m = await _userRepository.Where(u => u.UserId == id).FirstOrDefaultAsync();
                if (m != null)
                {
                    await _userRepository.DeleteAsync(m);
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


        public async Task<string> Update(AppUserDTO model)
        {
            try
            {
                AppUser Obj = this.MapDTOToModelAppUser(model);
                var entity =  _appUser.Where(i => i.Email == Obj.Email).FirstOrDefault();
                if (entity != null)
                {
                    entity.firstName = Obj.firstName;
                    entity.lastName = Obj.lastName;
                    entity.Email = Obj.Email;

                    await _appUser.UpdateAsync(entity);
                    await _appUser.SaveChangesAsync();

                    return entity.Email.ToString();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return "NotFound";
        }

        /// <summary>
        /// Get ALL
        /// </summary>
        /// <returns></returns>
        public List<AppUserDTO> GetAll()
        {
            try
            {
                string procName = SPROC_Names.UspGetAllUsers.ToString();
                var ParamsArray = new SqlParameter[2];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                var resultData = _appUserDTORepository.ExecuteWithJsonResult(procName, "UserList", ParamsArray);

                return resultData != null ? resultData.ToList() : new List<AppUserDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<AppUserDTO> GetUserDetailByIds(string UserName)
        {
            try
            {
                string procName = SPROC_Names.UspGetUserDetailById.ToString();
                var ParamsArray = new SqlParameter[3];
                ParamsArray[0] = new SqlParameter() { ParameterName = "@OpParm", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[1] = new SqlParameter() { ParameterName = "@OpCode", Value = "", DbType = System.Data.DbType.String };
                ParamsArray[2] = new SqlParameter() { ParameterName = "@UserName", Value = UserName, DbType = System.Data.DbType.String };
                var resultData = _appUserDTORepository.ExecuteWithJsonResult(procName, "AppUserDTO", ParamsArray);

                return resultData != null ? resultData.ToList() : new List<AppUserDTO>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Private Methods

        private AppUser MapDTOToModelAppUser(AppUserDTO obj)
        {
            return new AppUser()
            {
                firstName = obj.firstName,
                lastName = obj.lastName,
                Email = obj.Email
            };
        }

        private User MapDTOToModel(UserDTO obj)
        {
            return new User()
            {
                DisplayName = obj.DisplayName,
                UserName = obj.UserName,
                Email = obj.Email,
                Password = obj.Password,
                CreatedDate = obj.CreatedDate
            };
        }

        public async Task<string> SaveBusinessInfo(BusinessInfo obj)
        {
            try
            {

                var m = await _BusinessInfo.InsertAsync(obj);
                await _BusinessInfo.SaveChangesAsync();
                return "done";
            }
            catch (Exception ex)
            {
                return "error";

            }
            //  throw new NotImplementedException();
        }

        public async Task<string> SaveAddress(Address obj)
        {
            try
            {

                var m = await _Address.InsertAsync(obj);
                await _Address.SaveChangesAsync();
                return obj.trackingRegisterId.ToString();
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        //JWT METHODS

        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var user = _context.AppUser.SingleOrDefault(x => x.UserName == model.Username);

            // validate
            if (user == null )
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _jwtUtils.GenerateJwtToken(user);

            #region
            //  var refreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            //user.RefreshTokens.Add(refreshToken);
            // remove old refresh tokens from user
            // removeOldRefreshTokens(user);
            // save changes to db
            //_context.Update(user);
            // _context.SaveChanges();
            #endregion

            return new AuthenticateResponse(user, jwtToken);
        }

        #region commented code
        //public AuthenticateResponse RefreshToken(string token, string ipAddress)
        //{
        //    var user = getUserByRefreshToken(token);
        //    var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        //    if (refreshToken.IsRevoked)
        //    {
        //        // revoke all descendant tokens in case this token has been compromised
        //        revokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
        //        _context.Update(user);
        //        _context.SaveChanges();
        //    }

        //    if (!refreshToken.IsActive)
        //        throw new AppException("Invalid token");

        //    // replace old refresh token with a new one (rotate token)
        //    var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
        //    user.RefreshTokens.Add(newRefreshToken);

        //    // remove old refresh tokens from user
        //    removeOldRefreshTokens(user);

        //    // save changes to db
        //    _context.Update(user);
        //    _context.SaveChanges();

        //    // generate new jwt
        //    var jwtToken = _jwtUtils.GenerateJwtToken(user);

        //    return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        //}

        //public void RevokeToken(string token, string ipAddress)
        //{
        //    var user = getUserByRefreshToken(token);
        //    var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

        //    if (!refreshToken.IsActive)
        //        throw new AppException("Invalid token");

        //    // revoke token and save
        //    revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
        //    _context.Update(user);
        //    _context.SaveChanges();
        //}
        #endregion

        public IEnumerable<User> GetAlldata()
        {
            return _context.User;
        }

        public AppUser GetById(string id)
        {
            var user = _context.AppUser.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        #region
        // helper methods
        //private AppUser getUserByRefreshToken(string token)
        //{
        //    var user = _context.AppUser.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

        //    if (user == null)
        //        throw new AppException("Invalid token");

        //    return user;
        //}
        #endregion


        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtUtils.GenerateRefreshToken(ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private void removeOldRefreshTokens(User user)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            user.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    revokeRefreshToken(childToken, ipAddress, reason);
                else
                    revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

      
        #endregion
    }
}
