using Application.DTOs;
using Application.DTOs.RequestModel.User;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.Employee.Services
{
    public interface IEmployeeService
    {
        public Task<BaseResponse<User>> GetEmployeeByIdAsync(Guid employeeUserId);
        Task<BaseResponse<bool>> DeleteEmployee(Guid employeeUserId);
        public Task<BaseResponse<User>> UpdateEmployeeAsync(Guid employeeUserId, UpdateEmployeeRequest model);
        public Task<BaseResponse> CreateEmployee(string userToken, CreateEmployee request);
        public Task<BaseResponse<IList<EmployeeDto>>> GetAllEmployeesBySuperAdminAsync(string businessName);
    }
}
