using System;
using System.Collections.Generic;
using System.Text;

namespace VideoCollection.Model
{
    public class VideoDataTemplete
    {
        public Uri SourcePath { get; set; }
        public string VideoName { get; set; }
        public string Directory { get; set; }
        public string Size { get; set; }
        public DateTime CreationTime { get; set; }
        public List<TagTemplate> Tags { get; set; }
        public string Comment { get; set; }
    }
}
