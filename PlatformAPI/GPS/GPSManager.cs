using System;
using System.Collections.Generic;
using System.Text;

namespace PlatformAPI.GPS
{
    public class GPSManager
    {
        private GPSManager() 
        {
            this.updateDataHandler = new EventHandler(UpdateData);

            this.gps.DeviceStateChanged += new DeviceStateChangedEventHandler(gps_DeviceStateChanged);
            this.gps.LocationChanged += new LocationChangedEventHandler(gps_LocationChanged);
        }
        
        public static GPSManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockHelper)
                    {
                        if (instance == null)
                            instance = new GPSManager();
                    }
                }
                return instance;
            }
        }

        public bool GPSOpened
        {
            get { return gps.Opened; }
        }

        public void OpenGPS()
        {
            if (!this.gps.Opened)
            {
                this.gps.Open();
            }
        }

        public void CloseGPS()
        {
            if (this.gps.Opened)
            {
                this.gps.Close();
            }
        }

        public event EventHandler GPSStateChanged;

        protected void gps_LocationChanged(object sender, LocationChangedEventArgs args)
        {
            this.position = args.Position;

            this.UpdateData(sender, args);

        }

        protected void gps_DeviceStateChanged(object sender, DeviceStateChangedEventArgs args)
        {
            this.device = args.DeviceState;

            this.UpdateData(sender, args);
        }
        
        private void UpdateData(object sender, System.EventArgs args)
        {
            if (gps.Opened)
            {
                StringBuilder builder = new StringBuilder();
                if (device != null)
                {
                    builder.Append(
                        string.Format("{0} {1},{2}\n",
                            device.FriendlyName,
                            device.ServiceState,
                            device.DeviceState));
                }

                if (position != null)
                {
                    if (position.LatitudeValid)
                    {
                        GPSInfo.Latitude = position.Latitude;
                        GPSInfo.LatitudeDMS = position.LatitudeInDegreesMinutesSeconds.ToString();

                        builder.Append(
                            string.Format("Latitude (DD):\n   {0}\nLatitude (D,M,S):\n   {1}",
                                GPSInfo.Latitude,
                                GPSInfo.LatitudeDMS));
                    }

                    if (position.LongitudeValid)
                    {
                        GPSInfo.Longitude = position.Longitude;
                        GPSInfo.LongitudeDMS = position.LongitudeInDegreesMinutesSeconds.ToString();

                        builder.Append(
                            string.Format("Longitude (DD):\n   {0}\nLongitude (D,M,S):\n   {1}",
                                GPSInfo.Longitude,
                                GPSInfo.LongitudeDMS));
                    }

                    if (position.SatellitesInSolutionValid &&
                        position.SatellitesInViewValid &&
                        position.SatelliteCountValid)
                    {
                        GPSInfo.SatelliteCount = position.GetSatellitesInSolution().Length.ToString();

                        builder.Append(
                            string.Format("Satellite Count:\n   {0}/{1}({2})\n",
                                GPSInfo.SatelliteCount,
                                position.GetSatellitesInView().Length,
                                position.SatelliteCount));
                    }

                    if (position.TimeValid)
                    {
                        builder.Append(
                            string.Format("Time:\n   {0}\n",
                                position.Time.ToString()));
                    }
                }

                GPSInfo.InfoStr = builder.ToString();

            }

            GPSInfo.LocationValid = gps.Opened &&
                position != null &&
                position.LongitudeValid &&
                position.LatitudeValid;

            if (GPSStateChanged != null)
            {
                GPSStateChanged.Invoke(this, new EventArgs());
            }
        }

        protected GpsDeviceState device = null;

        protected GpsPosition position = null;

        protected Gps gps = new Gps();

        private volatile static GPSManager instance = null;

        private EventHandler updateDataHandler;

        private static readonly object lockHelper = new object();
    }
}
