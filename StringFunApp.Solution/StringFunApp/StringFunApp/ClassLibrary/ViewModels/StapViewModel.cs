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
        public StapViewModel(INavigation navigation, int boeknummer, string typeinstrument)
        {
            this.navigation = navigation;
            Stringfun = new Stringfun();
            BoekNummer = boeknummer;
            TypeInstrument = typeinstrument;
            GetStappen(BoekNummer);
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

        private INavigation navigation;

        private Stringfun Stringfun;

        public ObservableCollection<string> GetStappen(int boeknummer)
        {
            StappenLijst = new ObservableCollection<string>();
            switch (boeknummer)
            {
                case 1:
                    for (int i = 1; i < 25; i++)
                    {
                        StappenLijst.Add("Stap " + i.ToString());
                    }
                    break;

                case 2:
                    for (int i = 25; i < 57; i++)
                    {
                        StappenLijst.Add("Stap " + i.ToString());
                    }
                    break;

                case 3:
                    for (int i = 57; i < 79; i++)
                    {
                        StappenLijst.Add("Stap " + i.ToString());
                    }
                    break;
            }
            return StappenLijst;
        }

        public ICommand ViewStap => new Command(
            async () =>
            {
                try
                {
                    var NieuweStap = await Stringfun.CreateStap(SelectedStap, TypeInstrument);
                    await navigation.PushAsync(new VideoPlayerView(NieuweStap));
                }
                catch (Exception)
                {
                }
            }
            );

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
