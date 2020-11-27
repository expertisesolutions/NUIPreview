﻿/*
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

using Tizen.System;
using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class SystemInfo
    {
        internal enum SystemInfoValueType
        {
            Bool = 0,
            Int = 1,
            Double = 2,
            String = 3,
        }

        internal enum SystemInfoType
        {
            platform,
            Custom,
            None,
        }

        [DllImport(Libraries.SystemInfo, EntryPoint = "system_info_get_platform_type")]
        internal static extern InformationError SystemInfoGetPlatformType(string key, out SystemInfoValueType type);

        [DllImport(Libraries.SystemInfo, EntryPoint = "system_info_get_custom_type")]
        internal static extern InformationError SystemInfoGetCustomType(string key, out SystemInfoValueType type);

        [DllImport(Libraries.SystemInfo, EntryPoint = "system_info_get_platform_bool")]
        internal static extern InformationError SystemInfoGetPlatformBool(string key, out bool value);

        [DllImport(Libraries.SystemInfo, EntryPoint = "system_info_get_platform_int")]
        internal static extern InformationError SystemInfoGetPlatformInt(string key, out int value);

        [DllImport(Libraries.SystemInfo, EntryPoint = "system_info_get_platform_double")]
        internal static extern InformationError SystemInfoGetPlatformDouble(string key, out double value);

        [DllImport(Libraries.SystemInfo, EntryPoint = "system_info_get_platform_string")]
        internal static extern InformationError SystemInfoGetPlatformString(string key, out string value);

        [DllImport(Libraries.SystemInfo, EntryPoint = "system_info_get_custom_bool")]
        internal static extern InformationError SystemInfoGetCustomBool(string key, out bool value);

        [DllImport(Libraries.SystemInfo, EntryPoint = "system_info_get_custom_int")]
        internal static extern InformationError SystemInfoGetCustomInt(string key, out int value);

        [DllImport(Libraries.SystemInfo, EntryPoint = "system_info_get_custom_double")]
        internal static extern InformationError SystemInfoGetCustomDouble(string key, out double value);

        [DllImport(Libraries.SystemInfo, EntryPoint = "system_info_get_custom_string")]
        internal static extern InformationError SystemInfoGetCustomString(string key, out string value);
    }
}
