// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace FabricDCA 
{
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;
    using System.Security;
    using System.Fabric.Dca.Validator;

    // This class implements validation for file share upload settings in the 
    // cluster manifest for upload of files from a specific folder
    public class FileShareFolderUploaderValidator : ClusterManifestFileShareSettingsValidator, IDcaSettingsValidator
    {
        protected override string isEnabledParamName
        {
            get
            {
                return FileShareUploaderConstants.EnabledParamName;
            }
        }

        public FileShareFolderUploaderValidator()
        {
            this.settingHandlers = new Dictionary<string, Handler>()
                                   {
                                       {
                                           FileShareUploaderConstants.StoreConnectionStringParamName,
                                           new Handler()
                                           {
                                               Validator = this.DestinationPathHandler
                                           }
                                       },
                                       {
                                           FileShareUploaderConstants.UploadIntervalParamName,
                                           new Handler()
                                           {
                                               Validator = this.ValidateUploadIntervalSetting
                                           }
                                       },
                                       {
                                           FileShareUploaderConstants.FileSyncIntervalParamName,
                                           new Handler()
                                           {
                                               Validator = this.ValidateFileSyncIntervalSetting
                                           }
                                       },
                                       {
                                           FileShareUploaderConstants.DataDeletionAgeParamName,
                                           new Handler()
                                           {
                                               Validator = this.ValidateLogDeletionAgeSetting
                                           }
                                       },
                                       {
                                           FileShareUploaderConstants.TestDataDeletionAgeParamName,
                                           new Handler()
                                           {
                                               Validator = this.ValidateLogDeletionAgeTestSetting
                                           }
                                       },
                                       {
                                           FileShareUploaderConstants.StoreAccessAccountType,
                                           new Handler()
                                           {
                                               Validator = this.ValidateAccountType
                                           }
                                       },
                                       {
                                           FileShareUploaderConstants.StoreAccessAccountName,
                                           new Handler()
                                           {
                                               Validator = this.ValidateAccountName
                                           }
                                       },
                                       {
                                           FileShareUploaderConstants.StoreAccessAccountPassword,
                                           new Handler()
                                           {
                                               Validator = this.AccountPasswordHandler
                                           }
                                       },
                                   };
            this.encryptedSettingHandlers = new Dictionary<string, Handler>()
                                            {
                                               {
                                                    FileShareUploaderConstants.StoreAccessAccountPassword,
                                                    new Handler()
                                                    {
                                                        Validator = this.AccountPasswordHandler
                                                    }
                                                },
                                            };
            return;
        }
    }
}