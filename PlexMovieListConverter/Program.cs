using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PlexMovieListConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MediaContainer));

            MediaContainer container = new MediaContainer();

            FileStream fs = new FileStream("plex movies.xml", FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            container = (MediaContainer)xmlSerializer.Deserialize(reader);
            fs.Close();

            Console.WriteLine(string.Format("Container size:{0}", container.size));
            Console.WriteLine("Writing titles to file");

            using (StreamWriter sw = new StreamWriter("MovieList.txt"))
            {
                foreach(Video video in container.video)
                {
                    sw.WriteLine(video.title);
                }
            }

            Console.WriteLine("Finished");

            Console.ReadLine();
        }
    }

    #region XML Classes
    [Serializable]
    [XmlRoot(ElementName ="MediaContainer")]
    public class MediaContainer
    {
        [XmlAttribute("size")]
        public int size { get; set; }
        [XmlElement(ElementName ="Video")]
        public List<Video> video { get; set; }
    }

    [Serializable]
    public class Video
    {
        [XmlAttribute]
        public string title { get; set; }
    }
    #endregion
}
