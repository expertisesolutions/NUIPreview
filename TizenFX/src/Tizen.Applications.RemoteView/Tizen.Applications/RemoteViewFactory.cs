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

using ElmSharp;
using System;

namespace Tizen.Applications
{
    /// <summary>
    /// Represents a factory class for making the RemoteView objects.
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public static class RemoteViewFactory
    {
        private static bool _ready;

        /// <summary>
        /// Initializes RemoteViewFactory.
        /// </summary>
        /// <param name="win">Window object that will contain RemoteViews that are generated by RemoteViewFactory.
        /// All the remote views will be located in the specified window object.
        /// </param>
        /// <privilege>http://tizen.org/privilege/widget.viewer</privilege>
        /// <exception cref="InvalidOperationException">Thrown when this operation failed.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when this operation is denied.</exception>
        /// <exception cref="NotSupportedException">Thrown when this operation is not supported for this device.</exception>
        /// <since_tizen> 3 </since_tizen>
        public static void Init(EvasObject win)
        {
            if (_ready)
                throw new InvalidOperationException("Already initialized");
            RemoteView.CheckException(Interop.WidgetViewerEvas.Init(win));
            _ready = true;
        }

        /// <summary>
        /// Creates a RemoteView object.
        /// </summary>
        /// <param name="parent">Parent object.</param>
        /// <param name="widgetId">Widget ID.</param>
        /// <param name="content">Contents that will be given to the widget instance.</param>
        /// <param name="period">Update period.</param>
        /// <param name="previewImage">True if you want to show the preview image.</param>
        /// <param name="overlayText">True if you want to show the overlay text.</param>
        /// <param name="loadingMessage">True if you want to show the loading message.</param>
        /// <returns>RemoteView object.</returns>
        /// <privilege>http://tizen.org/privilege/widget.viewer</privilege>
        /// <exception cref="InvalidOperationException">Thrown when this operation failed.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when this operation is denied.</exception>
        /// <exception cref="NotSupportedException">Thrown when this operation is not supported for this device.</exception>
        /// <since_tizen> 3 </since_tizen>
        public static RemoteView Create(EvasObject parent, string widgetId, string content, double period,
            bool previewImage = true, bool overlayText = true, bool loadingMessage = true)
        {
            if (!_ready)
                throw new InvalidOperationException("Not initialized");

            var obj = new RemoteView()
            {
                Layout = new RemoteWindow(parent, widgetId, content, period)
            };

            if (!previewImage)
                obj.HidePreviewImage();

            if (!overlayText)
                obj.HideOverlayText();

            if (!loadingMessage)
                obj.HideLoadingMessage();

            return obj;
        }

        /// <summary>
        /// Finalizes the RemoteViewFactory.
        /// </summary>
        /// <privilege>http://tizen.org/privilege/widget.viewer</privilege>
        /// <exception cref="InvalidOperationException">Thrown when this operation failed.</exception>
        /// <exception cref="UnauthorizedAccessException">Thrown when this operation is denied.</exception>
        /// <exception cref="NotSupportedException">Thrown when this operation is not supported for this device.</exception>
        /// <since_tizen> 3 </since_tizen>
        public static void Shutdown()
        {
            if (!_ready)
                throw new InvalidOperationException("Not initialized");
            RemoteView.CheckException(Interop.WidgetViewerEvas.Fini());
            _ready = false;
        }

    }
}
