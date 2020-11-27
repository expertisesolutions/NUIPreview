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

using System.Runtime.InteropServices;
using System;

internal static partial class Interop
{
    internal static partial class Libraries
    {
        public const string PlatformConfig = "libtzplatform-config-2.0.so.2";
    }

    internal static partial class PlatformConfig
    {
        [DllImport(Libraries.PlatformConfig, EntryPoint = "tzplatform_getenv", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr _GetEnv(int id);

        [DllImport(Libraries.PlatformConfig, EntryPoint = "tzplatform_getid", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int GetId(string name);

        internal static string GetEnv(int id)
        {
            var text = _GetEnv(id);
            return Marshal.PtrToStringAnsi(text);
        }
    }
}
