using Apis.Models;
using DryIoc;
using Newtonsoft.Json;
using PharmacyNew.Events;
using PharmacyNew.Ribbon.Dialog.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telerik.Windows.Controls;
using Telerik.Windows.Persistence.Core;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using ApiResponse = PharmacyNew.Ribbon.Dialog.Models.ApiResponse;

namespace PharmacyNew.Ribbon.Dialog
{
    public class LoginDialogViewModel : BindableBase, IDialogAware
    {
        public string Title => "Login";
        private static IEventAggregator _eventAggregator;

        public event Action<IDialogResult> RequestClose;
       static  Notifier notifier = Helper.Helper.ShowMessage();
        public LoginDialogViewModel( IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            LoginCommand = new DelegateCommand<object>(LoginDialogCommandAsync);
            cancelCommand = new Prism.Commands.DelegateCommand(CancelDialog);

        


        }

        private void CancelDialog()
        {
            var result = new DialogResult(ButtonResult.Cancel);
            RequestClose?.Invoke(result);
        }

        private void LoginDialogCommandAsync(object obj)
        {
          

            var result = new DialogResult(ButtonResult.OK);
            RequestClose?.Invoke(result);

            var passWord1 = obj as RadPasswordBox;
            if (passWord1 is not null)
            {
                passWord = passWord1.Password;
            }

            User budy = new User
            {
                Password=passWord,
                Name=userName

            };

            LoginAsync(userName);


            

        }



        static HttpClient client = new HttpClient();


        private static string pass;
        static async Task<Uri> LoginAsync(string userName ) 
        {
            pass = _passWord;
          
            HttpResponseMessage response = await client.PostAsJsonAsync($"https://localhost:7061/api/Users/Login?password={pass}", userName);
            response.EnsureSuccessStatusCode();


            if (response.IsSuccessStatusCode) 
            {
                string token = await response.Content.ReadAsStringAsync();
                var tokenObject = JsonConvert.DeserializeObject<ApiResponse>(token);
                var data = tokenObject.Data;
                _eventAggregator.GetEvent<TokenEvent>().Publish(data.ToString());
                _eventAggregator.GetEvent<MkaeItesmEnable>().Publish(true);
                notifier.ShowSuccess("تم تسجيل الدخول بنجاح");

                return response.Headers.Location;

            }
            else
            {
                return response.Headers.Location;
            }



        }


        private static string? _passWord;

        public  string? passWord
        {
            get { return _passWord; }
            set { SetProperty(ref _passWord, value); }
        }

        private string? _userName;

        public string? userName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
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

        public DelegateCommand<object> LoginCommand { get; set; }
        public Prism.Commands.DelegateCommand cancelCommand { get; }
    }
}
