using Dapper;
using FileManager.DataAccessLayer.DataContext;
using Microsoft.Extensions.Configuration;
using Project_G2.DataAccessLayer.Repository.IRepository;
using Project_G2.DomainLayer.Model.RequestModel;
using Project_G2.DomainLayer.Model.ResponseModel;
using System.Data;

namespace Project_G2.DataAccessLayer.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DapperDBContext _context;
        private readonly IConfiguration _configuration;
        public EmployeeRepository(DapperDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ResponseModel> AddEmployee(AddEmployeeRequest addEmployeeRequest)
        {
            using (var connection = _context.CreateConnection())
            {
                ResponseModel responseModel = new ResponseModel();
                try
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@FirstName", addEmployeeRequest.FirstName);
                    parameter.Add("@LastName", addEmployeeRequest.LastName);
                    parameter.Add("@DepartmentId", addEmployeeRequest.DepartmentId);
                    var result = await connection.QueryAsync<int>("employee_create", parameter, commandType: CommandType.StoredProcedure);
                    if (result.Count() > 0)
                    {
                        responseModel.StatusCode = 200;
                        responseModel.Message = "Employee added successfully!!";
                        responseModel.Data = result;
                    }
                }
                catch (Exception ex)
                {
                    responseModel.StatusCode = 500;
                    responseModel.Message = $"Error occurred during AddEmployee: {ex.Message}";
                }
                return responseModel;
            }
        }

        public async Task<ResponseModel> GetEmployees()
        {
            using (var connection = _context.CreateConnection())
            {
                ResponseModel responseModel = new ResponseModel();
                try
                {
                    var result = await connection.QueryAsync<GetEmployeeResponse>("employee_read", commandType: CommandType.StoredProcedure);
                    if (result.Count() > 0)
                    {
                        responseModel.StatusCode = 200;
                        responseModel.Message = "Get Employee successfully!!";
                        responseModel.Data = result;
                    }
                    else
                    {
                        responseModel.StatusCode = 200;
                        responseModel.Message = "Employees not found!!";
                        responseModel.Data = result;
                    }
                }
                catch (Exception ex)
                {
                    responseModel.StatusCode = 500;
                    responseModel.Message = $"Error occurred during GetEmployees: {ex.Message}";
                }
                return responseModel;
            }
        }

        public async Task<ResponseModel> GetEmployeeById(GetEmployeeByIdRequest getEmployeeByIdRequest)
        {
            using (var connection = _context.CreateConnection())
            {
                ResponseModel responseModel = new ResponseModel();
                try
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@Id", getEmployeeByIdRequest.Id);
                    var result = await connection.QueryAsync<GetEmployeeResponse>("employee_read_id", parameter, commandType: CommandType.StoredProcedure);
                    if (result.Count() > 0)
                    {
                        responseModel.StatusCode = 200;
                        responseModel.Message = "Get Employee successfully!!";
                        responseModel.Data = result;
                    }
                    else
                    {
                        responseModel.StatusCode = 200;
                        responseModel.Message = "Employee not found!!";
                        responseModel.Data = result;
                    }
                }
                catch (Exception ex)
                {
                    responseModel.StatusCode = 500;
                    responseModel.Message = $"Error occurred during GetEmployeeById: {ex.Message}";
                }
                return responseModel;
            }
        }

        public async Task<ResponseModel> DepartmentCombo()
        {
            using (var connection = _context.CreateConnection())
            {
                ResponseModel responseModel = new ResponseModel();
                try
                {
                    var result = await connection.QueryAsync<DepartmentComboRes>("department_combo", commandType: CommandType.StoredProcedure);
                    if (result.Count() > 0)
                    {
                        responseModel.StatusCode = 200;
                        responseModel.Message = "department get successfully!!";
                        responseModel.Data = result;
                    }
                }
                catch (Exception ex)
                {
                    responseModel.StatusCode = 500;
                    responseModel.Message = $"Error occurred during ReadEmployeeById: {ex.Message}";
                }
                return responseModel;
            }
        }

        public async Task<ResponseModel> UpdateEmployee(UpdateEmployeeRequest editEmployeeRequest)
        {
            using (var connection = _context.CreateConnection())
            {
                ResponseModel responseModel = new ResponseModel();
                try
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@Id", editEmployeeRequest.Id);
                    parameter.Add("@FirstName", editEmployeeRequest.FirstName);
                    parameter.Add("@LastName", editEmployeeRequest.LastName);
                    parameter.Add("@DepartmentId", editEmployeeRequest.DepartmentId);
                    var result = await connection.QueryAsync<getDepartmentByIdResponse>("employee_update", parameter, commandType: CommandType.StoredProcedure);
                    if (result.Count() > 0)
                    {
                        responseModel.StatusCode = 200;
                        responseModel.Message = "Employee edited successfully!!";
                        responseModel.Data = result;
                    }
                }
                catch (Exception ex)
                {
                    responseModel.StatusCode = 500;
                    responseModel.Message = $"Error occurred during EditEmployee: {ex.Message}";
                }
                return responseModel;
            }
        }

        public async Task<ResponseModel> GetDepartmentById(GetDepartmentByIdRequest getDepartmentById)
        {
            using (var connection = _context.CreateConnection())
            {
                ResponseModel responseModel = new ResponseModel();
                try
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@Id", getDepartmentById.Id);
                    var result = await connection.QueryAsync<getDepartmentByIdResponse>("department_get_id", parameter, commandType: CommandType.StoredProcedure);
                    if (result.Count() > 0)
                    {
                        responseModel.StatusCode = 200;
                        responseModel.Message = "Employee added successfully!!";
                        responseModel.Data = result;
                    }
                }
                catch (Exception ex)
                {
                    responseModel.StatusCode = 500;
                    responseModel.Message = $"Error occurred during CreateEmployee: {ex.Message}";
                }
                return responseModel;
            }
        }

        public async Task<ResponseModel> DeleteEmployee(DeleteEmployeeRequest deleteEmployeeRequest)
        {
            using (var connection = _context.CreateConnection())
            {
                ResponseModel responseModel = new ResponseModel();
                try
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@Id", deleteEmployeeRequest.Id);
                    var result = await connection.QueryAsync<int>("employee_delete", parameter, commandType: CommandType.StoredProcedure);
                    if (result.Count() > 0)
                    {
                        responseModel.StatusCode = 200;
                        responseModel.Message = "Employee deleted successfully!!";
                        responseModel.Data = result;
                    }
                }
                catch (Exception ex)
                {
                    responseModel.StatusCode = 500;
                    responseModel.Message = $"Error occurred during RemoveEmployee: {ex.Message}";
                }
                return responseModel;
            }
        }
    }
}
