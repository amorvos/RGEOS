// Copyright 2005, 2006 - Morten Nielsen (www.iter.dk)
//
// This file is part of SharpMap.
// SharpMap is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpMap is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System.Drawing;
using SharpMap.Geometries;
using Point = SharpMap.Geometries.Point;
using RGeos.PluginEngine;
using RGeos.Geometry;

namespace SharpMap.Utilities
{
    /// <summary>
    /// Class for transforming between world and image coordinate
    /// </summary>
    public class Transform2
    {
        /// <summary>
        /// Transforms from world coordinate system (WCS) to image coordinates
        /// 将世界坐标转换为image坐标
        /// NOTE: This method DOES NOT take the MapTransform property into account (use SharpMap.Map.MapToWorld instead)
        /// </summary>
        /// <param name="p">Point in WCS</param>
        /// <param name="map">Map reference</param>
        /// <returns>Point in image coordinates</returns>
        public static PointF WorldtoMap(Point p, Map map)
        {
            //if (map.MapTransform != null && !map.MapTransform.IsIdentity)
            //	map.MapTransform.TransformPoints(new System.Drawing.PointF[] { p });
            PointF result = new System.Drawing.Point();
            double Height = (map.Zoom * map.Size.Height) / map.Size.Width;
            double left = map.Center.X - map.Zoom * 0.5;
            double top = map.Center.Y + Height * 0.5 * map.PixelAspectRatio;
            result.X = (float)((p.X - left) / map.PixelWidth);
            result.Y = (float)((top - p.Y) / map.PixelHeight);
            return result;
        }

        /// <summary>
        /// 像素坐标转换成地图坐标
        /// NOTE: This method DOES NOT take the MapTransform property into account (use SharpMap.Map.MapToWorld instead)
        /// </summary>
        /// <param name="p">Point in image coordinate system</param>
        /// <param name="map">Map reference</param>
        /// <returns>Point in WCS</returns>
        public static Point MapToWorld(PointF p, Map map)
        {
            //if (this.MapTransform != null && !this.MapTransform.IsIdentity)
            //{
            //    System.Drawing.PointF[] p2 = new System.Drawing.PointF[] { p };
            //    this.MapTransform.TransformPoints(new System.Drawing.PointF[] { p });
            //    this.MapTransformInverted.TransformPoints(p2);
            //    return Utilities.Transform.MapToWorld(p2[0], this);
            //}
            //else 
            REnvelope env = map.Extent;
            return new Point(env.LowLeft.X + p.X * map.PixelWidth,
                             env.TopRight.Y - p.Y * map.PixelHeight);
        }
    }
}