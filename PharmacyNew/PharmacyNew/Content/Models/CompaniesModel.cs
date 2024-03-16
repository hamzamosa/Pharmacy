using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNew.Content.Models
{
    public class CompaniesModel
    {
        public int Id { get; set; }

        public string? ComapanyName { get; set; }
    }

    public class ApiResponse
    {
        public List<CompaniesModel> Data { get; set; }
        public object Errors { get; set; }
        public int StatusCode { get; set; }
    }
}
