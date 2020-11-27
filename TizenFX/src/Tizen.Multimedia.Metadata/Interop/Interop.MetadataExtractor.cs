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
using System.Runtime.InteropServices;
using Tizen.Multimedia;

internal static partial class Interop
{
    internal static partial class MetadataExtractor
    {
        [DllImport(Libraries.MetadataExtractor, EntryPoint = "metadata_extractor_create")]
        internal static extern MetadataExtractorError Create(out IntPtr handle);

        [DllImport(Libraries.MetadataExtractor, EntryPoint = "metadata_extractor_set_path")]
        internal static extern MetadataExtractorError SetPath(IntPtr handle, string path);

        [DllImport(Libraries.MetadataExtractor, EntryPoint = "metadata_extractor_set_buffer")]
        internal static extern MetadataExtractorError SetBuffer(IntPtr handle, IntPtr buffer, int size);

        [DllImport(Libraries.MetadataExtractor, EntryPoint = "metadata_extractor_destroy")]
        internal static extern MetadataExtractorError Destroy(IntPtr handle);

        [DllImport(Libraries.MetadataExtractor, EntryPoint = "metadata_extractor_get_metadata")]
        internal static extern MetadataExtractorError GetMetadata(IntPtr handle, MetadataExtractorAttr attribute, out IntPtr value);

        [DllImport(Libraries.MetadataExtractor, EntryPoint = "metadata_extractor_get_artwork")]
        internal static extern MetadataExtractorError GetArtwork(IntPtr handle, out IntPtr artwork,
            out int size, out IntPtr mimeType);

        [DllImport(Libraries.MetadataExtractor, EntryPoint = "metadata_extractor_get_frame")]
        internal static extern MetadataExtractorError GetFrame(IntPtr handle, out IntPtr frame, out int size);

        [DllImport(Libraries.MetadataExtractor, EntryPoint = "metadata_extractor_get_synclyrics")]
        internal static extern MetadataExtractorError GetSynclyrics(IntPtr handle, int index,
            out uint timeStamp, out IntPtr lyrics);

        [DllImport(Libraries.MetadataExtractor, EntryPoint = "metadata_extractor_get_frame_at_time")]
        internal static extern MetadataExtractorError GetFrameAtTime(IntPtr handle, uint timeStamp,
            bool isAccurate, out IntPtr frame, out int size);
    }
}
