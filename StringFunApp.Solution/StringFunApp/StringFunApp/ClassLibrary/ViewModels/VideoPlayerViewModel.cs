using StringFunApp.ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace StringFunApp.ClassLibrary.ViewModels
{
    public class VideoPlayerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<VideoInfo> videos;
        public ObservableCollection<VideoInfo> Videos
        {
            get { return videos; }
            set { videos = value; RaisePropertyChanged(nameof(Videos)); }
        }

        public VideoPlayerViewModel(Stap stap)
        {
            Videos = stap.VideoLijst;
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
