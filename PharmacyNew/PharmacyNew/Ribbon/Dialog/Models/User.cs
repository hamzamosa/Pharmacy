using PharmacyNew.Content.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNew.Ribbon.Dialog.Models
{
    public class User
    {
        

        public string Name { get; set; } = null!;

        public string Password { get; set; } = null!;
    }

    public class ApiResponse
    {
        public object Data { get; set; }
        public object Errors { get; set; }
        public int StatusCode { get; set; }
    }
}
