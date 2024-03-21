using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNew.Content.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string? CategoryName { get; set; }
    }

    public class ApiResponseCategory
    {
        public List<CategoryModel> Data { get; set; }
        public object Errors { get; set; }
        public int StatusCode { get; set; }
    }
}
