using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace VideoCollection.Model
{
    class JsonFunctions : IJsonFunctions
    {
        private IListViewIndex listViewIndex = new ListViewIndex();
        public void FillingJsonFile(List<VideoDataTemplete> videoDataTempletesList)
        {
            string jsonDataString = "[";
            for (int i = 0; i < videoDataTempletesList.Count; i++)
            {
                if (i < videoDataTempletesList.Count - 1)
                {
                    jsonDataString += (JsonConvert.SerializeObject(videoDataTempletesList[i]).ToString() + ",");
                }
                else
                {
                    jsonDataString += JsonConvert.SerializeObject(videoDataTempletesList[i]).ToString();
                }
            }
            jsonDataString += "]";
            File.WriteAllText("videos.json", jsonDataString);
        }

        public void ReadFromJsonOrCreateNewJsonFile(ListView listView, List<VideoDataTemplete> videoDataTempletesList)
        {
            var videosList = File.Exists("videos.json");
            if (videosList)
            {
                var jsonData = JsonConvert.DeserializeObject<List<VideoDataTemplete>>(File.ReadAllText("videos.json"));
                if (jsonData != null)
                {
                    for (int i = 0; i < jsonData.Count; i++)
                    {
                        if (jsonData[i].Tags == null)
                        {
                            jsonData[i].Tags = new List<TagTemplate>();

                        }
                        FillingVideoDataTempleteListFromJson(jsonData[i].Directory, jsonData[i].VideoName, jsonData[i].Size, jsonData[i].CreationTime, jsonData[i].Tags, jsonData[i].Comment, videoDataTempletesList);
                    }
                    listViewIndex.FillingListView(listView, videoDataTempletesList);
                }
            }
            else
            {
                string path = System.IO.Path.Combine(Environment.CurrentDirectory, "videos.json");
                File.Create(path);
            }
        }
        public void FillingVideoDataTempleteListFromJson(string directory, string videoName, string size, DateTime creationTime, List<TagTemplate> tagTemplates, string comment, List<VideoDataTemplete> videoDataTempletesList)
        {
            videoDataTempletesList.Add(new VideoDataTemplete()
            {
                Directory = directory,
                VideoName = videoName,
                Size = size,
                Tags = tagTemplates,
                CreationTime = creationTime,
                Comment = comment
            });
        }
    }
}
