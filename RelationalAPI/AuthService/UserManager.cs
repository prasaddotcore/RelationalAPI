using Microsoft.EntityFrameworkCore;
using RelationalAPI.DataService;
using RelationalAPI.DataService.DataModels;
using RelationalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RelationalAPI.AuthService
{
    public interface IUserManager
    {
        //getroles

        //user.create
        //user.edit
        //user.deletebyid
        //user.getyId
        //user.getall

        Task<RoleDTOListModel> GetRoles(int roleId);
        Task<ResponseModel> CrateUser(NewUserModel objInput);
        Task<ResponseModel> UpdateUser(EditUserModel objInput);
        Task<UserDTO> GetUserById(int Id);
        Task<UserDTO> GetUserByEmail(string email);
        Task<ResponseModel> DeleteUserById(int Id);
        Task<UserListModel> GetUsers();
    }
    public class UserManager:IUserManager
    {
        private readonly RDBContext _RDBContext;

        public UserManager(RDBContext rDBContext)
        {
            _RDBContext = rDBContext;
        }

        public async Task<ResponseModel> CrateUser(NewUserModel objInput)
        {
            var newUser = new User {Name=objInput.Name,Email=objInput.Email,Password=objInput.Password,CreatedOn=DateTime.Now };
            _RDBContext.Users.Add(newUser);
            await _RDBContext.SaveChangesAsync();
            return new ResponseModel { status = true };
        }

        public async Task<ResponseModel> DeleteUserById(int Id)
        {
            var obj = await _RDBContext.Users.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (obj != null)
            {
                 _RDBContext.Users.Remove(obj);
                 await _RDBContext.SaveChangesAsync();
                return new ResponseModel {status=true };
            }else
                return new ResponseModel { status = false,message="user not found" };
        }

        public async Task<RoleDTOListModel> GetRoles(int roleId)
        {
            return new RoleDTOListModel {
            roles = await _RDBContext.Roles.Select(x=>new RoleDTO {Id=x.Id,Name=x.Name }).ToListAsync()
            };
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            return await _RDBContext.Users.Where(w => w.Email == email).Include(ur => ur.UserRoles).ThenInclude(r => r.Role).Select(x => new UserDTO { Id = x.Id, Email = x.Email, Name = x.Name, RoleId = x.UserRoles.FirstOrDefault().Role.Id, Role = x.UserRoles.FirstOrDefault().Role.Name }).FirstOrDefaultAsync();

        }

        public async Task<UserDTO> GetUserById(int Id)
        {
            return await _RDBContext.Users.Where(w=>w.Id==Id).Include(ur => ur.UserRoles).ThenInclude(r => r.Role).Select(x => new UserDTO { Id = x.Id, Email = x.Email, Name = x.Name, RoleId = x.UserRoles.FirstOrDefault().Role.Id, Role = x.UserRoles.FirstOrDefault().Role.Name }).FirstOrDefaultAsync();
        }

        public async Task<UserListModel> GetUsers()
        {
            return new UserListModel {users= await _RDBContext.Users.Include(ur=>ur.UserRoles).ThenInclude(r=>r.Role).Select(x=> new UserDTO{Id=x.Id,Email=x.Email,Name=x.Name,RoleId=x.UserRoles.FirstOrDefault().Role.Id, Role = x.UserRoles.FirstOrDefault().Role.Name }).ToListAsync() };
        }

        public async Task<ResponseModel> UpdateUser(EditUserModel objInput)
        {
            var obj = await _RDBContext.Users.Where(x => x.Id == objInput.Id).FirstOrDefaultAsync();
            if (obj != null)
            {
                if (!string.IsNullOrEmpty(objInput.Email))
                    obj.Email = objInput.Email;
                if(!string.IsNullOrEmpty(objInput.Name))
                    obj.Name = objInput.Name;
                if (!string.IsNullOrEmpty(objInput.Password))
                    obj.Password = objInput.Password;
                obj.ModifiedOn = DateTime.Now;

                _RDBContext.Users.Update(obj);
                await _RDBContext.SaveChangesAsync();
                return new ResponseModel { status = true };
            }
            else
                return new ResponseModel { status = false, message = "user not found" };
        }
    }
}
