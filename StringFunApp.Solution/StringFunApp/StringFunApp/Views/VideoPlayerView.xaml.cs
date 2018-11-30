using FormsVideoLibrary;
using StringFunApp.ClassLibrary;
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
        public VideoPlayerView()
        {
            InitializeComponent();
            BindingContext = new VideoPlayerViewModel();
        }

        /*void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            
        }*/
    }
}
