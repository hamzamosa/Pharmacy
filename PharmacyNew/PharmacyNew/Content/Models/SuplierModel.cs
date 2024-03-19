using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNew.Content.Models
{
    public class SuplierModel
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string Phone { get; set; }
    }

    public class ApiSupplierResponse
    {
        public List<SuplierModel> Data { get; set; }
        public object Errors { get; set; }
        public int StatusCode { get; set; }
    }
}
