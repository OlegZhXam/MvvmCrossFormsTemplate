using Acr.UserDialogs;
using Microsoft.Extensions.Logging;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MvvmCrossFormsTemplate.UI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly string _lastEmailKey = "email";

        public LoginViewModel(ILoggerFactory logProvider
            , IMvxNavigationService navigationService
            , IUserDialogs userDialogs)
            : base(logProvider, navigationService, userDialogs)
        {
            _email = Preferences.Get(_lastEmailKey, string.Empty);
        }

        public ICommand TryLoginCommand =>
            new Command(async () => await Task.Run(() => TryAcceptLogin()));

        public IMvxCommand TogglePasswordVisibilityCommand => new MvxCommand(TogglePasswordVisibility);

        public bool FieldsNotEmpty => !string.IsNullOrWhiteSpace(Email) && !string.IsNullOrWhiteSpace(Password);

        public string PasswordImage => HidePassword ? "ic_eye_closed" : "ic_eye";

        public bool CanProceed => !IsBusy && FieldsNotEmpty;

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                if (SetProperty(ref _email, value))
                {
                    RaisePropertyChanged(nameof(CanProceed));
                    Preferences.Set(_lastEmailKey, value);
                }
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                if (SetProperty(ref _password, value))
                {
                    RaisePropertyChanged(nameof(CanProceed));
                }
            }
        }

        private bool _hidePassword = true;
        public bool HidePassword
        {
            get => _hidePassword;
            set
            {
                if (SetProperty(ref _hidePassword, value))
                    RaisePropertyChanged(nameof(PasswordImage));
            }
        }

        public override void Prepare()
        {
            base.Prepare();
            RaisePropertyChanged(nameof(CanProceed));
        }

        public async Task TryAcceptLogin()
        {
            bool showUnauthorizedAlert = false;

            try
            {
                if (!FieldsNotEmpty)
                {
                    UserDialogs.Toast("Please provide Email and Password");
                    return;
                }

                UserDialogs.ShowLoading("Signing in...");

                //var response = await _restClientService.Login(Username, Password).ConfigureAwait(false);

                //if (response == null || !response.IsSuccessStatusCode)
                //{
                //    showUnauthorizedAlert = true;
                //    return;
                //}

                //await NavigationService.Navigate<SelectModeViewModel>().ConfigureAwait(false);

                UserDialogs.Toast("Success");
            }
            catch (Exception ex)
            {
                showUnauthorizedAlert = true;
                Log.LogError(ex.Message, ex);
            }
            finally
            {
                UserDialogs.HideLoading();

                if (showUnauthorizedAlert)
                    await UserDialogs.AlertAsync("Authorization failed. Please check credentials and the connection", "Unauthorized", "Got it").ConfigureAwait(false);
            }
        }

        private void TogglePasswordVisibility()
        {
            HidePassword = !HidePassword;
        }
    }
}
