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
    /// The exception that is thrown when noncompliance with the sound system policy happens.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class AudioPolicyException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioPolicyException"/> class.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public AudioPolicyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioPolicyException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <since_tizen> 4 </since_tizen>
        public AudioPolicyException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioPolicyException"/> class with a specified error message and inner exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception..</param>
        /// <since_tizen> 4 </since_tizen>
        public AudioPolicyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
