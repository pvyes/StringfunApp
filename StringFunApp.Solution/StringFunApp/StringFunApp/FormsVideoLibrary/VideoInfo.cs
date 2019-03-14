using FormsVideoLibrary;

namespace StringFunApp
{
    public class VideoInfo
    {
        public string Title { get; set; }

        public string Id { get; set; }

        public string DisplayName { set; get; }

        public VideoSource VideoSource { set; get; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
