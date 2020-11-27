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

namespace Tizen.Applications.Notifications
{
    using Tizen.Common;

    /// <summary>
    /// This class contains common properties and methods of notifications.
    /// </summary>
    /// <remarks>
    /// A notification is a message that is displayed on the notification area.
    /// It is created to notify information to the user through the application.
    /// This class helps you to provide method and property for creating notification object.
    /// </remarks>
    public sealed partial class Notification
    {
        /// <summary>
        ///  Class for notification AccessorySet, which includes vibration, LED, and sound option.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public sealed class AccessorySet : MakerBase
        {
            /// <summary>
            /// Gets or sets the sound option. Default to AccessoryOption.Off.
            /// </summary>
            /// <remarks>
            /// If you set AccessoryOption.Custom, you must the SoundPath. Otherwise, an exception is thrown.
            /// </remarks>
            /// <since_tizen> 3 </since_tizen>
            public AccessoryOption SoundOption { get; set; } = AccessoryOption.Off;

            /// <summary>
            /// Gets or sets the sound path, It will play on the sound file you set.
            /// You should set an absolute path for a sound file.
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public string SoundPath { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether vibration is operated. Default is false.
            /// </summary>
            /// <since_tizen> 3 </since_tizen>
            public bool CanVibrate { get; set; } = false;

            /// <summary>
            /// Gets or sets the led option. The default value is AccessoryOption.Off.
            /// </summary>
            /// <remarks>
            /// If you set AccessoryOption.Custom and not set LedColor, the LED will show default color.
            /// </remarks>
            /// <since_tizen> 3 </since_tizen>
            public AccessoryOption LedOption { get; set; } = AccessoryOption.Off;

            /// <summary>
            /// Gets or sets the on time so that it looks like the device's LED is blinking.
            /// </summary>
            /// <remarks>
            /// Default value of LedOnMillisecond is 0.
            /// The rate is specified in terms of the number of Milliseconds to be on.
            /// You must set the on and off times at the same time. Otherwise, it may not operate normally.
            /// </remarks>
            /// <since_tizen> 3 </since_tizen>
            public int LedOnMillisecond { get; set; }

            /// <summary>
            /// Gets or sets the off time so that it looks like the device's LED is blinking.
            /// </summary>
            /// <remarks>
            /// The rate is specified in terms of the number of Milliseconds to be off.
            /// You must set the on and off times at the same time. Otherwise, it may not operate normally.
            /// </remarks>
            /// <since_tizen> 3 </since_tizen>
            public int LedOffMillisecond { get; set; }

            /// <summary>
            /// Gets or sets the LED color that you would like the LED on the device to blink.
            /// </summary>
            /// <remarks>
            /// If you want to set LedColor, you should always set LedOption as AccessoryOption.Custom, otherwise, it may operate default LED color.
            /// </remarks>
            /// <since_tizen> 3 </since_tizen>
            public Color LedColor { get; set; }

            internal override void Make(Notification notification)
            {
                AccessorySetBinder.BindObject(notification);
            }
        }
    }
}
