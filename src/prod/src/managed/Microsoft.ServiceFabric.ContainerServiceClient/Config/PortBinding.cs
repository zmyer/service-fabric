// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.ServiceFabric.ContainerServiceClient.Config
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    internal class PortBinding
    {
        [DataMember(Name = "HostIp", EmitDefaultValue = false)]
        public string HostIP { get; set; }

        [DataMember(Name = "HostPort", EmitDefaultValue = false)]
        public string HostPort { get; set; }
    }
}