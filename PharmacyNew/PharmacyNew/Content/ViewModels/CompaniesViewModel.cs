using PharmacyNew.Content.Models;
using PharmacyNew.Events;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using Telerik.Windows.Controls;

namespace PharmacyNew.Content.ViewModels
{
    public class CompaniesViewModel
    {

        static HttpClient client = new HttpClient();
        private static IEventAggregator _eventAggregator;
        public CompaniesViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<TokenEvent>().Subscribe(SerTokenValue);
            eventAggregator.GetEvent<UpdateCoompnaiesList>().Subscribe(UpdateCompanies);
            // FillAllCompanies();
         
        }

        private void UpdateCompanies(CompaniesModel obj)
        {
            Companies.Add(obj);
           
        }

        private void SerTokenValue(string obj)
        {
            GetAllCompanies("https://localhost:7061/api/Companies", obj);
        }

        public static ObservableCollection<CompaniesModel> Companies { get; } = new();


        //public async Task<bool> FillAllCompanies()
        //{
        //    string uri = "https://localhost:7061/api/Companies";

        //    // Add your authentication token here
        //    string token = "";

        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //        var resp = await client.GetAsync(uri);

        //        if (resp.IsSuccessStatusCode)
        //        {
        //            var content = await resp.Content.ReadAsStringAsync();
        //        //    var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(content);

        //          //  var companies = JsonConvert.DeserializeObject<List<CompaniesModel>>(apiResponse.Data.ToString());

        //            Companies.Clear();
        //            foreach (var company in companies)
        //            {
        //                Companies.Add(company);
        //            }
        //            return true; // Success



        //        }
        //        else
        //        {
        //            // Handle unsuccessful response
        //            // Log or display error message
        //            return false; // Failure
        //        }
        //    }
        //}

        static async Task<List<CompaniesModel>> GetAllCompanies(string path, string authToken)
        {
            Models.ApiResponse myrespons = null;

            using (HttpClient client = new HttpClient())
            {
                // Add the authorization token to the request header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                HttpResponseMessage response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    myrespons = await response.Content.ReadAsAsync<Models.ApiResponse>();

                   foreach(var com in myrespons.Data) 
                    {
                    
                    Companies.Add(com);
                    
                    }
                }
            }

            return myrespons?.Data ?? new List<CompaniesModel>();

        }

      
     

      





    }
}
