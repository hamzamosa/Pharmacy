using Apis.Models;
using BL1.Categories;
using BL1.Cmpanies;
using BL1.Suppliers;
using DataLayer.Models;
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

        [HttpGet]
        public ApiResponse GetAllCategories()
        {



            try
            {
                oApiResponse.Data = _Categories.GetAll();
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = 200;
                return oApiResponse;
            }
            catch (Exception ex)
            {
                oApiResponse.Data = null;
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = 502;

                return oApiResponse;
            }



        }

        [HttpGet("{id}")]
        public ApiResponse FindCategoryById(int id)
        {

            try
            {
                oApiResponse.Data = _Categories.Find(id);
                oApiResponse.Errors = null;
                oApiResponse.StatusCode = 200;
                return oApiResponse;
            }
            catch (Exception ex)
            {
                oApiResponse.Data = null;
                oApiResponse.Errors = ex.Message;
                oApiResponse.StatusCode = 502;

                return oApiResponse;
            }




        }

        [HttpPost]
        public ApiResponse UpdateCompany([FromBody] TbCategories table)
        {

            if (table is null)
            {

                oApiResponse.StatusCode = 400;
                oApiResponse.Errors = "Invalid company data provided.";
                return oApiResponse;
            }
            else
            {
                try
                {
                    _Categories.Edit(table);

                    oApiResponse.Data = "done";
                    oApiResponse.Errors = null;
                    oApiResponse.StatusCode = 200;
                    return oApiResponse;
                }
                catch (Exception ex)
                {


                    oApiResponse.Data = null;
                    oApiResponse.Errors = "An error occurred while updating the company.";
                    oApiResponse.StatusCode = 502;

                    return oApiResponse;
                }



            }



        }


        [HttpPost]
        [Route("AddCategory")]
        public ApiResponse AddCategory([FromBody] TbCategories table)
        {
            if (table is null)
            {

                oApiResponse.StatusCode = 400;
                oApiResponse.Errors = "Invalid company data provided.";
                return oApiResponse;
            }
            else
            {
                try
                {
                   int id = _Categories.Add(table);


                    oApiResponse.Data = id; 
                    oApiResponse.Errors = null;
                    oApiResponse.StatusCode = 200;
                    return oApiResponse;
                }
                catch (Exception ex)
                {


                    oApiResponse.Data = null;
                    oApiResponse.Errors = "An error occurred while Adding the company.";
                    oApiResponse.StatusCode = 502;

                    return oApiResponse;
                }



            }



        }

        [HttpPost]
        [Route("DeleteCategory")]
        public ApiResponse DeleteCategory([FromBody] int id)
        {
            if (id == null)
            {

                oApiResponse.StatusCode = 400;
                oApiResponse.Errors = "Invalid company data provided.";
                return oApiResponse;
            }
            else
            {
                try
                {
                    _Categories.Delete(id);

                    oApiResponse.Data = "done";
                    oApiResponse.Errors = null;
                    oApiResponse.StatusCode = 200;
                    return oApiResponse;
                }
                catch (Exception ex)
                {


                    oApiResponse.Data = null;
                    oApiResponse.Errors = "An error occurred while Removing the company.";
                    oApiResponse.StatusCode = 502;

                    return oApiResponse;
                }




            }



        }

    }
}
