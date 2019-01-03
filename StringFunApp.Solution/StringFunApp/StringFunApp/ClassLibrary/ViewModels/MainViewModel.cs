using StringFunApp.ClassLibrary.Models;
using StringFunApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StringFunApp.ClassLibrary.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel(INavigation navigation, FlexLayout instrumentKnoppen, FlexLayout boekKnoppen)
        {
            this.navigation = navigation;
            InstrumentKnoppen = instrumentKnoppen;
            BoekKnoppen = boekKnoppen;
            try
            {
                InitializeButtons();
            }
            catch (Exception exception)
            {
                MessagingCenter.Subscribe(this, "Retry", (ErrorView sender) => { InitializeButtons(); });
                this.navigation.PushModalAsync(new ErrorView(exception));
            }
        }

        #region properties
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation navigation;

        private FlexLayout instrumentknoppen;
        public FlexLayout InstrumentKnoppen
        {
            get { return instrumentknoppen; }
            set { instrumentknoppen = value; RaisePropertyChanged(nameof(InstrumentKnoppen)); }
        }

        private FlexLayout boekknoppen;
        public FlexLayout BoekKnoppen
        {
            get { return boekknoppen; }
            set { boekknoppen = value; RaisePropertyChanged(nameof(BoekKnoppen)); }
        }


        private bool enabled;
        public bool Enabled
        {
            get { return enabled; }
            set { enabled = value; RaisePropertyChanged(nameof(Enabled)); }
        }

        private int boeknummer;
        public int BoekNummer
        {
            get { return boeknummer; }
            set { boeknummer = value; RaisePropertyChanged(nameof(BoekNummer)); }
        }

        private string typeinstrument;
        public string TypeInstrument
        {
            get { return typeinstrument; }
            set { typeinstrument = value; RaisePropertyChanged(nameof(TypeInstrument)); }
        }
        #endregion

        public ICommand KiesInstrument => new Command<string>(
            (string instrument) => { TypeInstrument = instrument; SelectedInstrument(TypeInstrument); Enabled = true; }
            );

        public ICommand KiesBoek => new Command<int>(
            (int num) => { BoekNummer = num; navigation.PushAsync(new StapView(BoekNummer, TypeInstrument)); }
            );

        public ICommand ViewContact => new Command(
            () => { navigation.PushAsync(new ContactView()); }
            );

        public void SelectedInstrument(string type)
        {
            foreach (Button instrumentknop in InstrumentKnoppen.Children)
            {
                if (instrumentknop.Text == type)
                {
                    instrumentknop.BackgroundColor = Color.Yellow;
                }
                else
                {
                    instrumentknop.BackgroundColor = Color.Default;
                }
            }
        }

        private void InitializeButtons()
        {
            Stringfun stringfun = Stringfun.Instance;
            var instruments = stringfun.Instruments;
            var boeken = stringfun.Books;
            foreach (var instrument in instruments)
            {
                var instrumentKnop = new Button { HeightRequest = 60, CommandParameter = instrument.Naam, Text = instrument.Naam };
                instrumentKnop.SetBinding(Button.CommandProperty, new Binding("KiesInstrument"));
                InstrumentKnoppen.Children.Add(instrumentKnop);
            }
            foreach (var boek in boeken)
            {
                var boekKnop = new Button { HeightRequest = 60, CommandParameter = boek.Nummer, Text = "Boek " + boek.Nummer };
                boekKnop.SetBinding(Button.CommandProperty, new Binding("KiesBoek"));
                boekKnop.SetBinding(Button.IsEnabledProperty, new Binding("Enabled"));
                BoekKnoppen.Children.Add(boekKnop);
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
