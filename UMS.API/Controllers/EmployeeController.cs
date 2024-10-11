using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_G2.BuissnessAccessLayer.Services.IServices;
using Project_G2.DomainLayer.Model.RequestModel;
using Project_G2.DomainLayer.Model.ResponseModel;

namespace UMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Route("AddEmployee")]
        [HttpPost]
        public async Task<ResponseModel> AddEmployee(AddEmployeeRequest addEmployeeRequest) 
        {
            var response = await _employeeService.AddEmployee(addEmployeeRequest);
            return response;
        }

        [Route("GetEmployees")]
        [HttpGet]
        public async Task<ResponseModel> GetEmployees()
        {
            var response = await _employeeService.GetEmployees();
            return response;
        }

        [Route("GetEmployeeById")]
        [HttpPost]
        public async Task<ResponseModel> GetEmployeeById(GetEmployeeByIdRequest getEmployeeByIdRequest)
        {
            var response = await _employeeService.GetEmployeeById(getEmployeeByIdRequest);
            return response;
        }

        [Route("UpdateEmployee")]
        [HttpPost]
        public async Task<ResponseModel> UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest)
        {
            var response = await _employeeService.UpdateEmployee(updateEmployeeRequest);
            return response;
        }

        [Route("DeleteEmployee")]
        [HttpPost]
        public async Task<ResponseModel> DeleteEmployee(DeleteEmployeeRequest deleteEmployeeRequest)
        {
            var response = await _employeeService.DeleteEmployee(deleteEmployeeRequest);
            return response;
        }

        [Route("GetDepartmentCombo")]
        [HttpGet]
        public async Task<ResponseModel> DepartmentCombo()
        {
            var response = await _employeeService.DepartmentCombo();
            return response;
        }

        [Route("GetDepartmentById")]
        [HttpPost]
        public async Task<ResponseModel> GetDepartmentById(GetDepartmentByIdRequest getDepartmentById)
        {
            var response = await _employeeService.GetDepartmentById(getDepartmentById);
            return response;
        }

    }
}
