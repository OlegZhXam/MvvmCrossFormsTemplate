using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MvvmCrossFormsTemplate.UI
{
    public partial class AppForms : Application
    {
        public AppForms()
        {
            InitializeComponent();

            //For versions 1.7 and up
            XF.Material.Forms.Material.Init(this);
            XF.Material.Forms.Material.Use("Material.Configuration");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}