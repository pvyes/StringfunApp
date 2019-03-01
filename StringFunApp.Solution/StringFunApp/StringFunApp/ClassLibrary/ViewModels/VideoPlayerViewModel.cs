using FormsVideoLibrary;
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

        private VideoSource videoUri;
        public VideoSource VideoUri
        {
            get { return videoUri; }
            set { videoUri = value; RaisePropertyChanged(nameof(VideoUri)); }
        }

        /*
private bool visible;
public bool Visible
{
get { return visible; }
set { visible = value; RaisePropertyChanged(nameof(Visible)); }
}

*/
        public VideoPlayerViewModel(VideoInfo videoInfo, INavigation navigation)
        {
            this.videoUri = videoInfo.VideoSource;
            this.navigation = navigation;           
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
