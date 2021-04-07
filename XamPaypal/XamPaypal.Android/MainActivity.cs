using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using FFImageLoading.Forms.Platform;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using Prism;
using Prism.Ioc;
using System;
using Xamarin.Forms;
namespace XamPaypal.Droid
{
    [Activity(Label = "XamPaypal", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            initFontScale();

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            base.OnCreate(savedInstanceState);


            ///For Performance 
            Forms.SetFlags("FastRenderers_Experimental");

            AndroidEnvironment.UnhandledExceptionRaiser -= StoreLogger;
            AndroidEnvironment.UnhandledExceptionRaiser += StoreLogger;
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
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
            CrossPayPalManager.Init(config, this);
            CachedImageRenderer.InitImageViewHandler();
            LoadApplication(new App(new AndroidInitializer()));
        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
        }
        private void StoreLogger(object sender, RaiseThrowableEventArgs e)
        {
            Console.WriteLine(e.Exception);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        private void initFontScale()
        {
            Configuration configuration = Resources.Configuration;
            configuration.FontScale = (float)1;
            DisplayMetrics metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);
            metrics.ScaledDensity = configuration.FontScale * metrics.Density;
            BaseContext.Resources.UpdateConfiguration(configuration, metrics);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            PayPalManagerImplementation.Manager.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            PayPalManagerImplementation.Manager.Destroy();
        }
    }
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register any platform specific implementations
        }
    }
}