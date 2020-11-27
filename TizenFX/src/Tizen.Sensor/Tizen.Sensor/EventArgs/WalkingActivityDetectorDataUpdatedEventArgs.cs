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

namespace Tizen.Sensor
{
    /// <summary>
    /// The WalkingActivityDetector changed event arguments class is used for storing the data returned by a walking activity detector.
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public class WalkingActivityDetectorDataUpdatedEventArgs : EventArgs
    {
        internal WalkingActivityDetectorDataUpdatedEventArgs(float state)
        {
            Walking = (DetectorState) state;
        }

        /// <summary>
        /// Gets the walking state.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <value> The walking state. </value>
        public DetectorState Walking { get; private set; }
    }
}
