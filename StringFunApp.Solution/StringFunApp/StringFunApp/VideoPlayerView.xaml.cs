using FormsVideoLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StringFunApp
{
    public partial class VideoPlayerView : ContentPage
    {
        public VideoPlayerView()
        {
            InitializeComponent();
        }

        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            var videoLijst = new List<VideoInfo>();

            videoLijst.Add(new VideoInfo {  });

            FasesEnStappen.ItemsSource = videoLijst;
        }
    }
}
