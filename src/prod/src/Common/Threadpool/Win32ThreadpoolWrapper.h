// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

#include "MinPal.h"
#include "Threadpool.h"

using namespace Threadpool;

// TP_POOL Related
typedef struct _TP_POOL
{
    ThreadpoolMgr *pThreadpoolMgr;
} TP_POOL, *PTP_POOL;


// TP_WORK Related
typedef struct _TP_CALLBACK_ENVIRON_V3  *PTP_CALLBACK_ENVIRON;
typedef struct _TP_CALLBACK_INSTANCE *PTP_CALLBACK_INSTANCE;
typedef struct _TP_WORK *PTP_WORK;

typedef VOID (*PTP_WORK_CALLBACK)(
        PTP_CALLBACK_INSTANCE Instance,
        PVOID                 Context,
        PTP_WORK              Work
);

typedef struct _TP_WORK
{
    PVOID CallbackParameter;
    PTP_WORK_CALLBACK WorkCallback;
    PTP_CALLBACK_ENVIRON CallbackEnvironment;
} TP_WORK, *PTP_WORK;

// TP_CALLBACK_ENVIRON Related
typedef DWORD TP_VERSION, *PTP_VERSION;

typedef struct _TP_CALLBACK_INSTANCE
{
    PTP_WORK Work;
} TP_CALLBACK_INSTANCE, *PTP_CALLBACK_INSTANCE;

typedef VOID (*PTP_SIMPLE_CALLBACK)(
        PTP_CALLBACK_INSTANCE Instance,
        PVOID                 Context
);

typedef enum _TP_CALLBACK_PRIORITY {
    TP_CALLBACK_PRIORITY_HIGH,
    TP_CALLBACK_PRIORITY_NORMAL,
    TP_CALLBACK_PRIORITY_LOW,
    TP_CALLBACK_PRIORITY_INVALID,
    TP_CALLBACK_PRIORITY_COUNT = TP_CALLBACK_PRIORITY_INVALID
} TP_CALLBACK_PRIORITY;

typedef struct _TP_CLEANUP_GROUP {
    // do not care
} TP_CLEANUP_GROUP, *PTP_CLEANUP_GROUP;

typedef struct _ACTIVATION_CONTEXT {
    // do not care
} ACTIVATION_CONTEXT;

typedef VOID (*PTP_CLEANUP_GROUP_CANCEL_CALLBACK)(
        PVOID ObjectContext,
        PVOID CleanupContext
);

typedef struct _TP_CALLBACK_ENVIRON_V3 {
    TP_VERSION                         Version;
    PTP_POOL                           Pool;
    PTP_CLEANUP_GROUP                  CleanupGroup;
    PTP_CLEANUP_GROUP_CANCEL_CALLBACK  CleanupGroupCancelCallback;
    PVOID                              RaceDll;
    struct _ACTIVATION_CONTEXT        *ActivationContext;
    PTP_SIMPLE_CALLBACK                FinalizationCallback;
    union {
        DWORD                          Flags;
        struct {
            DWORD                      LongFunction :  1;
            DWORD                      Persistent   :  1;
            DWORD                      Private      : 30;
        } s;
    } u;
    TP_CALLBACK_PRIORITY               CallbackPriority;
    DWORD                              Size;
} TP_CALLBACK_ENVIRON_V3;

typedef TP_CALLBACK_ENVIRON_V3 TP_CALLBACK_ENVIRON, *PTP_CALLBACK_ENVIRON;

VOID InitializeThreadpoolEnvironment(PTP_CALLBACK_ENVIRON pcbe);

VOID DestroyThreadpoolEnvironment(PTP_CALLBACK_ENVIRON pcbe);

VOID SetThreadpoolCallbackPool(PTP_CALLBACK_ENVIRON pcbe, PTP_POOL ptpp);

extern "C" PTP_POOL CreateThreadpool(PVOID reserved);

extern "C" VOID CloseThreadpool(PTP_POOL ptpp);

extern "C" BOOL SetThreadpoolThreadMinimum(PTP_POOL ptpp, DWORD cthrdMic);

extern "C" VOID SetThreadpoolThreadMaximum(PTP_POOL ptpp, DWORD cthrdMost);

extern "C" PTP_WORK CreateThreadpoolWork(PTP_WORK_CALLBACK pfnwk, PVOID pv, PTP_CALLBACK_ENVIRON pcbe);

extern "C" VOID CloseThreadpoolWork(PTP_WORK pwk);

extern "C" VOID SubmitThreadpoolWork(PTP_WORK pwk);

extern "C" VOID WaitForThreadpoolWorkCallbacks(PTP_WORK pwk, BOOL fCancelPendingCallbacks);
