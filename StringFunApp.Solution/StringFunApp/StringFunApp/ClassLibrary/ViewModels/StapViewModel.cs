using StringFunApp.ClassLibrary.Models;
using StringFunApp.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace StringFunApp.ClassLibrary.ViewModels
{
    public class StapViewModel : INotifyPropertyChanged
    {
        public StapViewModel(INavigation navigation, int boeknummer, string typeinstrument, FlexLayout buttons)
        {
            this.navigation = navigation;
            stringfun = Stringfun.Instance;
            stapknoppen = buttons;
            boek = stringfun.getBookByNumber(boeknummer);
            this.typeinstrument = typeinstrument;
            try
            {
                StappenLijst = stringfun.GetStappen(boeknummer);
                InitializeButtons();
            }
            catch (Exception exception)
            {
                MessagingCenter.Subscribe(this, "Retry", (ErrorView sender) => { StappenLijst = stringfun.GetStappen(boeknummer); InitializeButtons(); });
                this.navigation.PushModalAsync(new ErrorView(exception));
            }
        }

        #region properties
        private Boek boek;
        public Boek Boek
        {
            get { return boek; }
            set { boek = value; RaisePropertyChanged(nameof(Boek)); }
        }

        public string BoekNaam
        {
            get { return boek.Naam; }
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

        private FlexLayout stapknoppen;
        public FlexLayout StapKnoppen
        {
            get { return stapknoppen; }
            set { stapknoppen = value; RaisePropertyChanged(nameof(StapKnoppen)); }
        }

        private INavigation navigation;

        private Stringfun stringfun;
        #endregion

        public ICommand ViewStap => new Command<string>(
            (string selectedstap) =>
            {
                IsLoading = true;
                try
                {
                    SelectedStap = selectedstap;
                    ChangeStapButtonStyle(SelectedStap);
                    Stap NieuweStap = stringfun.getStap(SelectedStap, TypeInstrument, Boek);
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
