using FormsVideoLibrary;

namespace StringFunApp
{
    public class VideoInfo
    {
        public string UniekeNaam { get; set; }

        public string DisplayName { set; get; }

        public VideoSource VideoSource { set; get; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
