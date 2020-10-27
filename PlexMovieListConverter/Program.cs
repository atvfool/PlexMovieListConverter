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
            }
            else
            {
                if(args.Length < 2)
                {
                    Console.WriteLine("Incorrect number of arguemnets supplied. Use -h for usage rules.");
                }
                else
                {
                    XMLConverter converter = new XMLConverter();
                    converter.Convert(args[0], args[1]);
                }
                
            }
        }
    }

   
}
