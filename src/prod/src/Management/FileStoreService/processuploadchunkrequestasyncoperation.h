// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

#pragma once

namespace Management
{
    namespace FileStoreService
    {
        class RequestManager;
        class ProcessUploadChunkRequestAsyncOperation 
            : public ProcessRequestAsyncOperation
        {
            DENY_COPY(ProcessUploadChunkRequestAsyncOperation)

        public:
            ProcessUploadChunkRequestAsyncOperation(
                __in RequestManager & requestManager,
                UploadChunkRequest && uploadChunkMessage,
                Transport::IpcReceiverContextUPtr && receiverContext,
                Common::ActivityId const & activityId,
                Common::TimeSpan const & timeout,
                Common::AsyncCallback const &,
                Common::AsyncOperationSPtr const &);

            virtual ~ProcessUploadChunkRequestAsyncOperation();

            virtual void WriteTrace(__in Common::ErrorCode const &error) override;

            __declspec(property(get = get_StagingFullPath)) std::wstring const & StagingFullPath;
            std::wstring const & get_StagingFullPath() const { return stagingFullPath_; }

            __declspec(property(get = get_StartPosition)) uint64 StartPosition;
            uint64 const get_StartPosition() const { return startPosition_; }

            __declspec(property(get = get_EndPosition)) uint64 EndPosition;
            uint64 const get_EndPosition() const { return endPosition_; }

            __declspec(property(get = get_SessionId)) Common::Guid const & SessionId;
            Common::Guid const & get_SessionId() const { return sessionId_; }

        protected:

            Common::AsyncOperationSPtr BeginOperation(
                Common::AsyncCallback const & callback,
                Common::AsyncOperationSPtr const & parent);

            Common::ErrorCode EndOperation(
                __out Transport::MessageUPtr & reply,
                Common::AsyncOperationSPtr const & asyncOperation);

            Common::ErrorCode ValidateRequest();

            void OnRequestCompleted(__inout Common::ErrorCode & error) override;
        private:

            class UploadChunkAsyncOperation;
            Common::Guid sessionId_;
            std::wstring stagingFullPath_;
            uint64 startPosition_;
            uint64 endPosition_;
        };
    }
}
