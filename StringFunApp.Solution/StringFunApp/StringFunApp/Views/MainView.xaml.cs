using StringFunApp.ClassLibrary.Models;
using StringFunApp.ClassLibrary.ViewModels;
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
	public partial class MainView : ContentPage
	{
		public MainView ()
		{
			InitializeComponent ();
            var knoppenVerzameling = KnoppenVerzameling;
            BindingContext = new MainViewModel(this.Navigation, knoppenVerzameling);
		}
	}
}