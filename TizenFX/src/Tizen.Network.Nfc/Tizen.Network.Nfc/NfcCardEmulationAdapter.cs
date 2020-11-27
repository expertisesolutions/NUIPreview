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

using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Tizen.Network.Nfc
{
    /// <summary>
    /// A class for the NFC CardEmulation mode. It allows applications to handle Card Emulation informations.
    /// </summary>
    /// <since_tizen> 3 </since_tizen>
    /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
    public class NfcCardEmulationAdapter : IDisposable
    {
        private bool disposed = false;

        private event EventHandler<SecureElementEventArgs> _secureElementEvent;
        private event EventHandler<SecureElementTranscationEventArgs> _secureElementTransactionEvent;
        private event EventHandler<HostCardEmulationEventArgs> _hostCardEmulationEvent;

        private Interop.Nfc.SecureElementEventCallback _secureElementEventCallback;
        private Interop.Nfc.SecureElementTransactionEventCallback _secureElementTransactionEventCallback;
        private Interop.Nfc.HostCardEmulationEventCallback _hostCardEmulationEventCallback;

        /// <summary>
        /// An event that is called when receiving the Secure Element (SIM/UICC(Universal Integrated Circuit Card)) event.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        public event EventHandler<SecureElementEventArgs> SecureElementEvent
        {
            add
            {
                if (_secureElementEvent == null)
                {
                    RegisterSecureElementEvent();
                }
                _secureElementEvent += value;
            }
            remove
            {
                _secureElementEvent -= value;
                if (_secureElementEvent == null)
                {
                    UnregisterSecureElementEvent();
                }
            }
        }

        /// <summary>
        /// An event that is called when receiving the Secure Element (SIM/UICC (Universal Integrated Circuit Card)) transaction event for the 'ESE(SmartMX)' type.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        public event EventHandler<SecureElementTranscationEventArgs> EseSecureElementTransactionEvent
        {
            add
            {
                if (_secureElementTransactionEvent == null)
                {
                    RegisterSecureElementTransactionEvent(NfcSecureElementType.EmbeddedSE);
                }
                _secureElementTransactionEvent += value;
            }
            remove
            {
                _secureElementTransactionEvent -= value;
                if (_secureElementTransactionEvent == null)
                {
                    UnregisterSecureElementTransactionEvent(NfcSecureElementType.EmbeddedSE);
                }
            }
        }

        /// <summary>
        /// An event that is called when receiving the Secure Element (SIM/UICC (Universal Integrated Circuit Card)) transaction event for the 'UICC' type.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        public event EventHandler<SecureElementTranscationEventArgs> UiccSecureElementTransactionEvent
        {
            add
            {
                if (_secureElementTransactionEvent == null)
                {
                    RegisterSecureElementTransactionEvent(NfcSecureElementType.Uicc);
                }
                _secureElementTransactionEvent += value;
            }
            remove
            {
                _secureElementTransactionEvent -= value;
                if (_secureElementTransactionEvent == null)
                {
                    UnregisterSecureElementTransactionEvent(NfcSecureElementType.Uicc);
                }
            }
        }

        /// <summary>
        /// An event that is called when receiving the HCE (Host Card Emulation) event.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        public event EventHandler<HostCardEmulationEventArgs> HostCardEmulationEvent
        {
            add
            {
                if (_hostCardEmulationEvent == null)
                {
                    RegisterHostCardEmulationEvent();
                }
                _hostCardEmulationEvent += value;
            }
            remove
            {
                _hostCardEmulationEvent -= value;
                if (_hostCardEmulationEvent == null)
                {
                    UnregisterHostCardEmulationEvent();
                }
            }
        }

        internal NfcCardEmulationAdapter()
        {
        }

        /// <summary>
        /// NfcCardEmulationAdpater destructor.
        /// </summary>
        ~NfcCardEmulationAdapter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free managed objects.
            }
            //Free unmanaged objects
            disposed = true;
        }

        /// <summary>
        /// Enables the card emulation mode.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        /// <exception cref="NotSupportedException">Thrown when the NFC is not supported.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the method fails due to an invalid operation.</exception>
        public void EnableCardEmulation()
        {
            int ret = Interop.Nfc.CardEmulation.EnableCardEmulation();
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to enable card emulation mode, Error - " + (NfcError)ret);
                NfcErrorFactory.ThrowNfcException(ret);
            }
        }

        /// <summary>
        /// Disables the card emulation mode.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        /// <exception cref="NotSupportedException">Thrown when the NFC is not supported.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the method fails due to an invalid operation.</exception>
        public void DisableCardEmulation()
        {
            int ret = Interop.Nfc.CardEmulation.DisableCardEmulatiion();
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to disable card emulation mode, Error - " + (NfcError)ret);
                NfcErrorFactory.ThrowNfcException(ret);
            }
        }

        /// <summary>
        /// Gets the current card emulation mode.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <returns>Enumeration value for the NfcSecureElementCardEmulationMode.</returns>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        public NfcSecureElementCardEmulationMode GetCardEmulationMode()
        {
            int mode = 0;
            int ret = Interop.Nfc.CardEmulation.GetCardEmulationMode(out mode);
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to get card emulation mode, Error - " + (NfcError)ret);
            }

            return (NfcSecureElementCardEmulationMode)mode;
        }

        /// <summary>
        /// Gives the priority to the foreground application when dispatching the transaction event.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        /// <exception cref="NotSupportedException">Thrown when the NFC is not supported.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the method fails due to an invalid operation.</exception>
        public void EnableTransactionForegroundDispatch()
        {
            int ret = Interop.Nfc.EnableTransactionForegroundDispatch();
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to enable foreground dispatch, Error - " + (NfcError)ret);
                NfcErrorFactory.ThrowNfcException(ret);
            }
        }

        /// <summary>
        /// Disables the foreground dispatch for the "EVT_TRANSACTION" to the given application.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        /// <exception cref="NotSupportedException">Thrown when the NFC is not supported.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the method fails due to an invalid operation.</exception>
        public void DisableTransactionForegroundDispatch()
        {
            int ret = Interop.Nfc.DisableTransactionForegroundDispatch();
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to disable foreground dispatch, Error - " + (NfcError)ret);
                NfcErrorFactory.ThrowNfcException(ret);
            }
        }

        /// <summary>
        /// Gets the state, whether an application to call this API is currently the activated handler for the specific AID.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <returns>'True' when application is currently the activated handler, otherwise 'False'.</returns>
        /// <param name="seType">The type of the Secure Element.</param>
        /// <param name="aid">The application ID specified in the ISO/IEC 7816-4.</param>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        /// <exception cref="NotSupportedException">Thrown when the NFC is not supported.</exception>
        /// <exception cref="ArgumentException">Thrown when the method fails due to an invalid parameter.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the method fails due to an invalid operation.</exception>
        public bool IsActivatedHandlerForAid(NfcSecureElementType seType, string aid)
        {
            bool isActivatedHandle = false;
            int ret = Interop.Nfc.CardEmulation.IsActivatedHandlerForAid((int)seType, aid, out isActivatedHandle);
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to check activated handle for aid, Error - " + (NfcError)ret);
                NfcErrorFactory.ThrowNfcException(ret);
            }

            return isActivatedHandle;
        }

        /// <summary>
        /// Gets the state, whether an application to call this API is currently the activated handler for the category.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <returns>'True' when application is currently the activated handler, otherwise 'False'.</returns>
        /// <param name="seType">The type of the secure element.</param>
        /// <param name="category">Enumeration value of the category.</param>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        /// <exception cref="NotSupportedException">Thrown when the NFC is not supported.</exception>
        /// <exception cref="ArgumentException">Thrown when the method fails due to an invalid parameter.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the method fails due to an invalid operation.</exception>
        public bool IsActivatedHandlerForCategory(NfcSecureElementType seType, NfcCardEmulationCategoryType category)
        {
            bool isActivatedHandle = false;
            int ret = Interop.Nfc.CardEmulation.IsActivatedHandlerForCategory((int)seType, (int)category, out isActivatedHandle);
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to check activated handle for category, Error - " + (NfcError)ret);
                NfcErrorFactory.ThrowNfcException(ret);
            }

            return isActivatedHandle;
        }

        /// <summary>
        /// Registers the AID for a specific category.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="seType">The type of the secure element.</param>
        /// <param name="category">Enumeration value of the category.</param>
        /// <param name="aid">The application ID specified in the ISO/IEC 7816-4.</param>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        /// <exception cref="NotSupportedException">Thrown when the NFC is not supported.</exception>
        /// <exception cref="ArgumentException">Thrown when the method fails due to an invalid parameter.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the method fails due to an invalid operation.</exception>
        public void RegisterAid(NfcSecureElementType seType, NfcCardEmulationCategoryType category, string aid)
        {
            int ret = Interop.Nfc.CardEmulation.RegisterAid((int)seType, (int)category, aid);
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to register aid, Error - " + (NfcError)ret);
                NfcErrorFactory.ThrowNfcException(ret);
            }
        }

        /// <summary>
        /// Unregisters a previously registered AID for the specified category.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <param name="seType">The type of the secure element.</param>
        /// <param name="category">Enumeration value of the category.</param>
        /// <param name="aid">The application ID specified in the ISO/IEC 7816-4.</param>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        /// <exception cref="NotSupportedException">Thrown when the NFC is not supported.</exception>
        /// <exception cref="ArgumentException">Thrown when the method fails due to an invalid parameter.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the method fails due to an invalid operation.</exception>
        public void UnregisterAid(NfcSecureElementType seType, NfcCardEmulationCategoryType category, string aid)
        {
            int ret = Interop.Nfc.CardEmulation.UnregisterAid((int)seType, (int)category, aid);
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to unregister aid, Error - " + (NfcError)ret);
                NfcErrorFactory.ThrowNfcException(ret);
            }
        }

        /// <summary>
        /// Sets the application as a preferred handler.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        /// <exception cref="NotSupportedException">Thrown when the NFC is not supported.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the method fails due to an invalid operation.</exception>
        public void SetPreferredApplication()
        {
            int ret = Interop.Nfc.CardEmulation.SetPreferredHandler();
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to set preferred handler, Error - " + (NfcError)ret);
                NfcErrorFactory.ThrowNfcException(ret);
            }
        }

        /// <summary>
        /// Unsets the application as a preferred handler.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        /// <exception cref="NotSupportedException">Thrown when the NFC is not supported.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the method fails due to an invalid operation.</exception>
        public void UnsetPreferredApplication()
        {
            int ret = Interop.Nfc.CardEmulation.UnsetPreferredHandler();
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to unset preferred handler, Error - " + (NfcError)ret);
                NfcErrorFactory.ThrowNfcException(ret);
            }
        }

        /// <summary>
        /// Retrieves all registered AIDs.
        /// </summary>
        /// <since_tizen> 3 </since_tizen>
        /// <returns>The list of NfcRegisteredAidInformation objects.</returns>
        /// <param name="seType">The type of the secure element.</param>
        /// <param name="category">Enumeration value of the category.</param>
        /// <privilege>http://tizen.org/privilege/nfc.cardemulation</privilege>
        /// <exception cref="NotSupportedException">Thrown when the NFC is not supported.</exception>
        /// <exception cref="ArgumentException">Thrown when the method fails due to an invalid parameter.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the method fails due to an invalid operation.</exception>
        public IEnumerable<NfcRegisteredAidInformation> GetRegisteredAidInformation(NfcSecureElementType seType, NfcCardEmulationCategoryType category)
        {
            List<NfcRegisteredAidInformation> infoList = new List<NfcRegisteredAidInformation>();
            Interop.Nfc.SecureElementRegisteredAidCallback callback = (int type, IntPtr aid, bool readOnly, IntPtr userData) =>
            {
                if (aid != IntPtr.Zero)
                {
                    NfcRegisteredAidInformation aidInfo = new NfcRegisteredAidInformation((NfcSecureElementType)type, Marshal.PtrToStringAnsi(aid), readOnly);

                    infoList.Add(aidInfo);
                }
            };

            int ret = Interop.Nfc.CardEmulation.ForeachRegisterdAids((int)seType, (int)category, callback, IntPtr.Zero);
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to get all registerd aid informations, Error - " + (NfcError)ret);
                NfcErrorFactory.ThrowNfcException(ret);
            }

            return infoList;
        }

        private void RegisterSecureElementEvent()
        {
            _secureElementEventCallback = (int eventType, IntPtr userData) =>
            {
                NfcSecureElementEvent _eventType = (NfcSecureElementEvent)eventType;
                SecureElementEventArgs e = new SecureElementEventArgs(_eventType);
                _secureElementEvent.SafeInvoke(null, e);
            };

            int ret = Interop.Nfc.SetSecureElementEventCallback(_secureElementEventCallback, IntPtr.Zero);
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to set secure element event callback, Error - " + (NfcError)ret);
            }
        }

        private void UnregisterSecureElementEvent()
        {
            Interop.Nfc.UnsetSecureElementEventCallback();
        }

        private void RegisterSecureElementTransactionEvent(NfcSecureElementType seType)
        {
            _secureElementTransactionEventCallback = (int type, IntPtr aid, int aidSize, IntPtr param, int paramSize, IntPtr userData) =>
            {
                NfcSecureElementType _secureElementType = (NfcSecureElementType)type;
                byte[] _aid = NfcConvertUtil.IntLengthIntPtrToByteArray(aid, aidSize);
                byte[] _param = NfcConvertUtil.IntLengthIntPtrToByteArray(param, paramSize);
                SecureElementTranscationEventArgs e = new SecureElementTranscationEventArgs(_secureElementType, _aid, _param);
                _secureElementTransactionEvent.SafeInvoke(null, e);
            };

            int ret = Interop.Nfc.SetSecureElementTransactionEventCallback((int)seType, _secureElementTransactionEventCallback, IntPtr.Zero);
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to set secure element transaction event callback, Error - " + (NfcError)ret);
            }
        }

        private void UnregisterSecureElementTransactionEvent(NfcSecureElementType seType)
        {
            Interop.Nfc.UnsetSecureElementTransactionEventCallback((int)seType);
        }

        private void RegisterHostCardEmulationEvent()
        {
            _hostCardEmulationEventCallback = (IntPtr handle, int eventType, IntPtr apdu, uint apduLen, IntPtr userData) =>
            {
                IntPtr _seHandle = handle;
                NfcHceEvent _hcdEventType = (NfcHceEvent)eventType;
                byte[] _apdu = NfcConvertUtil.UintLengthIntPtrToByteArray(apdu, apduLen);
                HostCardEmulationEventArgs e = new HostCardEmulationEventArgs(_seHandle, _hcdEventType, _apdu);
                _hostCardEmulationEvent.SafeInvoke(null, e);
            };

            int ret = Interop.Nfc.SetHostCardEmulationEventCallback(_hostCardEmulationEventCallback, IntPtr.Zero);
            if (ret != (int)NfcError.None)
            {
                Log.Error(Globals.LogTag, "Failed to set host card emulation event callback, Error - " + (NfcError)ret);
                NfcErrorFactory.ThrowNfcException(ret);
            }
        }

        private void UnregisterHostCardEmulationEvent()
        {
            Interop.Nfc.UnsetHostCardEmulationEventCallback();
        }
    }
}
