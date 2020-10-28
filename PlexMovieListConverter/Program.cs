using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using PlexXMLConverter;

namespace PlexMovieListConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Contains("-h"))
            {
                Console.WriteLine("USAGE: PlexMovieListConverter.exe [FileToConvert] [OutputFile]");
                Console.WriteLine("This CLI only outputs the video objects");
            }
            else
            {
                if(args.Length < 2)
                {
                    Console.WriteLine("Incorrect number of arguemnets supplied. Use -h for usage rules.");
                }
                else
                {
                    if(File.Exists(args[0]))
                    {
                        string xmlString = File.ReadAllText(args[0]);
                        XMLConverter converter = new XMLConverter(xmlString);

                        using (StreamWriter sw = new StreamWriter( args[1]))
                        {
                            sw.WriteLine(converter.GetHeadersFromObject(converter.MediaContainers.videos.First()));

                            foreach(Video video in converter.MediaContainers.videos)
                            {
                                sw.WriteLine(converter.GetStringFromObject(video));
                            }
                        }

                        Console.WriteLine("File Created");
                    }
                    else
                    {
                        Console.WriteLine("Input file doesn't exist");
                    }

                    
                }
                
            }
        }
    }

   
}
