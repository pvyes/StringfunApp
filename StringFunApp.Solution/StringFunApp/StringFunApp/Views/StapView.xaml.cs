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
	public partial class StapView : ContentPage
	{
		public StapView (int boeknummer, string typeinstrument)
		{
			InitializeComponent ();
            BindingContext = new StapViewModel(this.Navigation ,boeknummer, typeinstrument);
		}
	}
}