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
        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            VioolKnopKleur = "Default";
            AltvioolKnopKleur = "Default";
            CelloKnopKleur = "Default";
        }

        #region properties
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation navigation;

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

        private string vioolknopkleur;
        public string VioolKnopKleur
        {
            get { return vioolknopkleur; }
            set { vioolknopkleur = value; RaisePropertyChanged(nameof(VioolKnopKleur)); }
        }

        private string altvioolknopkleur;
        public string AltvioolKnopKleur
        {
            get { return altvioolknopkleur; }
            set { altvioolknopkleur = value; RaisePropertyChanged(nameof(AltvioolKnopKleur)); }
        }

        private string celloknopkleur;
        public string CelloKnopKleur
        {
            get { return celloknopkleur; }
            set { celloknopkleur = value; RaisePropertyChanged(nameof(CelloKnopKleur)); }
        }
        #endregion

        public ICommand KiesInstrument => new Command<string>(
            (string instrument) => { TypeInstrument = instrument; SelectedInstrument(TypeInstrument); Enabled = true; }
            );

        public ICommand KiesBoek => new Command<string>(
            (string num) => { int nummer = Convert.ToInt32(num); BoekNummer = nummer; navigation.PushAsync(new StapView(BoekNummer, TypeInstrument)); }
            );

        public void SelectedInstrument(string type)
        {
            switch (type)
            {
                case "Viool":
                    VioolKnopKleur = "Yellow";
                    AltvioolKnopKleur = "Default";
                    CelloKnopKleur = "Default";
                        break;

                case "Altviool":
                    VioolKnopKleur = "Default";
                    AltvioolKnopKleur = "Yellow";
                    CelloKnopKleur = "Default";
                    break;

                case "Cello":
                    VioolKnopKleur = "Default";
                    AltvioolKnopKleur = "Default";
                    CelloKnopKleur = "Yellow";
                    break;

                default:
                    break;
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
