using PharmacyNew.Content.Models;
using PharmacyNew.Events;
using PharmacyNew.Ribbon.Dialog;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNew.Ribbon.ViewModels
{
    public class RibbonViewModel:BindableBase 
    {
        private readonly IDialogService _dialogService;
        private static IEventAggregator _eventAggregato;
        private static IRegionManager _regionManager;
        public RibbonViewModel(IDialogService dialogService,IEventAggregator eventAggregator,IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _eventAggregato = eventAggregator;
            _dialogService = dialogService;
            OpenLoginScreenCommand = new Prism.Commands.DelegateCommand(ShowLoginScreen);
            AddCompanyCommand = new Prism.Commands.DelegateCommand(OpenAddCompanyDialog);
            OpeneSupplierDialogCommand = new Prism.Commands.DelegateCommand(OpenAddSupplierDialog);
            eventAggregator.GetEvent<TokenEvent>().Subscribe(SerTokenValue);
            eventAggregator.GetEvent<SwndNameOfCompanyFromAddDialogEvent>().Subscribe(AddCompany);
            eventAggregator.GetEvent<MkaeItesmEnable>().Subscribe(SetEnableVlaue);

            NavigateCommand = new Prism.Commands.DelegateCommand<string>(Navaigation);
        }

        private void OpenAddSupplierDialog()
        {
            _dialogService.ShowDialog("AddSupllierView");
        }

        private void Navaigation(string uri)
        {
            _regionManager.RequestNavigate("ContentReigion", uri);
        }

        private void SetEnableVlaue(bool obj)
        {
           isEnable=obj;
        }

        private void AddCompany(string CompanyName)
        {
            

            AddNewCompany(CompanyName,token);
        }

        private static string token;
        private void SerTokenValue(string obj)
        {
            token = obj;
           
        }
        static HttpClient client = new HttpClient();
        public static async Task<Uri> AddNewCompany(string companyName, string token)
        {
            var data = new { comapanyName = companyName }; // Note the parameter name
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:7061/api/Companies/AddCompany", data);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode) 
            {

               

                _eventAggregato.GetEvent<UpdateCoompnaiesList>().Publish(new CompaniesModel { ComapanyName = companyName });


            }
            // Handle success or error scenarios here

            return response.Headers.Location;
        }

        private void OpenAddCompanyDialog()
        {
            

            _dialogService.ShowDialog("AddCompanyViewDilog");

          
        }

        private void ShowLoginScreen()
        {
            _dialogService.ShowDialog("LoginDialogView");
        }

        private  bool? _isEnable=false;

        public bool? isEnable
        {
            get { return _isEnable; }
            set { SetProperty(ref _isEnable, value); }
        }

        public Prism.Commands.DelegateCommand OpenLoginScreenCommand { get; }
        public Prism.Commands.DelegateCommand AddCompanyCommand { get; }
        public Prism.Commands.DelegateCommand OpeneSupplierDialogCommand { get; }
        public Prism.Commands.DelegateCommand<string> NavigateCommand { get; }



    }
}
