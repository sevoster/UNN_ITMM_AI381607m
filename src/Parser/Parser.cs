﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidSurfaceNameSpace.Primitive;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Windows;
using MidSurfaceNameSpace;

namespace MidSurfaceNameSpace.IO
{
    public interface IParser
    {
        Figure ImportFile(string filePath);
    }

    public class Parser : IParser
    {
        public Figure ImportFile(string filePath)
        {
            Shape2D deserialized = Parse(filePath);
            
            if (deserialized == null)
            {
                return null;
            }
            //Temporary check here
            foreach (var contour in deserialized.Contour)
            {
                if (contour.JointsOfSegments.Count() != contour.Segments.Count())
                {
                    return null;
                }
            }

            Figure figure = Convert(deserialized);
            return figure;
        }

        public void ExportFile(IMidSurface f,  string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(MidSurfaceNameSpace.Component.MSModel));
            FileStream fs = new FileStream(filePath, FileMode.Open);
            serializer.Serialize(fs, f);
            fs.Close();
        }

        private Shape2D Parse(string filePath)
        {
            if (!ValidateXML(filePath))
            {
                return null;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(Shape2D));
            FileStream fs = new FileStream(filePath, FileMode.Open);
            Shape2D shape = (Shape2D)serializer.Deserialize(fs);
            fs.Close();

            //Can be used to invert path for model
            //string path = Path.GetDirectoryName(filePath);
            //string filename = Path.GetFileNameWithoutExtension(filePath);
            //if (!filename.Contains("_new"))
            //{
            //    fs = new FileStream(path + @"\" + filename + "_new.xml", FileMode.CreateNew);

            //    foreach (var contour in shape.Contour)
            //    {
            //        List<Point> tmp = contour.JointsOfSegments.ToList();
            //        tmp.Reverse(1, tmp.Count - 1);
            //        contour.JointsOfSegments = tmp.ToArray();
            //        for (int i = 0; i < contour.Segments.Count(); i++)
            //        {
            //            contour.Segments[i] = contour.Segments[i].Reverse().ToArray();
            //        }
            //        contour.Segments = contour.Segments.Reverse().ToArray();
            //    }

            //    serializer.Serialize(fs, shape);
            //    fs.Close();
            //}
            return shape;
        }

        private bool ValidateXML(string filePath)
        {
            try
            {
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                var resourceName = "Parser.ShapeSchema.xsd";

                Stream stream = assembly.GetManifestResourceStream(resourceName);
                XmlReaderSettings settings = new XmlReaderSettings();
                
                settings.Schemas.Add("", XmlReader.Create(stream));
                settings.ValidationType = ValidationType.Schema;

                XmlReader reader = XmlReader.Create(filePath, settings);
                XmlDocument document = new XmlDocument();
                document.Load(reader);

                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);

                // the following call to Validate succeeds.
                document.Validate(eventHandler);
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        private static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}", e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}", e.Message);
                    break;
            }
        }

        private Figure Convert(Shape2D shape)
        {
            Figure result = new Figure();
            foreach (var contour in shape.Contour)
            {
                Primitive.Contour convertContour = new Primitive.Contour();
                List<ParserPoint> points = new List<ParserPoint>();

                for (int i = 0; i < contour.Segments.Count() - 1; i++)
                {
                    points.Add(contour.JointsOfSegments[i]);

                    foreach (var point in contour.Segments[i])
                    {
                        points.Add(point);
                    }

                    points.Add(contour.JointsOfSegments[i + 1]);

                    convertContour.Add(new Segment(new BezierCurve(), ConvertPoints(points)));
                    points.Clear();
                }

                points.Add(contour.JointsOfSegments.Last());

                foreach (var point in contour.Segments.Last())
                {
                    points.Add(point);
                }

                points.Add(contour.JointsOfSegments.First());

                convertContour.Add(new Segment(new BezierCurve(), ConvertPoints(points)));
                result.Add(convertContour);
            }
            return result;
        }

        private List<Point> ConvertPoints(List<ParserPoint> points)
        {
            List<Point> convertedPoints = new List<Point>();
            foreach (var point in points)
            {
                convertedPoints.Add(new Point(point.X, point.Y));
            }
            return convertedPoints;
        }
    }
}
