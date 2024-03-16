using Apis.Models;
using BL1.Cmpanies;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CompaniesController : ControllerBase
    {
        private readonly Companies _companies;
        ApiResponse oApiResponse = new ApiResponse();
     
        public CompaniesController(Companies companies)
        {


            _companies = companies;
         

        }

        private void SetTokenValue(string tokenEvent)
        {
            token = tokenEvent;
            ReaciveToken();
        }

        [HttpGet]
        public ApiResponse GetAllCompanies()
        {



            try
            {
                oApiResponse.Data = _companies.GetAll();
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

        private string token;
        private async Task<bool> ReaciveToken() // Change void to Task<bool>
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5067/api/Companies");
                request.Headers.Add("Authorization", token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }


        [HttpGet("{id}")]
        public ApiResponse FindCompanyById(int id)
        {

            try
            {
                oApiResponse.Data = _companies.Find(id);
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
        public ApiResponse UpdateCompany([FromBody] TbCompany table)
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
                    _companies.Edit(table);

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
        [Route("AddCompany")]
        public ApiResponse AddCompany([FromBody] TbCompany table)
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
                    _companies.Add(table);
                  

                    oApiResponse.Data = "done";
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
        [Route("DeleteCompany")]
        public ApiResponse DeleteCompany([FromBody] int id)
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
                    _companies.Delete(id);

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
