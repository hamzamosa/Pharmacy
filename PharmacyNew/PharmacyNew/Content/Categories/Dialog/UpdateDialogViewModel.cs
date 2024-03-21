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
    public class UpdateDialogViewModel : BindableBase, IDialogAware
    {
        public string Title =>"Alert";

        public event Action<IDialogResult> RequestClose;
        private static IEventAggregator _eventAggregator;
        public UpdateDialogViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            eventAggregator.GetEvent<SendCategoryNameEVENT>().Subscribe(SetCategoryNameValue);
           
            UpdateCategoryCommand = new Prism.Commands.DelegateCommand(UpdateCategoryName);
        }

        private void UpdateCategoryName()
        {
            var result = new DialogResult(ButtonResult.OK);
            RequestClose?.Invoke(result);
            _eventAggregator.GetEvent<SendCategoryNameEVENTFormUpdate>().Publish(CategoryName);
        }

        private void SetCategoryNameValue(string obj)
        {
            CategoryName=obj;
            
        }

        private static string? _CategoryName;

        public string? CategoryName
        {
            get { return _CategoryName; }
            set { SetProperty(ref _CategoryName, value); }
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
        public Prism.Commands.DelegateCommand UpdateCategoryCommand { get; }
    }
}
