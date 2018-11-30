﻿using FormsVideoLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StringFunApp.ClassLibrary
{
    public class VideoFactory
    {
        static List<VideoInfo> InMemoryVideos = new List<VideoInfo>
        {
            new VideoInfo{UniekeNaam = "Violin1Boek1Stap1Deel1", DisplayName = "Violin Boek 1 Stap 1 Deel 1", VideoSource = VideoSource.FromUri("https://videos.weebly.com/uploads/1/9/4/9/19490871/30_strijkstokhouding_potlood_891.mp4")},

            new VideoInfo{UniekeNaam = "Violin1Boek1Stap1Deel2", DisplayName = "Violin Boek 1 Stap 1 Deel 2", VideoSource = VideoSource.FromUri("https://videos.weebly.com/uploads/1/9/4/9/19490871/33_de_gitaarhouding_430.mp4")},

            new VideoInfo{UniekeNaam = "Violin1Boek1Stap1Deel3", DisplayName = "Violin Boek 1 Stap 1 Deel 3", VideoSource = VideoSource.FromUri("https://videos.weebly.com/uploads/1/9/4/9/19490871/4_losse_schouderoefening_451.mp4")},

            new VideoInfo{UniekeNaam = "Violin1Boek1Stap2Deel1", DisplayName = "Violin Boek 1 Stap 2 Deel 1", VideoSource = VideoSource.FromUri("https://videos.weebly.com/uploads/1/9/4/9/19490871/29_houding_strijkstok_165.mp4")},

            new VideoInfo{UniekeNaam = "Violin1Boek1Stap2Deel2", DisplayName = "Violin Boek 1 Stap 2 Deel 2", VideoSource = VideoSource.FromUri("https://videos.weebly.com/uploads/1/9/4/9/19490871/31_open_en_dicht_potood_340.mp4")},

            new VideoInfo{UniekeNaam = "Violin1Boek1Stap2Deel3", DisplayName = "Violin Boek 1 Stap 2 Deel 3", VideoSource = VideoSource.FromUri("https://videos.weebly.com/uploads/1/9/4/9/19490871/69_rechterhandpizzicato_gitaarhouding__109.mp4")},

            new VideoInfo{UniekeNaam = "Violin1Boek1Stap3Deel1", DisplayName = "Violin Boek 1 Stap 3 Deel 1", VideoSource = VideoSource.FromUri("https://videos.weebly.com/uploads/1/9/4/9/19490871/36_vingers_op_de_duim_987.mp4")},

            new VideoInfo{UniekeNaam = "Violin1Boek1Stap3Deel2", DisplayName = "Violin Boek 1 Stap 3 Deel 2", VideoSource = VideoSource.FromUri("https://videos.weebly.com/uploads/1/9/4/9/19490871/43_de_stappen_viool_op_de_schouder_929.mp4")},

            new VideoInfo{UniekeNaam = "Violin1Boek1Stap3Deel3", DisplayName = "Violin Boek 1 Stap 3 Deel 3", VideoSource = VideoSource.FromUri("https://videos.weebly.com/uploads/1/9/4/9/19490871/28_open_en_dicht_op_de_schouder_178.mp4")},
        };

        public IEnumerable<VideoInfo> GetAll()
        {
            return InMemoryVideos;
        }
    }
}
