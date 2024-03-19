using PharmacyNew.Content.Models;
using PharmacyNew.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using Telerik.Windows.Controls;
using ToastNotifications;
using ToastNotifications.Messages;

namespace PharmacyNew.Content.ViewModels
{
    public class CompaniesViewModel:BindableBase
    {

        static HttpClient client = new HttpClient();
        private static IEventAggregator _eventAggregator;
        private IDialogService _dialogService;
        static Notifier notifier = Helper.Helper.ShowMessage();
       
        public CompaniesViewModel(IEventAggregator eventAggregator,IDialogService dialogService)
        {
            _dialogService = dialogService;
            _eventAggregator= eventAggregator;  
            RemoveCompanyCommand = new Prism.Commands.DelegateCommand<object?>(RemoveCompanyMethod);
            UpdateCompanyName = new Prism.Commands.DelegateCommand<object?>(UpdateCompanyNameMethod);
            eventAggregator.GetEvent<TokenEvent>().Subscribe(SerTokenValue);
            eventAggregator.GetEvent<UpdateCoompnaiesList>().Subscribe(UpdateCompanies);
            eventAggregator.GetEvent<ConfirmEvent>().Subscribe(RemoveCompanyFromAll);
            eventAggregator.GetEvent<SendCompanyNameeventFromUpdate>().Subscribe(UpdateNameOfCompany);
    
            // FillAllCompanies();

        }

        private static string companyName1;
        private void UpdateNameOfCompany(string obj)
        {
            companyName1 = obj;

            UpdateCompany(companyId,companyName1);

            CompaniesModel oldCompany = Companies.FirstOrDefault(c => c.Id == companyId);
            CompaniesModel newCompany = new CompaniesModel();
            int index = Companies.IndexOf(oldCompany);
            Companies.Remove(oldCompany);

            newCompany.ComapanyName = companyName1;
            newCompany.Id =oldCompany.Id;

            Companies.Insert(index, newCompany);
          
        }

        private static int companyId;
        private void UpdateCompanyNameMethod(object? obj)
        {
            companyId = (int)obj;

            _dialogService.Show("UpdateCompanyNameView");

            foreach (var com in Companies) 
            {

                if (com.Id == companyId) 
                {
                   
                    _eventAggregator.GetEvent<SendCompanyNameEVENT>().Publish(com.ComapanyName);
                 
                }
            }
                
            
        }


        private void RemoveCompanyFromAll(bool obj)
        {
              RemoveCompany(Item);

            for (int i = Companies.Count - 1; i >= 0; i--)
            {
                var com = Companies[i];
                if (com.Id == Item)
                {
                    Companies.RemoveAt(i);

                    notifier.ShowSuccess("تم الحذف بنجاح");
                    break;
                }
            }

        }

        private int Item;
        private void RemoveCompanyMethod(object? companyId)
        {
            Item =(int)companyId;

            _dialogService.ShowDialog("ConformationDialogTpremveCompanyView");

        

           
        }


        
        public static async Task<Uri> RemoveCompany(int companyId)
        {
            
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token1);

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7061/api/Companies/DeleteCompany", companyId);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {



               


            }
            // Handle success or error scenarios here

            return response.Headers.Location;
        }

        public static async Task<Uri> UpdateCompany(int id,  string CompanYName)
        {
            CompaniesModel model = new CompaniesModel();
            model.Id = id;
            model.ComapanyName = CompanYName;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token1);

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7061/api/Companies", model);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {






            }
            // Handle success or error scenarios here

            return response.Headers.Location;
        }

        private void UpdateCompanies(CompaniesModel obj)
        {
            Companies.Add(obj);
           
        }

        private static  string token1;
        private void SerTokenValue(string obj)
        {
            GetAllCompanies("https://localhost:7061/api/Companies", obj);

            token1 = obj;
        }

        public static ObservableCollection<CompaniesModel> Companies { get; } = new();


    

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


      
        public DelegateCommand<object?> RemoveCompanyCommand { get; private set; }
        public DelegateCommand<object?> UpdateCompanyName { get; private set; }







    }
}
