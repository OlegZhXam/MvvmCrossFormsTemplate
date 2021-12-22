using Acr.UserDialogs;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MvvmCrossFormsTemplate.UI.ViewModels
{
    public class BaseViewModel : MvxNavigationViewModel
    {
        public BaseViewModel(ILoggerFactory logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs)
            : base(logProvider, navigationService)
        {
            UserDialogs = userDialogs;
        }

        public IUserDialogs UserDialogs { get; }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        protected virtual async Task RemoteActiondWrapper(Func<Task> action)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await action();
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected virtual async Task RemoteActionWrapperParallel(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }
        }
    }

    public class BaseViewModel<TParameter> : MvxNavigationViewModel<TParameter> where TParameter : class
    {
        public BaseViewModel(ILoggerFactory logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs)
            : base(logProvider, navigationService)
        {
            UserDialogs = userDialogs;
        }

        public IUserDialogs UserDialogs { get; }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public override void Prepare(TParameter parameter)
        {

        }

        protected virtual async Task RemoteActiondWrapper(Func<Task> action)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await action();
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected virtual async Task RemoteActionWrapperParallel(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }
        }
    }

    public class BaseViewModel<TParameter, TResult> : MvxNavigationViewModel<TParameter, TResult>
        where TParameter : class where TResult : class
    {
        public BaseViewModel(ILoggerFactory logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs)
            : base(logProvider, navigationService)
        {
            UserDialogs = userDialogs;
        }

        public IUserDialogs UserDialogs { get; }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public override void Prepare(TParameter parameter)
        {

        }
        protected virtual async Task RemoteActiondWrapper(Func<Task> action)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await action();
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected virtual async Task RemoteActionWrapperParallel(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }
        }
    }

    public class BaseViewModelWithResult<TResult> : MvxNavigationViewModelResult<TResult> where TResult : class
    {
        public BaseViewModelWithResult(ILoggerFactory logProvider, IMvxNavigationService navigationService, IUserDialogs userDialogs)
               : base(logProvider, navigationService)
        {
            UserDialogs = userDialogs;
        }

        public IUserDialogs UserDialogs { get; }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        protected virtual async Task RemoteActiondWrapper(Func<Task> action)
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                await action();
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected virtual async Task RemoteActionWrapperParallel(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }
        }
    }
}
