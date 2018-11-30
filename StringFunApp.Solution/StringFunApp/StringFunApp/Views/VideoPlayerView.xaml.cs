using FormsVideoLibrary;
using StringFunApp.ClassLibrary;
using StringFunApp.ClassLibrary.Models;
using StringFunApp.ClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StringFunApp.Views
{
    public partial class VideoPlayerView : ContentPage
    {
        public VideoPlayerView(Stap stap)
        {
            InitializeComponent();
            BindingContext = new VideoPlayerViewModel(stap);
        }
    }
}
