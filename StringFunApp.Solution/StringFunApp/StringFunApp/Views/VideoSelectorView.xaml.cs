using StringFunApp.ClassLibrary.Models;
using StringFunApp.ClassLibrary.ViewModels;
using Xamarin.Forms;

namespace StringFunApp.Views
{
    public partial class VideoSelectorView : ContentPage
    {
        public VideoSelectorView(Stap stap)
        {
            InitializeComponent();
            BindingContext = new VideoSelectorViewModel(stap, Navigation);
        }
    }
}
