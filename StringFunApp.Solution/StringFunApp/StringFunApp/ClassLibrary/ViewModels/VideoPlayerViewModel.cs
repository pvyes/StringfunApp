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
    public class VideoPlayerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation navigation;

        private ObservableCollection<VideoInfo> videos;
        public ObservableCollection<VideoInfo> Videos
        {
            get { return videos; }
            set { videos = value; RaisePropertyChanged(nameof(Videos)); }
        }

        private bool visible;
        public bool Visible
        {
            get { return visible; }
            set { visible = value; RaisePropertyChanged(nameof(Visible)); }
        }


        public VideoPlayerViewModel(Stap stap, INavigation navigation)
        {
            Videos = stap.VideoLijst;
            Visible = false;
            this.navigation = navigation;
        }

        public ICommand ViewContact => new Command(
            () => { navigation.PushAsync(new ContactView()); }
            );

        public ICommand ShowVideoPlayer => new Command(
            () => { Visible = true; }
            );

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
