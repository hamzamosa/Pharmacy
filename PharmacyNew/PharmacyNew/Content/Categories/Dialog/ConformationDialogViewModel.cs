﻿using PharmacyNew.Events;
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
    public class ConformationDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "alert";

        public event Action<IDialogResult> RequestClose;

        private IEventAggregator _eventAggregator;
        public ConformationDialogViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            yesCommand = new Prism.Commands.DelegateCommand(ConfirmRemove);
            CancelCommand = new Prism.Commands.DelegateCommand(CloseDialog);

        }

        private void CloseDialog()
        {
            var result = new DialogResult(ButtonResult.Cancel);
            RequestClose?.Invoke(result);
        }

        private void ConfirmRemove()
        {
            var result = new DialogResult(ButtonResult.OK);
            RequestClose?.Invoke(result);
            _eventAggregator.GetEvent<ConfirmEventFromRemoveCategorie>().Publish(true);
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

        public Prism.Commands.DelegateCommand yesCommand { get; }
        public Prism.Commands.DelegateCommand CancelCommand { get; }
    }
}
