using System;
using System.Collections.Generic;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCrossFormsTemplate.UI.ViewModels;
using Xamarin.Forms;

namespace MvvmCrossFormsTemplate.UI.Views
{
    [MvxContentPagePresentation(NoHistory = true)]
    public partial class LoginPage : BaseContentPage<LoginViewModel>
    {
        public LoginPage()
        {
            InitializeComponent();
        }
    }
}
