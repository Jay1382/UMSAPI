using Dapper;
using Project_G2.DomainLayer.Model.RequestModel;
using Project_G2.DomainLayer.Model.ResponseModel;
using System.Data;
using System.Reflection.Metadata;
using UMS.Web.DataContext;
using UMS.Web.Repository.IRepository;

namespace UMS.Web.Repository
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

        public async Task<List<GetEmployeeResponse>> GetEmployees()
        {
            using (var connection = _context.CreateConnection())
            {
                return (await connection.QueryAsync<GetEmployeeResponse>("employee_read")).ToList();
            }
        }

        public async Task<GetEmployeeResponse> GetEmployeeById(GetEmployeeByIdRequest getEmployeeByIdRequest)
        {
            using (var connection = _context.CreateConnection())
            {
                GetEmployeeResponse getEmployeeResponse = new GetEmployeeResponse();
                try
                {
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@Id", getEmployeeByIdRequest.Id);
                    getEmployeeResponse = await connection.QueryFirstOrDefaultAsync<GetEmployeeResponse>("employee_read_id", parameter, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {

                }
                return getEmployeeResponse;
            }
        }

        public async Task<List<DepartmentComboRes>> GetDepartment()
        {
            using (var connection = _context.CreateConnection())
            {
                return (await connection.QueryAsync<DepartmentComboRes>("department_combo")).ToList();
            }
        }

        public async Task<int> AddEmployee(AddEmployeeRequest addEmployeeRequest)
        {
            using (var connection = _context.CreateConnection())
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@FirstName", addEmployeeRequest.FirstName);
                parameter.Add("@LastName", addEmployeeRequest.LastName);
                parameter.Add("@DepartmentId", addEmployeeRequest.DepartmentId);
                var result = await connection.QueryAsync<int>("employee_create", parameter, commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }

        public async Task<int> UpdateEmployee(UpdateEmployeeRequest updateEmployeeRequest)
        {
            using (var connection = _context.CreateConnection())
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Id", updateEmployeeRequest.Id);
                parameter.Add("@FirstName", updateEmployeeRequest.FirstName);
                parameter.Add("@LastName", updateEmployeeRequest.LastName);
                parameter.Add("@DepartmentId", updateEmployeeRequest.DepartmentId);
                var result = await connection.QueryAsync<int>("employee_update", parameter, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<int> DeleteEmployee(DeleteEmployeeRequest deleteEmployeeRequest)
        {
            using (var connection = _context.CreateConnection())
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Id", deleteEmployeeRequest.Id);
                var result = await connection.QueryAsync<int>("employee_delete", parameter, commandType: CommandType.StoredProcedure);

                return result.FirstOrDefault();
            }
        }
    }
}
