using BL1.Cmpanies;
using PharmacyNew.Content.Models;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PharmacyNew.Events;
using Prism.Events;
using Newtonsoft.Json.Linq;
using BL1.Suppliers;
using Prism.Commands;
using Prism.Services.Dialogs;
using ToastNotifications;
using ToastNotifications.Messages;
using DataLayer.Migrations;

namespace PharmacyNew.Content.Categories.ViewModels
{
    public class CategoryViewModel :BindableBase
    {
        private IDialogService _dialogService;
        static Notifier notifier = Helper.Helper.ShowMessage();
        private static IEventAggregator _eventAggregator;
        public CategoryViewModel(IEventAggregator eventAggregator, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<TokenEvent>().Subscribe(SetTokenValue);
            eventAggregator.GetEvent<SwndNameOfCategoryFromAddDialogEvent>().Subscribe(AddCategoryMethod);
            eventAggregator.GetEvent<SendCategoryNameEVENTFormUpdate>().Subscribe(UpdateNameOfCategory);
            eventAggregator.GetEvent<ConfirmEventFromRemoveCategorie>().Subscribe(RemoveCategory);

            UpdateCategoryCoomand = new Prism.Commands.DelegateCommand<object?>(UpdateCategoryNameMethod);
            RemoveCategoryCommand = new Prism.Commands.DelegateCommand<object?>(RemoveCategoryNameMethod);
        }

        private void RemoveCategory(bool obj)
        {
            RemoveCategory(Item);

            for (int i = Categories.Count - 1; i >= 0; i--)
            {
                var com = Categories[i];
                if (com.Id == Item)
                {
                    Categories.RemoveAt(i);

                    notifier.ShowSuccess("تم الحذف بنجاح");
                    break;
                }
            }
        }

        private int Item;
        private void RemoveCategoryNameMethod(object? obj)
        {
            Item = (int)obj;
            _dialogService.ShowDialog("ConformationDialogView");
        }

        private void UpdateNameOfCategory(string obj)
        {
          

            UpdateCategory1(CategoryId,obj);

            

            CategoryModel oldCompany = Categories.FirstOrDefault(c => c.Id == CategoryId);
            CategoryId = oldCompany.Id;
            CategoryModel newCompany = new CategoryModel();
            int index = Categories.IndexOf(oldCompany);
            Categories.Remove(oldCompany);

            newCompany.CategoryName = obj;
            newCompany.Id = oldCompany.Id;

            Categories.Insert(index, newCompany);

        }

        public static async Task<Uri> UpdateCategory1(int id, string CategoeyName)
        {
            CategoryModel model = new CategoryModel();
            model.Id = id;
            model.CategoryName = CategoeyName;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token2);

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7061/api/Category", model);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {






            }
            // Handle success or error scenarios here

            return response.Headers.Location;
        }

        private static int CategoryId;
        private void UpdateCategoryNameMethod(object? obj)
        {
            CategoryId = (int)obj;
            
            _dialogService.Show("UpdateDialogView");

            foreach (var com in Categories)
            {

                if (com.Id == CategoryId)
                {

                    _eventAggregator.GetEvent<SendCategoryNameEVENT>().Publish(com.CategoryName);

                }
            }
        }

        private void AddCategoryMethod(string obj)
        {
            CategoryModel model = new CategoryModel();
            model.CategoryName = obj;

            var lastCategory = Categories.OrderByDescending(c => c.Id).FirstOrDefault();
            int lastCategoryId = lastCategory.Id;
            model.Id =lastCategoryId+1;

            AddCategory(obj);
           
            Categories.Add(model);

            
          

        }

        public static async Task<Uri> RemoveCategory(int categoryid)
        {

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token2);

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7061/api/Category/DeleteCategory", categoryid);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {






            }
            // Handle success or error scenarios here

            return response.Headers.Location;
        }

        private static string token2;
        private void SetTokenValue(string obj)
        {
            GetAllCategories("https://localhost:7061/api/Category", obj);

            token2 = obj;
        }

        public static ObservableCollection<CategoryModel> Categories { get; } = new();


        static async Task<List<CategoryModel>> GetAllCategories(string path, string authToken)
        {
            Models.ApiResponseCategory myrespons = null;

            using (HttpClient client = new HttpClient())
            {
                // Add the authorization token to the request header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                HttpResponseMessage response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    myrespons = await response.Content.ReadAsAsync<Models.ApiResponseCategory>();

                    foreach (var com in myrespons.Data)
                    {

                        Categories.Add(com);

                    }
                }
            }

            return myrespons?.Data ?? new List<CategoryModel>();

        }

        static HttpClient client = new HttpClient();
        public static async Task<Uri> AddCategory(string CategoryName)
        {
            CategoryModel model = new CategoryModel();
            model.CategoryName = CategoryName;
            
           

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token2);

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7061/api/Category/AddCategory", model);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {

               

            }
            // Handle success or error scenarios here

            return response.Headers.Location;
        }

        public DelegateCommand<object?> UpdateCategoryCoomand { get; private set; }
        public DelegateCommand<object?> RemoveCategoryCommand { get; private set; }
    }
}
