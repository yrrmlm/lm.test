using System;

namespace lm.tool
{
    public class CoordHelper
    {
        /// <summary>
        /// 根据角度获取边上点位置
        /// </summary>
        /// <param name="center">中心点</param>
        /// <param name="angle">角度</param>
        /// <param name="radius">半径</param>
        /// <returns></returns>
        public Point GetPointByAngle(Point center,double angle,double radius)
        {
            var p = new Point();
            double angleHude = angle * Math.PI / 180;/*角度变成弧度*/
            p.X = (int)(radius * Math.Cos(angleHude)) + center.X;
            p.Y = (int)(radius * Math.Sin(angleHude)) + center.Y;
            return p;
        }

    }

    public class Point
    {
        public double X { get; set; }

        public double Y { get; set; }
    }

}
