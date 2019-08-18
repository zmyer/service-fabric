// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace System.Fabric.Query
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Fabric.Common;
    using System.Fabric.Interop;

    /// <summary>
    /// <para>Represents a list of <see cref="System.Fabric.Query.DeployedServiceType" />.</para>
    /// </summary>
    public sealed class DeployedServiceTypeList : IList<DeployedServiceType>
    {
        IList<DeployedServiceType> list;

        internal DeployedServiceTypeList()
            : this(new List<DeployedServiceType>())
        {
        }

        internal DeployedServiceTypeList(IList<DeployedServiceType> list)
        {
            this.list = list;
        }

        /// <summary>
        /// <para>Gets the index of the specified item in this list.</para>
        /// </summary>
        /// <param name="item">
        /// <para>The item in the list.</para>
        /// </param>
        /// <returns>
        /// <para>The index of the specified item in this list.</para>
        /// </returns>
        public int IndexOf(DeployedServiceType item)
        {
            return this.list.IndexOf(item);
        }

        /// <summary>
        /// <para>Inserts the item at the specified index.</para>
        /// </summary>
        /// <param name="index">
        /// <para>The index where the item will be inserted.</para>
        /// </param>
        /// <param name="item">
        /// <para>The item to insert.</para>
        /// </param>
        public void Insert(int index, DeployedServiceType item)
        {
            this.list.Insert(index, item);
        }

        /// <summary>
        /// <para>Removes the item at the specified index.</para>
        /// </summary>
        /// <param name="index">
        /// <para>The index of the item to remove.</para>
        /// </param>
        public void RemoveAt(int index)
        {
            this.list.RemoveAt(index);
        }

        /// <summary>
        /// <para>Gets the item at the specified index.</para>
        /// </summary>
        /// <param name="index">
        /// <para>The index of the item.</para>
        /// </param>
        /// <returns>
        /// <para>The item at the specified index.</para>
        /// </returns>
        public DeployedServiceType this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                this.list[index] = value;
            }
        }

        /// <summary>
        /// <para>Adds the specified item to the list.</para>
        /// </summary>
        /// <param name="item">
        /// <para>The item to add.</para>
        /// </param>
        public void Add(DeployedServiceType item)
        {
            this.list.Add(item);
        }

        /// <summary>
        /// <para>Removes all items from the list.</para>
        /// </summary>
        public void Clear()
        {
            this.list.Clear();
        }

        /// <summary>
        /// <para>Indicates a flag that specifies whether the list contains a specific item.</para>
        /// </summary>
        /// <param name="item">
        /// <para>The item to search.</para>
        /// </param>
        /// <returns>
        /// <para>
        ///     <languageKeyword>true</languageKeyword> if the list contains a specific item; otherwise, <languageKeyword>false</languageKeyword>.</para>
        /// </returns>
        public bool Contains(DeployedServiceType item)
        {
            return this.list.Contains(item);
        }

        /// <summary>
        /// <para>Copies items from the list to the specified array at the specified starting index.</para>
        /// </summary>
        /// <param name="array">
        /// <para>The array.</para>
        /// </param>
        /// <param name="arrayIndex">
        /// <para>The starting index of the array.</para>
        /// </param>
        public void CopyTo(DeployedServiceType[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// <para>Gets or sets the number of items in this list.</para>
        /// </summary>
        /// <value>
        /// <para>The number of items in this list.</para>
        /// </value>
        public int Count
        {
            get { return this.list.Count; }
        }

        /// <summary>
        /// <para>Gets or sets a flag that indicates whether the list can be modified.</para>
        /// </summary>
        /// <value>
        /// <para>
        ///     <languageKeyword>true</languageKeyword> if the list can be modified; otherwise, <languageKeyword>false</languageKeyword>.</para>
        /// </value>
        public bool IsReadOnly
        {
            get { return this.list.IsReadOnly; }
        }

        /// <summary>
        /// <para>Remove the specified item from this list.</para>
        /// </summary>
        /// <param name="item">
        /// <para>The item to remove.</para>
        /// </param>
        /// <returns>
        /// <para>The list with removed item.</para>
        /// </returns>
        public bool Remove(DeployedServiceType item)
        {
            return this.list.Remove(item);
        }

        /// <summary>
        /// <para>Gets an enumerator to items in this list.</para>
        /// </summary>
        /// <returns>
        /// <para>The enumerator to items in this list.</para>
        /// </returns>
        public IEnumerator<DeployedServiceType> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        /// <summary>
        /// <para>Gets an enumerator for items in this list.</para>
        /// </summary>
        /// <returns>
        /// <para>The enumerator for items in this list.</para>
        /// </returns>
        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        [SuppressMessage(FxCop.Category.Reliability, FxCop.Rule.RemoveCallsToGCKeepAlive, Justification = FxCop.Justification.EnsureNativeBuffersLifetime)]
        internal static unsafe DeployedServiceTypeList CreateFromNativeListResult(
            NativeClient.IFabricGetDeployedServiceTypeListResult result)
        {
            var retval = CreateFromNativeList((NativeTypes.FABRIC_DEPLOYED_SERVICE_TYPE_QUERY_RESULT_LIST*)result.get_DeployedServiceTypeList());
            GC.KeepAlive(result);

            return retval;
        }

        internal static unsafe DeployedServiceTypeList CreateFromNativeList(
            NativeTypes.FABRIC_DEPLOYED_SERVICE_TYPE_QUERY_RESULT_LIST* nativeList)
        {
            var retval = new DeployedServiceTypeList();

            var nativeItemArray = (NativeTypes.FABRIC_DEPLOYED_SERVICE_TYPE_QUERY_RESULT_ITEM*)nativeList->Items;
            for (int i = 0; i < nativeList->Count; ++i)
            {
                var nativeItem = *(nativeItemArray + i);
                retval.Add(DeployedServiceType.CreateFromNative(nativeItem));
            }

            return retval;
        }
    }
}