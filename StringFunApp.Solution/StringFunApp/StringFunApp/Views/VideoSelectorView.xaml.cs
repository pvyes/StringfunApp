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
            VideoSelectorViewModel vsvm = new VideoSelectorViewModel(stap, Navigation);
            BindingContext = vsvm;
            /* bind an action to the listview items*/
            VideoListView.ItemTapped += vsvm.ShowVideo;
            VideoListView.SelectedItem = null;
        }
    }
}
