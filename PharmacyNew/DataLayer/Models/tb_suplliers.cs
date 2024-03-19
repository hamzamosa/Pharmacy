using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class tb_suplliers
    {
        [Key]
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string Phone { get; set; }
    }
}
