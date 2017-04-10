﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidSurfaceNameSpace.Primitive;
using System.Windows;

namespace MidSurfaceNameSpace.Solver
{
    public partial class MSPointFinder : IMSPointFinder
    {
        List<ICustomLine> custompoints;
        double Rmax;

        public MSPointFinder()
        {
            //this.Rmax = getRmax();
        }

        public List<IMSPoint> FindMSPoints(List<ICustomLine> custompoints)
        {
            this.custompoints = custompoints;
            List<IMSPoint> mspoints = new List<IMSPoint>();

            for (int i = 0; i < custompoints.Count(); i++)
            {
                ICustomPoint point1 = custompoints[i].GetPoint1();
                ICustomPoint point2 = custompoints[i].GetPoint2();
            
                double X = (point2.GetPoint().X + point1.GetPoint().X) / 2;
                double Y = (point2.GetPoint().Y + point1.GetPoint().Y) / 2;
                
                mspoints.Add(GetMSPoint(custompoints[i].GetRightNormal(), new Point(X, Y), custompoints[i]));
            }
            return mspoints;
        }

        public IMSPoint GetMSPoint(Vector vector, Point point, ICustomLine line)
        {
            double Rmax = getRmax();
            double Rmin = 0;
            double R = Rmax;
            Point center = new Point(point.X + vector.X * R, point.Y + vector.Y * R);
            List<Point> pCross = cross(center, R, new Point(point.X, point.Y));
            while (pCross.Count != 1 || Rmax != Rmin)
            {
                if (pCross.Count >= 2)
                {
                    R = (Rmax + Rmin) / 2;
                    Rmax = R;
                }
                else
                {
                    R = (Rmax + Rmin) / 2;
                    Rmin = R;
                }

                center.X = point.X + vector.X * R;
                center.Y = point.Y + vector.Y * R;
                pCross = cross(center, R, point);
            }

            return new MSPoint(point, line);
        }

        List<Point> cross(Point center, double rad, Point rivol)
        {
            List<Point> result = new List<Point>();

            double x0 = center.X;
            double y0 = center.Y;
            for (int i =0; i < custompoints.Count(); i++)
            {
                double x1 = this.custompoints[i].GetPoint1().GetPoint().X;
                double y1 = this.custompoints[i].GetPoint1().GetPoint().Y;           
                double x2 = this.custompoints[i].GetPoint2().GetPoint().X;
                double y2 = this.custompoints[i].GetPoint2().GetPoint().Y;
                double q = x0 * x0 + y0 * y0 - rad * rad;
                double k = -2.0 * x0;
                double l = -2.0 * y0;

                double z = x1 * y2 - x2 * y1;
                double p = y1 - y2;
                double s = x1 - x2;

                if (EqualDoubles(s, 0.0, 0.001))
                {
                    s = 0.001;
                }

                double A = s * s + p * p;
                double B = s * s * k + 2.0 * z * p + s * l * p;
                double C = q * s * s + z * z + s * l * z;

                double D = B * B - 4.0 * A * C;

                if (D < 0.0)
                {
                    //return 0;
                }
                else if (D < 0.001)
                {
                    double xa = -B / (2.0 * A);
                    double ya = (p * xa + z) / s;
                    result.Add(new Point(xa, ya));
                    //return 1;
                }
                else
                {
                    double xa = (-B + Math.Sqrt(D)) / (2.0 * A);
                    double ya = (p * xa + z) / s;

                    double xb = (-B - Math.Sqrt(D)) / (2.0 * A);
                    double yb = (p * xb + z) / s;

                    result.Add(new Point(xa, ya));
                    result.Add(new Point(xb, yb));
                }

                //return 2;

                if (result.Count() >= 2)
                    break; 
            }


            return result;
        }
        bool EqualDoubles(double n1, double n2, double precision_)
        {
            return (Math.Abs(n1 - n2) <= precision_);
        }


        double getRmax()
        {
            double R, Xmax = 0, Xmin = double.MaxValue, Ymax = 0, Ymin = double.MaxValue;
            for (int i = 0; i < this.custompoints.Count; i++)
            {
                double X = this.custompoints[i].GetPoint1().GetPoint().X;
                double Y = this.custompoints[i].GetPoint1().GetPoint().Y;
                if (X < Xmin)
                    Xmin = X;
                else if (X > Xmax)
                    Xmax = X;
                if (Y < Ymin)
                    Ymin = Y;
                else if (Y > Ymax)
                    Ymax = Y;
            }
            R = Math.Max(Xmax - Xmin, Ymax - Ymin);
            return R;
        }
    }
}
