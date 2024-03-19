using PharmacyNew.Events;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNew.Content.ViewModels
{
    public class UpdateCompanyNameViewModel : BindableBase, IDialogAware
    {
        public string Title => "Alert";

        public event Action<IDialogResult> RequestClose;

        private IEventAggregator _eventAggregator;
        public UpdateCompanyNameViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<SendCompanyNameEVENT>().Subscribe(SetCompanyNameValueMethod);
            UodateCompanyCommand = new Prism.Commands.DelegateCommand(UpdateName);
            ClanceCommand = new Prism.Commands.DelegateCommand(CancelDialog);


        }

        private void CancelDialog()
        {
            var result = new DialogResult(ButtonResult.Cancel);
            RequestClose?.Invoke(result);

        }

        private void UpdateName()
        {

            var result = new DialogResult(ButtonResult.OK);
            RequestClose?.Invoke(result);
            _eventAggregator.GetEvent<SendCompanyNameeventFromUpdate>().Publish(companyName);
        }

        private void SetCompanyNameValueMethod(string obj)
        {
            companyName = obj;


        }


        private  string? _companyName;

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
        public Prism.Commands.DelegateCommand UodateCompanyCommand { get; }
        public Prism.Commands.DelegateCommand ClanceCommand { get; }
    }
}
