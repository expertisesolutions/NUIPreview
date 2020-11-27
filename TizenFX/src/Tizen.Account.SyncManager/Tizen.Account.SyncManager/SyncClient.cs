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
using System.Collections.Generic;
using Tizen.Applications;
using Tizen.Account.AccountManager;

namespace Tizen.Account.SyncManager
{
    /// <summary>
    /// The SyncClient APIs for managing the sync operations. Applications will call these APIs to schedule their sync operations.
    /// The sync service maintains sync requests from all the applications and invokes their respective callback methods to perform account synchronization operations.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public static class SyncClient
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        static SyncClient()
        {
        }

        /// <summary>
        /// Requests the sync manager to perform one time sync operation.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <param name="request"> The sync job information of the sync job request. </param>
        /// <param name="syncOptions"> Sync options determine a way to operate the sync job and can be used as ORing. </param>
        /// <exception cref="ArgumentNullException"> Thrown when any of the arugments are null. </exception>
        /// <exception cref="InvalidOperationException"> Thrown when the application calling this API doesn't have a sync adapter. </exception>
        /// <returns> An unique value which can manage sync jobs. The number of sync job ID is limite as it is less than hundred. </returns>
        public static int RequestOnDemandSyncJob(SyncJobData request, SyncOption syncOptions)
        {
            if (request == null || request.SyncJobName == null)
            {
                throw new ArgumentNullException();
            }

            SafeAccountHandle accountHandle = (request.Account != null) ? request.Account.SafeAccountHandle : new SafeAccountHandle();
            SafeBundleHandle bundleHandle = (request.UserData != null) ? request.UserData.SafeBundleHandle : new SafeBundleHandle();

            int id = 0;
            int ret = Interop.Manager.RequestOnDemandSyncJob(accountHandle, request.SyncJobName, (int)syncOptions, bundleHandle, out id);
            if (ret != (int)SyncManagerErrorCode.None)
            {
                Log.Error(ErrorFactory.LogTag, "Failed to request on demand sync job");
                throw ErrorFactory.GetException(ret);
            }
            return id;
        }

        /// <summary>
        /// Requests the sync manager to perform periodic sync operations.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <param name="request"> The sync job information of the sync job request. </param>
        /// <param name="period"> Determines the time interval of the periodic sync. The periodic sync operation can be triggered in that interval, but it does not guarantee the exact time. The minimum value is 30 minutes. </param>
        /// <param name="syncOptions"> Sync options determine a way to operate the sync job and can be used as ORing. </param>
        /// <privilege>http://tizen.org/privilege/alarm.set</privilege>
        /// <exception cref="UnauthorizedAccessException"> In case of a privilege not defined. </exception>
        /// <exception cref="ArgumentNullException"> Thrown when any of the arguments are null. </exception>
        /// <exception cref="InvalidOperationException"> Thrown when the application calling this API doesn't have a sync adapter. </exception>
        /// <returns> A unique value which can manage sync jobs. The number of sync job IDs is limited as it is less than hundred. </returns>
        public static int AddPeriodicSyncJob(SyncJobData request, SyncPeriod period, SyncOption syncOptions)
        {
            if (request == null || request.SyncJobName == null)
            {
                throw new ArgumentNullException();
            }

            SafeAccountHandle accountHandle = (request.Account != null) ? request.Account.SafeAccountHandle : new SafeAccountHandle();
            SafeBundleHandle bundleHandle = (request.UserData != null) ? request.UserData.SafeBundleHandle : new SafeBundleHandle();

            int id = 0;
            int ret = Interop.Manager.AddPeriodicSyncJob(accountHandle, request.SyncJobName, (int) period, (int)syncOptions, bundleHandle, out id);
            if (ret != (int)SyncManagerErrorCode.None)
            {
                Log.Error(ErrorFactory.LogTag, "Failed to add periodic sync job");
                throw ErrorFactory.GetException(ret);
            }
            return id;
        }

