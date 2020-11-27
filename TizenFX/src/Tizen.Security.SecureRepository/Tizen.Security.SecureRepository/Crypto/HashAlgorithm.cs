﻿/*
 *  Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License
 */

namespace Tizen.Security.SecureRepository.Crypto
{
    /// <summary>
    /// Enumeration for the hash algorithm.
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public enum HashAlgorithm : int
    {
        /// <summary>
        /// The no hash algorithm.
        /// </summary>
        None = 0,
        /// <summary>
        /// The hash algorithm SHA1.
        /// </summary>
        Sha1,
        /// <summary>
        /// The hash algorithm SHA256.
        /// </summary>
        Sha256,
        /// <summary>
        /// The hash algorithm SHA384.
        /// </summary>
        Sha384,
        /// <summary>
        /// The hash algorithm SHA512.
        /// </summary>
        Sha512
    }
}
