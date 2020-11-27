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

namespace Tizen.Multimedia
{
    /// <summary>
    /// Provides data for the <see cref="Recorder.RecordingStatusChanged"/> event.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class RecordingStatusChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordingStatusChangedEventArgs"/> class
        /// with the specified elapsed time and file size.
        /// </summary>
        /// <param name="elapsedTime">The time of the recording in milliseconds.</param>
        /// <param name="fileSize">The size of the recording in kilobytes.</param>
        /// <since_tizen> 4 </since_tizen>
        public RecordingStatusChangedEventArgs(long elapsedTime, long fileSize)
        {
            ElapsedTime = elapsedTime;
            FileSize = fileSize;
        }

        /// <summary>
        /// Gets the time of the recording in milliseconds.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public long ElapsedTime { get; }

        /// <summary>
        /// Gets the size of the recording file in kilobytes.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public long FileSize { get; }
    }
}
