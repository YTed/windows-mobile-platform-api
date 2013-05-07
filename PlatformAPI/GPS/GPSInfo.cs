using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformAPI.GPS
{
    public static class GPSInfo
    {
        public static bool LocationValid = false;
        
        /// <summary>
        /// 经度
        /// </summary>
        public static double Longitude = 0;

        public static string LongitudeDMS = null;

        /// <summary>
        /// 纬度
        /// </summary>
        public static double Latitude = 0;

        public static string LatitudeDMS = null;

        /// <summary>
        /// 卫星数量
        /// </summary>
        public static string SatelliteCount = null;

        /// <summary>
        /// 
        /// </summary>
        public static string InfoStr = null;

    }
}
