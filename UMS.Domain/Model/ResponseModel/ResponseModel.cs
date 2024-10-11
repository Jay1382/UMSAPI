using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_G2.DomainLayer.Model.ResponseModel
{
    public class ResponseModel
    {
        public int? StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
