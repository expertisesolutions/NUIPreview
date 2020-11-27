/*
 * Copyright (c) 2019 Samsung Electronics Co., Ltd All Rights Reserved
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

namespace Tizen.Multimedia.Remoting
{
    /// <summary>
    /// Provides data for the <see cref="MediaController.SubtitleModeUpdated"/> event.
    /// </summary>
    /// <since_tizen> 6 </since_tizen>
    public class SubtitleModeUpdatedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubtitleModeUpdatedEventArgs"/> class.
        /// </summary>
        /// <param name="isEnabled">A value indicating the updated subtitle mode.</param>
        /// <since_tizen> 6 </since_tizen>
        internal SubtitleModeUpdatedEventArgs(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        /// <summary>
        /// Gets the value indicating whether subtitle is enabled or not.
        /// </summary>
        /// <since_tizen> 6 </since_tizen>
        public bool IsEnabled { get; }
    }
}