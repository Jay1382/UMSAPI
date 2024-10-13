using Project_G2.DomainLayer.Model.RequestModel;
using Project_G2.DomainLayer.Model.ResponseModel;

namespace UMS.Web.Repository.IRepository
{
    public interface IEmployeeRepository
    {
        Task<List<GetEmployeeResponse>> GetEmployees();
        Task<GetEmployeeResponse> GetEmployeeById(GetEmployeeByIdRequest getEmployeeByIdRequest);
        Task<int> AddEmployee(AddEmployeeRequest addEmployeeRequest);
        Task<List<DepartmentComboRes>> GetDepartment();
        Task<int> UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest);
        Task<int> DeleteEmployee(DeleteEmployeeRequest deleteEmployeeRequest);
    }
}
