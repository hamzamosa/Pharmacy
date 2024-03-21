using Apis.Models;
using BL1.Cmpanies;
using BL1.Suppliers;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : Controller
    {

        private readonly Suppliers _suppliers;
        ApiResponse oApiResponse = new ApiResponse();

        public SuppliersController(Suppliers suppliers)
        {


            _suppliers = suppliers;

        }

        [HttpGet]
      
        public ApiResponse GetAllSuppliers()
        {

            try
            {
                oApiResponse.Data = _suppliers.GetAll();
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
        public ApiResponse FindSupplierById(int id)
        {

            try
            {
                oApiResponse.Data = _suppliers.Find(id);
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
        public ApiResponse UpdateSupplier([FromBody] tb_suplliers table)
        {

            if (table is null)
            {

                oApiResponse.StatusCode = 400;
                oApiResponse.Errors = "Invalid Supplier data provided.";
                return oApiResponse;
            }
            else
            {
                try
                {
                    _suppliers.Edit(table);

                    oApiResponse.Data = "done";
                    oApiResponse.Errors = null;
                    oApiResponse.StatusCode = 200;
                    return oApiResponse;
                }
                catch (Exception ex)
                {


                    oApiResponse.Data = null;
                    oApiResponse.Errors = "An error occurred while updating the Supplier.";
                    oApiResponse.StatusCode = 502;

                    return oApiResponse;
                }



            }



        }

        [HttpPost]
        [Route("AddSupplier")]
        public ApiResponse AddSupplier([FromBody] tb_suplliers table)
        {
            if (table is null)
            {

                oApiResponse.StatusCode = 400;
                oApiResponse.Errors = "Invalid Supplier data provided.";
                return oApiResponse;
            }
            else
            {
                try
                {
                    _suppliers.Add(table);


                    oApiResponse.Data = "done";
                    oApiResponse.Errors = null;
                    oApiResponse.StatusCode = 200;
                    return oApiResponse;
                }
                catch (Exception ex)
                {


                    oApiResponse.Data = null;
                    oApiResponse.Errors = "An error occurred while Adding the Supplier.";
                    oApiResponse.StatusCode = 502;

                    return oApiResponse;
                }



            }



        }

        [HttpPost]
        [Route("DeleteSupplier")]
        public ApiResponse DeleteSupplier([FromBody] int id)
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
                    _suppliers.Delete(id);

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
