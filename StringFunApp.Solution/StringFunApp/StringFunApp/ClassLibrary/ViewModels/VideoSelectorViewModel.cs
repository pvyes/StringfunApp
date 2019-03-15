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
    public class VideoSelectorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private INavigation navigation;
        private const string NO_VIDEOS_AVAILABLE_MESSAGE = "Voor deze stap zijn momenteel geen videos beschikbaar.";

        private ObservableCollection<VideoInfo> videos;
        public ObservableCollection<VideoInfo> Videos
        {
            get { return videos; }
            set { videos = value; RaisePropertyChanged(nameof(Videos)); setVideoDisplayNames(videos); }
        }

        private void setVideoDisplayNames(ObservableCollection<VideoInfo> videos)
        {
            videoDisplayNames = new ObservableCollection<string>();
            foreach (VideoInfo vi in videos)
            {
                videoDisplayNames.Add(vi.DisplayName);
            }
        }

        private ObservableCollection<string> videoDisplayNames;
        public ObservableCollection<string> VideoDisplayNames
        {
            get { return videoDisplayNames; }
            set { videoDisplayNames = value; RaisePropertyChanged(nameof(VideoDisplayNames)); }
        }


        private string noVideosAvailableMessage;
        public string NoVideosAvailableMessage
        {
            get { return noVideosAvailableMessage; }
            set { noVideosAvailableMessage = value; RaisePropertyChanged(nameof(NoVideosAvailableMessage)); }
        }

        private bool listVisibility;
        public bool ListVisibility
        {
            get { return listVisibility; }
            set { listVisibility = value; RaisePropertyChanged(nameof(ListVisibility)); }
        }

        private bool noVideosVisibility;
        public bool NoVideosVisibility
        {
            get { return noVideosVisibility; }
            set { noVideosVisibility = value; RaisePropertyChanged(nameof(NoVideosVisibility)); }
        }
/*
        private VideoInfo selectedVideo;
        public VideoInfo SelectedVideo
        {
            get { return selectedVideo; }
            set { selectedVideo = value; RaisePropertyChanged(nameof(SelectedVideo)); ShowVideo(selectedVideo); }
        }
*/
        public VideoSelectorViewModel(Stap stap, INavigation navigation)
        {
            Videos = stap.VideoLijst;
            noVideosAvailableMessage = NO_VIDEOS_AVAILABLE_MESSAGE;
            this.navigation = navigation;
            
            if (Videos.Count == 0)
            {
                noVideosVisibility = true;
                listVisibility = false;
            } else
            {
                noVideosVisibility = false;
                listVisibility = true;
            }
        }

        public ICommand ViewContact => new Command(
            () => { navigation.PushAsync(new ContactView()); }
            );

/*        public ICommand ShowVideoPlayer => new Command(
            () => { Visible = true; }
            );
*/
        public ICommand ItemClickCommand
        {
            get
            {
                return new Command((item) => {
                    VideoInfo vi = item as VideoInfo;
                    navigation.PushAsync(new VideoPlayerView(vi)); });
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ShowVideo(object sender, ItemTappedEventArgs e)
        {
            VideoInfo vi = getVideoInfoByDisplayName((string)e.Item);
            navigation.PushAsync(new VideoPlayerView(vi));
        }

        private VideoInfo getVideoInfoByDisplayName(string displayName)
        {
            foreach (VideoInfo vi in videos)
            {
                if (displayName == vi.DisplayName)
                {
                    return vi;
                }
            }
            return null;
        }
    }
}
