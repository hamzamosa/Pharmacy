using PharmacyNew.Events;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNew.Ribbon.Dialog
{
    public class AddSupllierViewModel : BindableBase, IDialogAware
    {
        public string Title => "Dialog";

        public event Action<IDialogResult> RequestClose;

        private static IEventAggregator _eventAggregator;
        public AddSupllierViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            AddSupllierCommand = new Prism.Commands.DelegateCommand(AddSupllierMethod);

        }

        private void AddSupllierMethod()
        {
            var result = new DialogResult(ButtonResult.OK);
            RequestClose?.Invoke(result);

            _eventAggregator.GetEvent<SendDataOfSuppliers>().Publish((SupplierName, PhoneNumber));

        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
           
        }

        private static string? _SupplierName;

        public string? SupplierName
        {
            get { return _SupplierName; }
            set { SetProperty(ref _SupplierName, value); }
        }


        private static string? _PhoneNumber;

        public string? PhoneNumber
        {
            get { return _PhoneNumber; }
            set { SetProperty(ref _PhoneNumber, value); }
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
          
        }
        public Prism.Commands.DelegateCommand AddSupllierCommand { get; }
    }
}
