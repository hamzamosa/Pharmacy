using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class TbStore
    {
        [Key]
        public int Id { get; set; }

        public string? CompanyName { get; set; }
        public string? MedicanName { get; set; }
        public string? Barcode { get; set; }
        public string? CategoryName { get; set; }
        public string? SupplierName { get; set; }
        public decimal? salsePrice { get; set; }
        public decimal? purchesePrice { get; set; }
        public int? amount { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? StoreName { get; set; }

    }
}
