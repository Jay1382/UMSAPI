using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_G2.DomainLayer.Model.RequestModel
{
    public class UpdateEmployeeRequest
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? DepartmentId { get; set; }
    }
}
