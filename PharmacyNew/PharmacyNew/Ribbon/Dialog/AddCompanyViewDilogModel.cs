using Newtonsoft.Json;
using PharmacyNew.Events;
using PharmacyNew.Ribbon.Dialog.Models;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNew.Ribbon.Dialog
{
    public class AddCompanyViewDilogModel : BindableBase, IDialogAware
    {
        public string Title => "Add Company";

        public event Action<IDialogResult> RequestClose;

        private IEventAggregator _eventAggregator;
        public AddCompanyViewDilogModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            AddCompanyCommand = new Prism.Commands.DelegateCommand(AddCompanyMethod);
          

        }

        

        private void AddCompanyMethod()
        {
            var result = new DialogResult(ButtonResult.OK);
            RequestClose?.Invoke(result);
            _eventAggregator.GetEvent<SwndNameOfCompanyFromAddDialogEvent>().Publish(companyName);
          
        }

      


   

        private static string? _companyName;

        public string? companyName
        {
            get { return _companyName; }
            set { SetProperty(ref _companyName, value); }
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            
        }
        public Prism.Commands.DelegateCommand AddCompanyCommand { get; }
    }
}
