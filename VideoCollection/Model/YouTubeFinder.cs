using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using VideoLibrary;

namespace VideoCollection.Model
{
    class YouTubeFinder
    {
        private string name;
        private Uri videoUri;
        public void GetSource(string link)
        {
            try
            {
                var youtube = YouTube.Default;
                var video = youtube.GetVideo(link);
                name = video.FullName;
                videoUri = new Uri(video.Uri);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }
        public string GetName()
        {
            return name;
        }
        public Uri GetUri()
        {
            return videoUri;
        }
        
    }
}
