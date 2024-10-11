using Project_G2.BuissnessAccessLayer.Services.IServices;
using Project_G2.DataAccessLayer.Repository.IRepository;
using Project_G2.DomainLayer.Model.RequestModel;
using Project_G2.DomainLayer.Model.ResponseModel;

namespace Project_G2.BuissnessAccessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ResponseModel> AddEmployee(AddEmployeeRequest addEmployeeRequest)
        {
            return await _employeeRepository.AddEmployee(addEmployeeRequest);
        }

        public async Task<ResponseModel> GetEmployees()
        {
            return await _employeeRepository.GetEmployees();
        }

        public async Task<ResponseModel> GetEmployeeById(GetEmployeeByIdRequest getEmployeeByIdRequest)
        {
            return await _employeeRepository.GetEmployeeById(getEmployeeByIdRequest);
        }

        public async Task<ResponseModel> DepartmentCombo()
        {
            return await _employeeRepository.DepartmentCombo();
        }

        public async Task<ResponseModel> UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest)
        {
            return await _employeeRepository.UpdateEmployee(updateEmployeeRequest);
        }

        public async Task<ResponseModel> GetDepartmentById(GetDepartmentByIdRequest getDepartmentById)
        {
            return await _employeeRepository.GetDepartmentById(getDepartmentById);
        }

        public async Task<ResponseModel> DeleteEmployee(DeleteEmployeeRequest deleteEmployeeRequest)
        {
            return await _employeeRepository.DeleteEmployee(deleteEmployeeRequest);
        }
    }
}
