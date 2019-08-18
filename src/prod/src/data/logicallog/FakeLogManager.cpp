// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

#include "stdafx.h"
#include "TestHeaders.h"

using namespace Data::Log;

FakeLogManager::FakeLogManager() {}
FakeLogManager::~FakeLogManager() {}

KDefineDefaultCreate(FakeLogManager, fakeLogManager);
