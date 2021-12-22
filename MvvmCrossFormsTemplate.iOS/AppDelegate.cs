using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using MvvmCrossFormsTemplate.Core;
using MvvmCross.Forms.Platforms.Ios.Views;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCrossFormsTemplate.UI;
using FFImageLoading.Forms.Platform;

namespace MvvmCrossFormsTemplate.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxFormsApplicationDelegate<Setup, App, AppForms>
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Plugin.InputKit.Platforms.iOS.Config.Init();
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            Xamarin.Forms.FormsMaterial.Init();
            CachedImageRenderer.Init();
            XF.Material.iOS.Material.Init();

            app.SetStatusBarStyle(UIStatusBarStyle.LightContent, true);

            return base.FinishedLaunching(app, options);
        }
    }
}
