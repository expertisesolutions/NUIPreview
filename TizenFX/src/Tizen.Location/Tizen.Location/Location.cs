/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;

namespace Tizen.Location
{
    /// <summary>
    /// This class contains details of the location requested.
    /// Includes the functionality to get the distance between locations.
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public class Location
    {
        private double _latitude;
        private double _longitude;
        private double _altitude;
        private double _speed;
        private double _direction;
        private double _accuracy;
        internal int _timestamp;

        /// <summary>
        /// The default constructor of the Location class.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public Location()
        {
        }

        /// <summary>
        /// The parameterized constructor of the Location class.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="latitude">The latitude component of the device co-ordinate [-90.0 ~ 90.0] \(degrees).</param>
        /// <param name="longitude">The longitude component of the device co-ordinate[-180.0 ~ 180.0] \(degrees).</param>
        /// <param name="altitude">The altitude value.</param>
        /// <param name="accuracy">The accuracy in meters.</param>
        /// <param name="speed">The device speed.</param>
        /// <param name="direction">The device direction with respect to the north.</param>
        /// <param name="timestamp"> Time when the measurement took place.</param>
        /// <exception cref="ArgumentException">Thrown when an invalid argument is used.</exception>
        public Location(double latitude, double longitude, double altitude, double speed, double direction, double accuracy, int timestamp)
        {
            _latitude = latitude;
            _longitude = longitude;
            _altitude = altitude;
            _speed = speed;
            _direction = direction;
            _accuracy = accuracy;
            _timestamp = timestamp;
        }

        /// <summary>
        /// The current latitude [-90.0 ~ 90.0] \(degrees).
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public double Latitude
        {
            get
            {
                Log.Info(Globals.LogTag, "Getting the latitude");
                return _latitude;
            }
            set
            {
                Log.Info(Globals.LogTag, "Setting the latitude");
                if (value >= -90.0 && value <= 90.0)
                {
                    _latitude = value;
                }
                else
                {
                    Log.Error(Globals.LogTag, "Error setting latitude");
                    throw LocationErrorFactory.ThrowLocationException((int)LocationError.InvalidParameter);
                }
            }
        }

        /// <summary>
        /// The current longitude [-180.0 ~ 180.0] \(degrees).
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public double Longitude
        {
            get
            {
                Log.Info(Globals.LogTag, "Getting the longitude");
                return _longitude;
            }
            set
            {
                Log.Info(Globals.LogTag, "Setting the longitude");
                if (value >= -180.0 && value <= 180.0)
                {
                    _longitude = value;
                }
                else
                {
                    Log.Error(Globals.LogTag, "Error setting longitude");
                    throw LocationErrorFactory.ThrowLocationException((int)LocationError.InvalidParameter);
                }
            }
        }

        /// <summary>
        /// The current altitude (meters).
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public double Altitude
        {
            get
            {
                return _altitude;
            }
            set
            {
                _altitude = value;
            }
        }

        /// <summary>
        /// The device speed (km/h).
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public double Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }

        /// <summary>
        /// The direction and degrees from the north.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public double Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
            }
        }

        /// <summary>
        /// The accuracy.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public double Accuracy
        {
            get
            {
                return _accuracy;
            }
            set
            {
                _accuracy = value;
            }
        }

        /// <summary>
        /// The time value when the measurement was done.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public DateTime Timestamp
        {
            get
            {
                return Interop.ConvertDateTime(_timestamp);
            }
            internal set
            {
                Timestamp = value;
            }

        }

        /// <summary>
        /// Gets the distance between the two given coordinates.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="startLatitude">The latitude of the source location [-90.0 ~ 90.0] \(degrees).</param>
        /// <param name="startLongitude">The longitude of the source location[-180.0 ~ 180.0] \(degrees).</param>
        /// <param name="endLatitude">The latitude of the source location [-90.0 ~ 90.0] \(degrees).</param>
        /// <param name="endLongitude">The longitude of the source location[-180.0 ~ 180.0] \(degrees).</param>
        /// <returns>Returns the distance between the source and the destination.</returns>
        /// <exception cref="ArgumentException">Thrown when an invalid argument is used.</exception>
        /// <exception cref="NotSupportedException">Thrown when the location is not supported.</exception>
        public static double GetDistanceBetween(double startLatitude, double startLongitude, double endLatitude, double endLongitude)
        {
            double result;
            Log.Info(Globals.LogTag, "Calling GetDistanceBetween");
            int ret = Interop.Location.GetDistanceBetween(startLatitude, startLongitude, endLatitude, endLongitude, out result);
            if (((LocationError)ret != LocationError.None))
            {
                Log.Error(Globals.LogTag, "Error getting single distance information ," + (LocationError)ret);
                throw LocationErrorFactory.ThrowLocationException(ret);
            }
            return result;
        }

        /// <summary>
        /// Gets the distance between the current and the specified location.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="location"> The location object to which distance is to be calculated.</param>
        /// <returns>Returns the distance to the specified location.</returns>
        /// <exception cref="ArgumentException">Thrown when an invalid argument is used.</exception>
        /// <exception cref="NotSupportedException">Thrown when the location is not supported.</exception>
        public double GetDistanceTo(Location location)
        {
            double result;
            Log.Info(Globals.LogTag, "Calling GetDistanceTo");
            int ret = Interop.Location.GetDistanceBetween(this.Latitude, this.Longitude, location.Latitude, location.Longitude, out result);
            if (((LocationError)ret != LocationError.None))
            {
                Log.Error(Globals.LogTag, "Error getting distance information to the specifed location," + (LocationError)ret);
                throw LocationErrorFactory.ThrowLocationException(ret);
            }
            return result;
        }
    }
}
