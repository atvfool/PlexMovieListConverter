using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace PlexXMLConverter
{
    public class XMLConverter
    {
        public MediaContainer MediaContainers;
        public char chrDelimiter = ',';

        public XMLConverter() { }
        public XMLConverter(string xmlString)
        {
            Convert(xmlString);
        }

        /// <summary>
        /// Deserializes an xml string to MediaContainerClass
        /// </summary>
        /// <param name="xmlString"></param>
        public void Convert(string xmlString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(MediaContainer));

            MediaContainers = new MediaContainer();

            using(TextReader tr = new StringReader(xmlString))
            {
                XmlReader reader = XmlReader.Create(tr);

                MediaContainers = (MediaContainer)xmlSerializer.Deserialize(reader);
            }
        }

        #region csv/delimited text stuff
        public string GetCSV()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetDelimitedHeadersFromObject(MediaContainers.videos.First()));

            foreach (Video video in MediaContainers.videos)
            {
                sb.AppendLine(GetDelimitedStringFromObject(video));
            }

            return sb.ToString();
        }

        private string GetDelimitedHeadersFromObject(object obj)
        {
            string result = string.Empty;

            foreach(var prop in obj.GetType().GetProperties())
            {
                result += prop.Name + chrDelimiter;
            }

            return result;
        }

        private string GetDelimitedStringFromObject(object obj)
        {
            string result = string.Empty;

            foreach (var prop in obj.GetType().GetProperties())
            {
                result += (prop.GetValue(obj, null) ?? string.Empty).ToString() + chrDelimiter;
            }

            return result;
        }
        #endregion

        #region json stuff

        public string GetJson()
        {
            string result = JsonConvert.SerializeObject(MediaContainers.videos);

            return result;
        }

        #endregion
    }

    #region XML Classes
    [Serializable]
    [XmlRoot(ElementName = "MediaContainer")]
    public class MediaContainer
    {
        [XmlAttribute]
        public int size { get; set; }
        [XmlAttribute]
        public string title1 { get; set; }
        [XmlAttribute]
        public string title2 { get; set; }
		[XmlAttribute]
        public string allowSync { get; set; }
		[XmlAttribute]
        public string art { get; set; }
		[XmlAttribute]
        public string identifier { get; set; }
		[XmlAttribute]
        public string librarySectionID { get; set; }
		[XmlAttribute]
        public string librarySectionTitle { get; set; }
		[XmlAttribute]
        public string librarySectionUUID { get; set; }
		[XmlAttribute]
        public string mediaTagPrefix { get; set; }
		[XmlAttribute]
        public string mediaTagVersion { get; set; }
		[XmlAttribute]
        public string thumb { get; set; }
		[XmlAttribute]
        public string viewGroup { get; set; }
		[XmlAttribute]
        public string viewMode { get; set; }

        [XmlElement(ElementName = "Video")]
        public List<Video> videos { get; set; }
    }

    [Serializable]
    public class Video
    {
        [XmlAttribute]
        public string title { get; set; }
		[XmlAttribute]
        public string studio { get; set; }
		[XmlAttribute]
        public string type { get; set; }
		[XmlAttribute]
        public string contentRating { get; set; }
		[XmlAttribute]
        public string summary { get; set; }
		[XmlAttribute]
        public string audienceRating { get; set; }
		[XmlAttribute]
        public int viewCount { get; set; }
		[XmlAttribute]
        public int lastViewedAt { get; set; }
		[XmlAttribute]
        public string year { get; set; }
		[XmlAttribute]
        public string tagline { get; set; }
		[XmlAttribute]
        public string thumb { get; set; }
		[XmlAttribute]
        public string art { get; set; }
		[XmlAttribute]
        public int duration { get; set; }
		[XmlAttribute]
        public string originallyAvailableAt { get; set; }
		[XmlAttribute]
        public string addedAt { get; set; }
		[XmlAttribute]
        public string updatedAt { get; set; }
		[XmlAttribute]
        public string audienceRatingImage { get; set; }
		[XmlAttribute]
        public string chapterSource { get; set; }
		[XmlAttribute]
        public string hasPremiumPrimaryExtra { get; set; }
		[XmlAttribute]
        public string ratingImage { get; set; }
		[XmlAttribute]
        public string ratingKey { get; set; }
		[XmlAttribute]
        public string key { get; set; }
		[XmlAttribute]
        public string guid { get; set; }
        [XmlElement(ElementName = "Media")]
        public List<Media> media { get; set; }
        [XmlElement(ElementName = "Genre")]
        public List<Generic> genres { get; set; }
        [XmlElement(ElementName = "Director")]
        public List<Generic> directors{get;set;}
        [XmlElement(ElementName = "Writer")]
        public List<Generic> writers { get; set; }
        [XmlElement(ElementName = "Country")]
        public List<Generic> countries { get; set; }
        [XmlElement(ElementName = "Collection")]
        public List<Generic> collections { get; set; }
        [XmlElement(ElementName = "Role")]
        public List<Generic> roles { get; set; }
    }

    [Serializable]
    public class Media
    {
		[XmlAttribute]
        public int id { get; set; }
		[XmlAttribute]
        public int duration { get; set; }
		[XmlAttribute]
        public int bitrate { get; set; }
		[XmlAttribute]
        public int width { get; set; }
		[XmlAttribute]
        public int height { get; set; }
		[XmlAttribute]
        public string aspectRation { get; set; }
		[XmlAttribute]
        public int audioChannels { get; set; }
		[XmlAttribute]
        public string audioCodec { get; set; }
		[XmlAttribute]
        public string videoCodec { get; set; }
		[XmlAttribute]
        public string videoResolution { get; set; }
		[XmlAttribute]
        public string container { get; set; }
		[XmlAttribute]
        public string videoFrameRate { get; set; }
		[XmlAttribute]
        public string optimizedForStreaming { get; set; }
		[XmlAttribute]
        public string audioProfile { get; set; }
		[XmlAttribute]
        public string has64bitOffsets { get; set; }
		[XmlAttribute]
        public string videoProfile { get; set; }
        public List<Part> parts { get; set; }
    }

    [Serializable]
    public class Part
    {
		[XmlAttribute]
        public int id { get; set; }
		[XmlAttribute]
        public string key { get; set; }
		[XmlAttribute]
        public int duration { get; set; }
		[XmlAttribute]
        public string file { get; set; }
		[XmlAttribute]
        public int size { get; set; }
		[XmlAttribute]
        public string audioProfile { get; set; }
		[XmlAttribute]
        public string container { get; set; }
		[XmlAttribute]
        public string has64bitOffsets { get; set; }
		[XmlAttribute]
        public string optimizedForStreaming { get; set; }
		[XmlAttribute]
        public string videoProfile { get; set; }
    }

    [Serializable]
    public class Generic
    {
        [XmlAttribute]
        public string tag { get; set; }
    }
    #endregion
}
