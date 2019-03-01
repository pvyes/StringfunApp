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
            Stringfun = Stringfun.Instance;
            StapKnoppen = buttons;
            BoekNummer = boeknummer;
            BoekNaam = "Boek " + boeknummer;
            TypeInstrument = typeinstrument;
            try
            {
                StappenLijst = Stringfun.GetStappen(BoekNummer);
                InitializeButtons();
            }
            catch (Exception exception)
            {
                MessagingCenter.Subscribe(this, "Retry", (ErrorView sender) => { StappenLijst = Stringfun.GetStappen(BoekNummer); InitializeButtons(); });
                this.navigation.PushModalAsync(new ErrorView(exception));
            }
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

        private bool isloading;
        public bool IsLoading
        {
            get { return isloading; }
            set { isloading = value; RaisePropertyChanged(nameof(IsLoading)); }
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
        public FlexLayout StapKnoppen
        {
            get { return knoppen; }
            set { knoppen = value; RaisePropertyChanged(nameof(StapKnoppen)); }
        }

        private INavigation navigation;

        private Stringfun Stringfun;
        #endregion

        public ICommand ViewStap => new Command<string>(
            (string selectedstap) =>
            {
                IsLoading = true;
                try
                {
                    SelectedStap = selectedstap;
                    ChangeStapButtonStyle(SelectedStap);
                    Stap NieuweStap = Stringfun.getStap(SelectedStap, TypeInstrument);
                    navigation.PushAsync(new VideoSelectorView(NieuweStap));
                }
                catch (Exception e)
                {
                    string message = e.GetType().ToString() + ". " + e.Message;
                    Console.Write("WARNING: {0}", message);
                }
                IsLoading = false;
            }
            );

        public ICommand ViewContact => new Command(
            () => { navigation.PushAsync(new ContactView()); }
            );

        private void InitializeButtons()
        {
            foreach (var stapnr in StappenLijst)
            {
                var stapKnop = new Button { CommandParameter = stapnr, Text = stapnr, WidthRequest = 70, HeightRequest = 60 };
                stapKnop.SetBinding(Button.CommandProperty, new Binding("ViewStap"));
                StapKnoppen.Children.Add(stapKnop);
            }
        }

        private void ChangeStapButtonStyle(string buttontext)
        {
            foreach (Button stapknop in StapKnoppen.Children)
            {
                if (stapknop.Text == buttontext)
                {
                    stapknop.BackgroundColor = MainViewModel.BTN_SELECTED;
                    stapknop.TextColor = MainViewModel.TEXT_SELECTED;
                }
                else
                {
                    stapknop.TextColor = MainViewModel.TEXT_DEFAULT;
                    stapknop.BackgroundColor = MainViewModel.BTN_DEFAULT;
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
