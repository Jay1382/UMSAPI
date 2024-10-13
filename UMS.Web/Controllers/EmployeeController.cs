using Microsoft.AspNetCore.Mvc;
using Project_G2.DomainLayer.Model.RequestModel;
using Project_G2.DomainLayer.Model.ResponseModel;
using UMS.Web.Repository.IRepository;

namespace UMS.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetEmployees()
        {
            var result = await _employeeRepository.GetEmployees();
            
            return Json(result);
        }

        public async Task<JsonResult> AddEmployee(AddEmployeeRequest addEmployeeRequest)
        {
            var result = await _employeeRepository.AddEmployee(addEmployeeRequest);
            return Json(result);
        }

        public async Task<JsonResult> GetEmployeeById(GetEmployeeByIdRequest getEmployeeByIdRequest)
        {
            var result = await _employeeRepository.GetEmployeeById(getEmployeeByIdRequest);
            return Json(result);
        }

        public async Task<JsonResult> GetDepartment()
        {
            var result = await _employeeRepository.GetDepartment();
            return Json(result);
        }

        public async Task<JsonResult> UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest)
        {
            var result = await _employeeRepository.UpdateEmployee(updateEmployeeRequest);
            return Json(result);
        }

        public async Task<JsonResult> DeleteEmployee(DeleteEmployeeRequest deleteEmployeeRequest)
        {
            var result = await _employeeRepository.DeleteEmployee(deleteEmployeeRequest);
            return Json(result);
        }
    }
}