using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Forms.Platforms.Android.Views;
using MvvmCrossFormsTemplate.UI;

namespace MvvmCrossFormsTemplate.Droid
{
    [Activity(
        Label = "MvvmCrossFormsTemplate"
        , MainLauncher = true
        , Icon = "@mipmap/icon"
        , Theme = "@style/Theme.Splash" 
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait,
        LaunchMode = LaunchMode.SingleTask)]
    public class SplashActivity : MvxFormsSplashScreenActivity<Setup, Core.App, AppForms>
    {
        public SplashActivity()
            : base()//Resource.Layout.SplashScreen) 
        {
        }

        protected override Task RunAppStartAsync(Bundle bundle)
        {
            StartActivity(typeof(MainActivity));
            return Task.CompletedTask;
        }
    }
}