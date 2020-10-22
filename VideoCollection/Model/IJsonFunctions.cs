using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace VideoCollection.Model
{
    interface IJsonFunctions
    {
        void FillingJsonFile(List<VideoDataTemplete> videoDataTempletesList);
        void ReadFromJsonOrCreateNewJsonFile(ListView listView, List<VideoDataTemplete> videoDataTempletesList);
        void FillingVideoDataTempleteListFromJson(string directory, string videoName, string size, DateTime creationTime, List<TagTemplate> tagTemplates, string comment, int pressingFrequency, List<VideoDataTemplete> videoDataTempletesList);
    }
}
