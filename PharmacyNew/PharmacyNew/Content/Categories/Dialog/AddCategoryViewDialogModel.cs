using PharmacyNew.Events;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyNew.Content.Categories.Dialog
{
    public class AddCategoryViewDialogModel : BindableBase, IDialogAware
    {
        public string Title => "Alert";

        public event Action<IDialogResult> RequestClose;

        private IEventAggregator _eventAggregator;
        public AddCategoryViewDialogModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            //AddCategoryCommand
            AddCategoryCommand = new Prism.Commands.DelegateCommand(AddCaregoryMethod);
        }

        private void AddCaregoryMethod()
        {
            var result = new DialogResult(ButtonResult.OK);
            RequestClose?.Invoke(result);
            _eventAggregator.GetEvent<SwndNameOfCategoryFromAddDialogEvent>().Publish(categoryName);
        }


        private static string? _categoryName;

        public string? categoryName
        {
            get { return _categoryName; }
            set { SetProperty(ref _categoryName, value); }
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
        public Prism.Commands.DelegateCommand AddCategoryCommand { get; }
    }
}
