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
using Prism.Commands;
using Prism.Services.Dialogs;
using ToastNotifications;
using ToastNotifications.Messages;

namespace PharmacyNew.Content.ViewModels
{
    public class SupplierViewModel :BindableBase
    {
        static HttpClient client = new HttpClient();
        private IDialogService _dialogService;
        private IEventAggregator _eventAggregator;
        static Notifier notifier = Helper.Helper.ShowMessage();
        public SupplierViewModel(IEventAggregator eventAggregator,IDialogService dialogService)
        {
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            eventAggregator.GetEvent<TokenEvent>().Subscribe(SerTokenValue);
            eventAggregator.GetEvent<SendDataOfSuppliers>().Subscribe(AddSuplierMethod);
            eventAggregator.GetEvent<SendDataOfSuppliersFromUpdateDialog>().Subscribe(UpdateSuppliers);
            eventAggregator.GetEvent<ConfirmEventFromRemoveSupplier>().Subscribe(RemoveSupplierMethod);
            UpdateSupplierInfo = new Prism.Commands.DelegateCommand<object?>(UpdateSupplierMethod);
            RemoveSupplierInfo = new Prism.Commands.DelegateCommand<object?>(RemoveSupplier);
        }

        private void RemoveSupplierMethod(bool obj)
        {
            RemoveSupplierApi(Item);

           

            for (int i = Suppliers.Count - 1; i >= 0; i--)
            {
                var com = Suppliers[i];
                if (com.Id == Item)
                {
                    Suppliers.RemoveAt(i);

                    notifier.ShowSuccess("تم الحذف بنجاح");
                    break;
                }
            }
        }

        private int Item;
        private void RemoveSupplier(object? obj)
        {
            Item = (int)obj;
            _dialogService.Show("ConformationDialogToremveSupplierView");

        }

        private void UpdateSuppliers((string supplierName, string phone) tuple)
        {
            UpdateSupplierApi(tuple.supplierName,tuple.phone);

            SuplierModel oldSupplier = Suppliers.FirstOrDefault(c => c.Id == supplierId);
            SuplierModel newSupplier = new SuplierModel();
            int index = Suppliers.IndexOf(oldSupplier);
            Suppliers.Remove(oldSupplier);

            newSupplier.SupplierName = tuple.supplierName;
            newSupplier.Phone = tuple.phone;
            newSupplier.Id = newSupplier.Id;

            Suppliers.Insert(index,newSupplier);
        }


        public static async Task<Uri> UpdateSupplierApi(string supplierName, string phone)
        {
            SuplierModel model = new SuplierModel();
            model.Id = supplierId;
            model.SupplierName = supplierName;
            model.Phone = phone;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token1);

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7061/api/Suppliers", model);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {






            }
            // Handle success or error scenarios here

            return response.Headers.Location;
        }

        public static async Task<Uri> RemoveSupplierApi(int supplierId)
        {

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token1);

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7061/api/Suppliers/DeleteSupplier", supplierId);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {






            }
            // Handle success or error scenarios here

            return response.Headers.Location;
        }

        private static int supplierId;
        private void UpdateSupplierMethod(object? obj)
        {
            supplierId = (int)obj;

            _dialogService.Show("UpdateSupplierView");

            foreach (var sub in Suppliers)
            {

                if (sub.Id == supplierId)
                {

                    _eventAggregator.GetEvent<SendDataOfSuppliersToUpdateDialog>().Publish((sub.SupplierName,sub.Phone));

                }
            }

        }

        private void AddSuplierMethod((string supplierName, string phone) tuple)
        {
            
            AddSuplier(tuple.supplierName,  tuple.phone);

            var lastCategory = Suppliers.OrderByDescending(c => c.Id).FirstOrDefault();
            int lastCategoryId = lastCategory.Id;
           

            SuplierModel model = new SuplierModel();
            model.SupplierName=tuple.supplierName;
            model.Phone=tuple.phone;
            model.Id = lastCategoryId + 1;

            Suppliers.Add(model);
        }

        private static string token1;
        private void SerTokenValue(string obj)
        {
            GetAllSuppliers("https://localhost:7061/api/Suppliers",obj);
             token1 = obj;
        }

        public static ObservableCollection<SuplierModel> Suppliers { get; } = new();

        static async Task<List<SuplierModel>> GetAllSuppliers(string path, string authToken)
        {
            ApiSupplierResponse myrespons = null;

            using (HttpClient client = new HttpClient())
            {
                // Add the authorization token to the request header
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                HttpResponseMessage response = await client.GetAsync(path);

                if (response.IsSuccessStatusCode)
                {
                    myrespons = await response.Content.ReadAsAsync<ApiSupplierResponse>();

                    foreach (var sub in myrespons.Data)
                    {

                        Suppliers.Add(sub);

                    }
                }
            }

            return myrespons?.Data ?? new List<SuplierModel>();

        }


        public static async Task<Uri> AddSuplier(string supplierName, string Phone)
        {
            SuplierModel model = new SuplierModel();
            model.SupplierName = supplierName;
            model.Phone = Phone;

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token1);

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7061/api/Suppliers/AddSupplier", model);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {






            }
            // Handle success or error scenarios here

            return response.Headers.Location;
        }
        public DelegateCommand<object?> UpdateSupplierInfo { get; private set; }
        public DelegateCommand<object?> RemoveSupplierInfo { get; private set; }
    }




}
