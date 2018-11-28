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
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation navigation;

        public MainViewModel(INavigation navigation)
        {
            this.navigation = navigation;
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

        public ICommand KiesViool => new Command(
            () => { TypeInstrument = "Viool"; }
            );

        public ICommand KiesAltviool => new Command(
            () => { TypeInstrument = "Altviool"; }
            );

        public ICommand KiesCello => new Command(
            () => { TypeInstrument = "Cello"; }
            );

        public ICommand KiesBoek1 => new Command(
            () => { BoekNummer = 1; navigation.PushAsync(new StapView(BoekNummer, TypeInstrument)); },
            () =>
            {
                if (TypeInstrument != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            );

        public ICommand KiesBoek2 => new Command(
            () => { BoekNummer = 2; navigation.PushAsync(new StapView(BoekNummer, TypeInstrument)); },
            () =>
            {
                if (TypeInstrument != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            );

        public ICommand KiesBoek3 => new Command(
            () => { BoekNummer = 3; navigation.PushAsync(new StapView(BoekNummer, TypeInstrument)); },
            () =>
            {
                if (TypeInstrument != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            );

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
