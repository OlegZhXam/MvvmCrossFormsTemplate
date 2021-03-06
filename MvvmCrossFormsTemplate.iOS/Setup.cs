using MvvmCross;
using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.IoC;
using MvvmCrossFormsTemplate.UI;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using Serilog;

namespace MvvmCrossFormsTemplate.iOS
{
    public class Setup : MvxFormsIosSetup<Core.App, AppForms>
    {
        protected override ILoggerFactory CreateLogFactory()
        {
            // serilog configuration
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                // add more sinks here
                .WriteTo.NSLog()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }

        protected override ILoggerProvider CreateLogProvider()
        {
            return new SerilogLoggerProvider();
        }

        protected override void InitializeFirstChance(IMvxIoCProvider iocProvider)
        {
            base.InitializeFirstChance(iocProvider);
            //Initialize and Register Platform-Specific services here
        }

        protected override void InitializeLastChance(IMvxIoCProvider iocProvider)
        {
            base.InitializeLastChance(iocProvider);
            //Mvx.IoCProvider.LazyConstructAndRegisterSingleton<INativeBackPressHandler, NativeBackPressHandler>();
        }
    }
}
