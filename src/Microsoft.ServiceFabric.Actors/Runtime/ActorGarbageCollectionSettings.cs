﻿// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------
namespace Microsoft.ServiceFabric.Actors.Runtime
{
    using System;

    /// <summary>
    /// Settings to configures Garbage Collection behavior of Actor Service.
    /// </summary>
    public sealed class ActorGarbageCollectionSettings
    {
        private long scanIntervalInSeconds = 60;
        private long idleTimeoutInSeconds = 3600;

        /// <summary>
        /// Initializes a new instance of the ActorGarbageCollectionSettings class.
        /// </summary>
        public ActorGarbageCollectionSettings()
        {
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="settings"></param>
        internal ActorGarbageCollectionSettings(ActorGarbageCollectionSettings settings)
        {
            this.idleTimeoutInSeconds = settings.IdleTimeoutInSeconds;
            this.scanIntervalInSeconds = settings.ScanIntervalInSeconds;
        }

        /// <summary>
        /// Initializes a new instance of the ActorGarbageCollectionSettings class.
        /// </summary>
        /// <param name="idleTimeoutInSeconds">Time interval to wait before garbage collecting an actor which is not in use.</param>
        /// <param name="scanIntervalInSeconds">Time interval to run Actor Garbage Collection scan.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// <para>When idleTimeoutInSeconds is less than or equal to 0.</para>
        /// <para>When scanIntervalInSeconds is less than or equal to 0.</para>
        /// <para>When idleTimeoutInSeconds is less than scanIntervalInSeconds.</para>
        /// </exception>
        public ActorGarbageCollectionSettings(long idleTimeoutInSeconds, long scanIntervalInSeconds)
        {
            // Verify that values are within acceptable range.
            if (idleTimeoutInSeconds <= 0)
            {
                throw new ArgumentOutOfRangeException("idleTimeoutInSeconds)", SR.ActorGCSettingsValueOutOfRange);
            }

            if (scanIntervalInSeconds <= 0)
            {
                throw new ArgumentOutOfRangeException("scanIntervalInSeconds)", SR.ActorGCSettingsValueOutOfRange);
            }

            if (idleTimeoutInSeconds / scanIntervalInSeconds >= 1)
            {
                this.scanIntervalInSeconds = scanIntervalInSeconds;
                this.idleTimeoutInSeconds = idleTimeoutInSeconds;
            }
            else
            {
                throw new ArgumentOutOfRangeException(SR.ActorGCSettingsNotValid);
            }
        }

        /// <summary>
        /// Gets time interval to run Actor Garbage Collection scan.
        /// </summary>
        /// <value>Time interval in <see cref="System.Int64"/> to run Actor Garbage Collection scan.</value>
        public long ScanIntervalInSeconds
        {
            get { return this.scanIntervalInSeconds; }
        }

        /// <summary>
        /// Gets time interval to wait before garbage collecting an actor which is not in use.
        /// </summary>
        /// <value>Time interval in <see cref="System.Int64"/> to wait before garbage collecting an actor which is not in use.</value>
        public long IdleTimeoutInSeconds
        {
            get { return this.idleTimeoutInSeconds; }
        }
    }
}
