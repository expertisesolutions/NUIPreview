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


using static Tizen.Uix.Tts.SupportedVoice;

namespace Tizen.Uix.Tts
{
    /// <summary>
    /// This class holds information related to the EngineChanged event.
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    public class EngineChangedEventArgs
    {
        internal EngineChangedEventArgs(string engineId, string language, int voiceType, bool needCredential)
        {
            this.EngineId = engineId;
            this.VoiceType = new SupportedVoice(language, voiceType);
            this.NeedCredential = needCredential;
        }

        /// <summary>
        /// The engine ID.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public string EngineId
        {
            get;
            internal set;
        }

        /// <summary>
        /// The necessity of the credential.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public bool NeedCredential
        {
            get;
            internal set;
        }

        /// <summary>
        /// The supported voice.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public SupportedVoice VoiceType
        {
            get;
            internal set;
        }
    }
}
