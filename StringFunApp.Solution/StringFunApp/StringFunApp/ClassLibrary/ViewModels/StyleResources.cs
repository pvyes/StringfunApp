using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StringFunApp.ClassLibrary.ViewModels
{
    public sealed class StyleResources
    {
        private static ResourceDictionary rd;
        private static readonly Lazy<StyleResources> _instance = new Lazy<StyleResources>(() => new StyleResources());

        private StyleResources()
        {
            rd = new ResourceDictionary();
        }

        public static StyleResources Instance
        {
            get { return _instance.Value; }
        }
    }
}
