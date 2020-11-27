/*
 * Copyright (c) 2017 Samsung Electronics Co., Ltd All Rights Reserved
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

namespace Tizen.Applications.NotificationEventListener
{
    using Tizen.Common;

    /// <summary>
    /// This class provides methods and properties to get information about the posted or updated notification.
    /// </summary>
    public partial class NotificationEventArgs
    {
        /// <summary>
        /// Class to get infomation about notification accessory.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public class AccessoryArgs
        {
            /// <summary>
            /// Gets the sound option.
            /// </summary>
            /// <since_tizen> 4 </since_tizen>
            public AccessoryOption SoundOption { get; internal set; }

            /// <summary>
            /// Gets the sound path.
            /// </summary>
            /// <since_tizen> 4 </since_tizen>
            public string SoundPath { get; internal set; }

            /// <summary>
            /// Gets the vibration option.
            /// </summary>
            /// <since_tizen> 4 </since_tizen>
            public bool CanVibrate { get; internal set; }

            /// <summary>
            /// Gets the LED option.
            /// </summary>
            /// <since_tizen> 4 </since_tizen>
            public AccessoryOption LedOption { get; internal set; }

            /// <summary>
            /// Gets LED on time period that you would like the LED on the device to blink as well as the rate.
            /// </summary>
            /// <value>
            /// Default value of LedOnMilliseconds is 0.
            /// The rate is specified in terms of the number of milliseconds to be on.
            /// </value>
            /// <since_tizen> 4 </since_tizen>
            public int LedOnMillisecond { get; internal set; }

            /// <summary>
            /// Gets LED on time period that you would like the LED on the device to blink as well as the rate.
            /// </summary>
            /// <value>
            /// Default value of LedOffMillisecond is 0.
            /// The rate is specified in terms of the number of millisecond to be off.
            /// </value>
            /// <since_tizen> 4 </since_tizen>
            public int LedOffMillisecond { get; internal set; }

            /// <summary>
            /// Gets LED color that you would like the LED on the device to blink.
            /// </summary>
            /// <since_tizen> 4 </since_tizen>
            public Color LedColor { get; internal set; }
        }
    }
}
