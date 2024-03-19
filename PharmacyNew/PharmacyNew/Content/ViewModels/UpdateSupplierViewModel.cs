using PharmacyNew.Events;
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
    public class UpdateSupplierViewModel : BindableBase, IDialogAware
    {
        public string Title => "alert";

        public event Action<IDialogResult> RequestClose;
        private IEventAggregator _eventAggregator;
        public UpdateSupplierViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<SendDataOfSuppliersToUpdateDialog>().Subscribe(SetValues);
            UpdateSupplierInfoCommand = new Prism.Commands.DelegateCommand(UpdateSupplierInfo);
            ClanceCommand = new Prism.Commands.DelegateCommand(CloseDialog);

        }

        private void CloseDialog()
        {
            var result = new DialogResult(ButtonResult.Cancel);
            RequestClose?.Invoke(result);
        }

        private void UpdateSupplierInfo()
        {
            var result = new DialogResult(ButtonResult.OK);
            RequestClose?.Invoke(result);
            _eventAggregator.GetEvent<SendDataOfSuppliersFromUpdateDialog>().Publish((supplierName,supplierPhone));
        }

        private void SetValues((string supplierName, string phone) tuple)
        {
            supplierName = tuple.supplierName;
           supplierPhone= tuple.phone;  
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

        private string? _supplierName;

        public string? supplierName
        {
            get { return _supplierName; }
            set { SetProperty(ref _supplierName, value); }
        }

        private string? _supplierPhone;

        public string? supplierPhone
        {
            get { return _supplierPhone; }
            set { SetProperty(ref _supplierPhone, value); }
        }
        public Prism.Commands.DelegateCommand UpdateSupplierInfoCommand { get; }
        public Prism.Commands.DelegateCommand ClanceCommand { get; }
    }
}
