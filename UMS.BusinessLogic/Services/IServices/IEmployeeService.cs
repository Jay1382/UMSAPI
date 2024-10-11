﻿using Project_G2.DomainLayer.Model.RequestModel;
using Project_G2.DomainLayer.Model.ResponseModel;

namespace Project_G2.BuissnessAccessLayer.Services.IServices
{
    public interface IEmployeeService
    {
        Task<ResponseModel> AddEmployee(AddEmployeeRequest addEmployeeRequest);
        Task<ResponseModel> GetEmployees();
        Task<ResponseModel> GetEmployeeById(GetEmployeeByIdRequest getEmployeeByIdRequest);
        Task<ResponseModel> DepartmentCombo();
        Task<ResponseModel> UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest);
        Task<ResponseModel> GetDepartmentById(GetDepartmentByIdRequest getDepartmentById);
        Task<ResponseModel> DeleteEmployee(DeleteEmployeeRequest deleteEmployeeRequest);

    }
}