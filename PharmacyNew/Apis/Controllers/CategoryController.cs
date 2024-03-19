using Apis.Models;
using BL1.Categories;
using BL1.Cmpanies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : Controller
    {
        private readonly Categories _Categories;
        ApiResponse oApiResponse = new ApiResponse();

        public CategoryController(Categories Categories)
        {
            _Categories = Categories;
        }

    }
}
