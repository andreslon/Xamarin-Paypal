using Foundation;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using Prism;
using Prism.Ioc;
using System;
using UIKit;

namespace XamPaypal.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.za
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.Forms.FormsMaterial.Init();
            var config = new PayPalConfiguration(PayPalEnvironment.NoNetwork, "XamPaypalID")
            {
                AcceptCreditCards = true,
                MerchantName = "XamPaypal",
                MerchantPrivacyPolicyUri = "https://www.XamPaypal.com/privacy",
                MerchantUserAgreementUri = "https://www.XamPaypal.com/legal",
                ShippingAddressOption = ShippingAddressOption.Both,
                Language = "es",
                PhoneCountryCode = "52",
            };
            CrossPayPalManager.Init(config);

            LoadApplication(new App(new iOSInitializer()));
            return base.FinishedLaunching(app, options);
        }
    }
    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}