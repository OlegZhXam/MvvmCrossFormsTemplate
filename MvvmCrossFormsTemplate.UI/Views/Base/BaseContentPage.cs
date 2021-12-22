using MvvmCross.Forms.Views;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MvvmCrossFormsTemplate.UI.Views
{
    public class BaseContentPage<TViewModel> : MvxContentPage<TViewModel> where TViewModel : class, IMvxViewModel
    {
        public BaseContentPage()
        {

        }
    }
}