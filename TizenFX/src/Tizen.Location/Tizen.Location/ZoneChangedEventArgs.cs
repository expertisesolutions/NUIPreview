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

namespace Tizen.Location
{
    /// <summary>
    /// An extended EventArgs class which contains the changed zone status.
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public class ZoneChangedEventArgs : EventArgs
    {
        /// <summary>
        /// The class constructor for the ZoneChangedEventArgs class.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="state"> An enumeration of type BoundaryState.</param>
        /// <param name="latitude">The latitude value [-90.0 ~ 90.0] \(degrees).</param>
        /// <param name="longitude">The longitude value [-180.0 ~ 180.0] \(degrees).</param>
        /// <param name="altitude">The altitude value.</param>
        /// <param name="timestamp">The timestamp value.</param>
        public ZoneChangedEventArgs(BoundaryState state, double latitude, double longitude, double altitude, DateTime timestamp)
        {
            BoundState = state;
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
            Timestamp = timestamp;
        }

        /// <summary>
        /// Gets the boundary state.
        /// </summary>
        /// <since_tizen>3</since_tizen>
        public BoundaryState BoundState { get; private set; }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public double Latitude { get; private set; }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        /// <since_tizen>3</since_tizen>
        public double Longitude { get; private set; }

        /// <summary>
        /// Gets the altitude.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public double Altitude { get; private set; }

        /// <summary>
        /// Method to get the timestamp.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public DateTime Timestamp { get; private set; }
    }
}
