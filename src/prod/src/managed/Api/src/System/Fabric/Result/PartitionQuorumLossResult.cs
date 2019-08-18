// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace System.Fabric.Result
{
    using System.Fabric.Common;
    using System.Fabric.Interop;
    using System.Text;

    /// <summary>
    /// Returns Invoke quorum loss result object.
    /// </summary>
    /// <remarks>
    /// This class returns SelectedPartition information for which Invoke Quorum Loss action was called. 
    /// </remarks>
    [Serializable]
    public class PartitionQuorumLossResult : TestCommandResult
    {
        internal PartitionQuorumLossResult(SelectedPartition selectedPartition, Exception exception)
        {
            ReleaseAssert.AssertIf(selectedPartition == null, "Selected partition cannot be null");
            this.SelectedPartition = selectedPartition;
            this.Exception = exception;
        }

        internal PartitionQuorumLossResult(SelectedPartition selectedPartition)
            : this(selectedPartition, null)
        {
        }

        internal PartitionQuorumLossResult(SelectedPartition selectedPartition, int errorCode)
        {
            this.SelectedPartition = selectedPartition;
            this.ErrorCode = errorCode;
        }

        internal PartitionQuorumLossResult() { }

        /// <summary>
        /// Gets selecetd partition.
        /// </summary>
        /// <value>The SelectedPartition object.</value>
        public SelectedPartition SelectedPartition { get; private set; }

        internal int ErrorCode { get; set; }

        /// <summary>
        /// Returns the string representation of the contained information.
        /// </summary>
        /// <returns>A string containing information about the SelectedPartition 
        /// and (conditionally) about the Exception </returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("SelectedPartition: {0}", this.SelectedPartition);

            if (this.Exception != null)
            {
                sb.AppendFormat(", Exception : {0}", this.Exception);
            }

            return sb.ToString();
        }

        internal unsafe void CreateFromNative(IntPtr pointer)
        {
            NativeTypes.FABRIC_PARTITION_QUORUM_LOSS_RESULT nativeResult = *(NativeTypes.FABRIC_PARTITION_QUORUM_LOSS_RESULT*)pointer;
            this.SelectedPartition = SelectedPartition.CreateFromNative(nativeResult.SelectedPartition);
            this.ErrorCode = nativeResult.ErrorCode;
            try
            {
                this.Exception = InteropHelpers.TranslateError(this.ErrorCode);
            }
            catch (FabricException)
            {
                // Exception could not be translated.  To the user, it will look like whole FabricClient API itself failed, 
                // instead of just the translation above, and they won't be able to determine what happened.  So 
                // instead, suppress this.  The user will still have this.ErrorCode.  We will still  
                // be able to catch and fix situations that hit this case in test automation because 
                // there be a check on this.Exception there.
            }
        }

        internal unsafe IntPtr ToNative(PinCollection pinCollection)
        {
            var nativeResult = new NativeTypes.FABRIC_PARTITION_QUORUM_LOSS_RESULT();
            if (this.SelectedPartition != null)
            {
                nativeResult.SelectedPartition = this.SelectedPartition.ToNative(pinCollection);
            }

            nativeResult.ErrorCode = this.ErrorCode;

            return pinCollection.AddBlittable(nativeResult);
        }
    }
}