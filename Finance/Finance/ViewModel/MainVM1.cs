﻿using Finance.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace Finance.ViewModel
{
    public class MainVM1 : INotifyPropertyChanged
    {
        public Posts Blog
        {
            get;
            set;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainVM1()
        {
            ReadRss();
        }

        private void ReadRss()
        {
            try {
                XmlSerializer serializer = new XmlSerializer(typeof(Posts));
                using (WebClient client = new WebClient())
                {
                    string xml = Encoding.Default.GetString(client.DownloadData("https://www.nasa.gov/rss/dyn/shuttle_station.rss"));
                    using (Stream reader = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                    {
                        Blog = (Posts)serializer.Deserialize(reader);
                    }

                }
            }
            catch(Exception e)
            {

            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