        /// <summary>
        /// Requests the sync manager to perform sync operations whenever the corresponding DB is changed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <param name="request"> The sync job information of the sync job request. </param>
        /// <param name="syncOptions"> Sync options determine a way to operate the sync job and can be used as ORing. </param>
        /// <privilege>http://tizen.org/privilege/calendar.read</privilege>
        /// <privilege>http://tizen.org/privilege/contact.read</privilege>
        /// <exception cref="UnauthorizedAccessException"> In case of a privilege is not defined. </exception>
        /// <exception cref="ArgumentNullException"> Thrown when any of the arguments are null. </exception>
        /// <exception cref="InvalidOperationException"> Thrown when the application calling this API doesn't have a sync adapter. </exception>
        /// <returns> A unique value which can manage sync jobs. The number of sync job IDs is limited as it is less than hundred. </returns>
        public static int AddDataChangeSyncJob(SyncJobData request, SyncOption syncOptions)
        {
            if (request == null || request.SyncJobName == null)
            {
                throw new ArgumentNullException();
            }

            SafeAccountHandle accountHandle = (request.Account != null) ? request.Account.SafeAccountHandle : new SafeAccountHandle();
            SafeBundleHandle bundleHandle = (request.UserData != null) ? request.UserData.SafeBundleHandle : new SafeBundleHandle();

            int id = 0;
            int ret = Interop.Manager.AddDataChangeSyncJob(accountHandle, request.SyncJobName, (int)syncOptions, bundleHandle, out id);
            if (ret != (int)SyncManagerErrorCode.None)
            {
                Log.Error(ErrorFactory.LogTag, "Failed to add data change sync job");
                throw ErrorFactory.GetException(ret);
            }
            return id;
        }

        /// <summary>
        /// Gets all the sync jobs registered with the sync manager.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <returns>
        /// Returns the list of SyncJobData corresponding to sync requests.
        /// </returns>
        public static IEnumerable<KeyValuePair<int, SyncJobData>> GetAllSyncJobs()
        {
            IDictionary<int, SyncJobData> syncJobs = new Dictionary<int, SyncJobData>();
            Interop.Manager.SyncManagerSyncJobCallback cb = (IntPtr accountHandle, string syncJobName, string syncCapability, int syncJobId, IntPtr syncJobUserData, IntPtr userData) =>
            {
                AccountManager.Account account = new AccountManager.Account(new SafeAccountHandle(accountHandle, true));
                Bundle bundle = new Bundle(new SafeBundleHandle(syncJobUserData, true));

                SyncJobData syncJobData = new SyncJobData();
                syncJobData.Account = account;
                if (syncJobName != null)
                    syncJobData.SyncJobName = syncJobName;
                else
                    syncJobData.SyncJobName = syncCapability;
                syncJobData.UserData = bundle;

                syncJobs.Add(syncJobId, syncJobData);
                return true;
            };

            int ret = Interop.Manager.ForeachSyncJob(cb, IntPtr.Zero);
            if (ret != (int)SyncManagerErrorCode.None)
            {
                Log.Error(ErrorFactory.LogTag, "Failed to get registered sync job");
                throw ErrorFactory.GetException(ret);
            }
            return syncJobs;
        }

        /// <summary>
        /// Requests the sync manager to remove the corresponding sync job based on the ID.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        /// <param name="id"> A unique value of each sync job, it can be used to search a specific sync job and remove it. </param>
        /// <exception cref="ArgumentException"> Thrown if the input arugments is invalid. </exception>
        public static void RemoveSyncJob(int id)
        {
            int ret = Interop.Manager.RemoveSyncJob(id);
            if (ret != (int)SyncManagerErrorCode.None)
            {
                Log.Error(ErrorFactory.LogTag, "Failed to remove sync job");
                throw ErrorFactory.GetException(ret);
            }
        }
    }
}

