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
            InitializeButtons();
            BindingContext = new MainViewModel(this.Navigation);
		}

        public void InitializeButtons()
        {
            Stringfun stringfun = new Stringfun();
            var instruments = stringfun.GetInstruments();
            var boeken = stringfun.GetBooks();
            int column = -1;
            foreach (var instrument in instruments)
            {
                var instrumentKnop = new Button { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.CenterAndExpand, CommandParameter = instrument.Naam, Text = instrument.Naam };
                instrumentKnop.SetBinding(Button.CommandProperty, new Binding("KiesInstrument"));
                KnoppenGrid.Children.Add(instrumentKnop);
            }
        }
	}
}