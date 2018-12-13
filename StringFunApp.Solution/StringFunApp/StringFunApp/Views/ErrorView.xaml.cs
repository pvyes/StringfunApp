using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StringFunApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ErrorView : ContentPage
	{
		public ErrorView (Exception exception)
		{
			InitializeComponent ();
            Error(exception);
		}

        private void Error(Exception exception)
        {
            if (exception.GetType().Name == "AggregateException")
            {
                ErrorMessage.Text = "Zorg dat u verbonden bent met het internet.";
            }
            else
            {
                ErrorMessage.Text = exception.GetType().Name;
            }
        }

        private void BtnRetry_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new MainView());
            }
            catch (Exception)
            {
            }
        }
    }
}