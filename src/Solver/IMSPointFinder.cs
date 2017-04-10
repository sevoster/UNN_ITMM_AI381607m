﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MidSurfaceNameSpace.Solver
{
    public interface IMSPointFinder
    {
        List<IMSPoint> FindMSPoints(List<ICustomLine> customline);
        IMSPoint GetMSPoint(Vector vector, Point point, ICustomLine line);
    }
}
