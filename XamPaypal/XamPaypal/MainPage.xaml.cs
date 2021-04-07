using PayPal.Forms;
using PayPal.Forms.Abstractions;
using System;
using System.Diagnostics;

namespace XamPaypal
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        async void PayWithPaypal(System.Object sender, System.EventArgs e)
        {
            //Single Item
            var result = await CrossPayPalManager.Current.Buy(new PayPalItem("Test Product", new Decimal(43.10), "USD"), new Decimal(0));
            if (result.Status == PayPalStatus.Cancelled)
            {
                Debug.WriteLine("Cancelled");
            }
            else if (result.Status == PayPalStatus.Error)
            {
                Debug.WriteLine(result.ErrorMessage);
            }
            else if (result.Status == PayPalStatus.Successful)
            {
                Debug.WriteLine(result.ServerResponse.Response.Id);
            }
        }
    }
}