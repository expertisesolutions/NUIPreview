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
    /// Provides data for the <see cref="Recorder.MuxedStreamDelivered"/> event.
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public class MuxedStreamDeliveredEventArgs : EventArgs
    {
        internal MuxedStreamDeliveredEventArgs(IMediaBuffer stream, ulong offset)
        {
            Stream = stream;
            Offset = (long)offset;
        }

        /// <summary>
        /// Gets the stream buffer that will be recordered.
        /// </summary>
        /// <remarks>
        /// This buffer is read-only and only valid in the event.<br/>
        /// Any attempt to access to this buffer after the event ends will throw an exception.
        /// </remarks>
        /// <since_tizen> 3 </since_tizen>
        public IMediaBuffer Stream { get; }

        /// <summary>
        /// The file offset where the buffer will be written.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public long Offset { get; }
    }
}
