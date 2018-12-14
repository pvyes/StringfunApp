using StringFunApp.ClassLibrary.Models;
using StringFunApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace StringFunApp.ClassLibrary.ViewModels
{
    public class StapViewModel : INotifyPropertyChanged
    {
        public StapViewModel(INavigation navigation, int boeknummer, string typeinstrument, FlexLayout buttons)
        {
            this.navigation = navigation;
            Stringfun = new Stringfun();
            Knoppen = buttons;
            BoekNummer = boeknummer;
            BoekNaam = "Boek " + boeknummer;
            TypeInstrument = typeinstrument;
            StappenLijst = Stringfun.GetStappen(BoekNummer);
            InitializeButtons();
        }

        #region properties
        private int boeknummer;
        public int BoekNummer
        {
            get { return boeknummer; }
            set { boeknummer = value; RaisePropertyChanged(nameof(BoekNummer)); }
        }

        private string boeknaam;
        public string BoekNaam
        {
            get { return boeknaam; }
            set { boeknaam = value; RaisePropertyChanged(nameof(BoekNaam)); }
        }


        private string typeinstrument;
        public string TypeInstrument
        {
            get { return typeinstrument; }
            set { typeinstrument = value; RaisePropertyChanged(nameof(TypeInstrument)); }
        }

        private ObservableCollection<string> stappenlijst;
        public ObservableCollection<string> StappenLijst
        {
            get { return stappenlijst; }
            set { stappenlijst = value; RaisePropertyChanged(nameof(StappenLijst)); }
        }

        private string selectedstap;
        public string SelectedStap
        {
            get { return selectedstap; }
            set { selectedstap = value; RaisePropertyChanged(nameof(SelectedStap)); }
        }

        private FlexLayout knoppen;
        public FlexLayout Knoppen
        {
            get { return knoppen; }
            set { knoppen = value; RaisePropertyChanged(nameof(Knoppen)); }
        }

        private INavigation navigation;

        private Stringfun Stringfun;
        #endregion

        public ICommand ViewStap => new Command<string>(
            async (string selectedstap) =>
            {
                try
                {
                    SelectedStap = selectedstap;
                    var NieuweStap = await Stringfun.CreateStap(SelectedStap, TypeInstrument);
                    await navigation.PushAsync(new VideoPlayerView(NieuweStap));
                }
                catch (Exception)
                {
                }
            }
            );

        private void InitializeButtons()
        {
            foreach (var stap in StappenLijst)
            {
                var stapKnop = new Button { CommandParameter = stap, Text = stap, WidthRequest = 70, HeightRequest = 60 };
                stapKnop.SetBinding(Button.CommandProperty, new Binding("ViewStap"));
                Knoppen.Children.Add(stapKnop);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
