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

        private int headerHeight;
        public int HeaderHeight
        {
            get { return headerHeight; }
            set { headerHeight = value; RaisePropertyChanged(nameof(HeaderHeight)); }
        }

        private string typeInstrument;
        public string TypeInstrument
        {
            get { return typeInstrument; }
            set { typeInstrument = value; RaisePropertyChanged(nameof(TypeInstrument)); }
        }

        private string boekNaam;
        public string BoekNaam
        {
            get { return boekNaam; }
            set { boekNaam = value; RaisePropertyChanged(nameof(BoekNaam)); }
        }

        private string stapNummer;
        public string StapNummer
        {
            get { return stapNummer; }
            set { stapNummer = value; RaisePropertyChanged(nameof(StapNummer)); }
        }

        public VideoSelectorViewModel(Stap stap, INavigation navigation)
        {
            Videos = stap.VideoLijst;
            typeInstrument = stap.Instrument.Naam;
            stapNummer = "Stap " + stap.Nummer.ToString();
            boekNaam = stap.Boek.Naam;
            noVideosAvailableMessage = NO_VIDEOS_AVAILABLE_MESSAGE;
            this.navigation = navigation;
            
            if (Videos.Count == 0)
            {
                noVideosVisibility = true;
                listVisibility = false;
                headerHeight = 80;
            } else
            {
                noVideosVisibility = false;
                listVisibility = true;
                headerHeight = 50;
            }
        }

        public ICommand ViewContact => new Command(
            () => { navigation.PushAsync(new ContactView()); }
            );

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
            //VideoInfo vi = getVideoInfoByDisplayName((string)e.Item);
            navigation.PushAsync(new VideoPlayerView((VideoInfo) e.Item));
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
