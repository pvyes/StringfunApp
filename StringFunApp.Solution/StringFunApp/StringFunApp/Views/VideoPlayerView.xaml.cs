using StringFunApp.ClassLibrary.Models;
using StringFunApp.ClassLibrary.ViewModels;
using System;
using Xamarin.Forms;

namespace StringFunApp.Views
{
    public partial class VideoPlayerView : ContentPage
    {
        public VideoPlayerView(VideoInfo videoinfo)
        {
            InitializeComponent();
            VideoPlayerViewModel vpvm = new VideoPlayerViewModel(videoinfo, Navigation);
            videoPlayer.Source = vpvm.VideoUri;
        }
    }
}
