using Application.DTOs;
using Application.DTOs.RequestModel;
using Application.DTOs.RequestModel.User;
using Domain.Domain.Modules.Users.Entities;
using Domain.Entities;
using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.Users.IService
{
    public interface IUserService
    {
        public Task<BaseResponse> AddUserAsync(CreateUserAdmin request);
        public Task<SuperAdminUserResponseModel> GetUserAsync(string email);
        //public Task<List<SuperAdminUserResponseModel>> GetUsersAsync();
        //public Task<IList<UsersInRoleResponseModel>> GetUsersByRoleAsync(string roleName);
        public Task<TokenResponseData> CreateToken(TokenRequestModel model);
        public Task<VerifyUserResponse> VerifyUser(string token);
        public Task<BaseResponse> VerifyToken(string token);
        public Task <BaseResponse> VerifyPinCode(string token, string userPincode);
        public Task<BaseResponse> ChangePincode(ChangePincode request);
        public Task<BaseResponse> ResetPasswordAndPincode(string token, ResetPasswordAndPincodeRequest request);
        public Task<BaseResponse> ForgotPassword(ForgotPasswordRequest request);
        public Task<BaseResponse> PasswordReset(string token, PasswordResetRequestModel request);
        public Task<BaseResponse> AddKycAsync(KYCRequestModel request);
        public Task<BaseResponse> KycExistsAsync(Guid superAdminId);
        public Task<BaseResponse> GetUserById(Guid userId);
        Task<BaseResponse> DeleteUserAsync(Guid userId);
    }
}
