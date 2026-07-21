using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace MemoryCleaner
{
    public class Win32_PrivilegeElevation
    {
        private const int SE_PRIVILEGE_ENABLED = 2;

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string? host, string name, ref long pluid);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(
            IntPtr htok,
            bool disall,
            ref TokPriv1Luid newst,
            int len,
            IntPtr prev,
            IntPtr relen);

        private static bool SetIncreasePrivilege(string privilegeName)
        {
            using var identity = WindowsIdentity.GetCurrent(
                TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges);

            var tokenPrivilege = new TokPriv1Luid
            {
                Count = 1,
                Luid = 0L,
                Attr = SE_PRIVILEGE_ENABLED
            };

            if (!LookupPrivilegeValue(null, privilegeName, ref tokenPrivilege.Luid))
                throw new Exception("Error in LookupPrivilegeValue: ", new Win32Exception(Marshal.GetLastWin32Error()));

            if (!AdjustTokenPrivileges(identity.Token, false, ref tokenPrivilege, 0, IntPtr.Zero, IntPtr.Zero))
                throw new Exception("Error in AdjustTokenPrivileges: ", new Win32Exception(Marshal.GetLastWin32Error()));

            return true;
        }

        public static bool SetIncreasePrivileges(params string[] privilegeNames)
        {
            foreach (string privilegeName in privilegeNames)
                SetIncreasePrivilege(privilegeName);

            return true;
        }

        public static class SE
        {
            public const string ASSIGNPRIMARYTOKEN_NAME = "SeAssignPrimaryTokenPrivilege";
            public const string AUDIT_NAME = "SeAuditPrivilege";
            public const string BACKUP_NAME = "SeBackupPrivilege";
            public const string CHANGE_NOTIFY_NAME = "SeChangeNotifyPrivilege";
            public const string CREATE_GLOBAL_NAME = "SeCreateGlobalPrivilege";
            public const string CREATE_PAGEFILE_NAME = "SeCreatePagefilePrivilege";
            public const string CREATE_PERMANENT_NAME = "SeCreatePermanentPrivilege";
            public const string CREATE_TOKEN_NAME = "SeCreateTokenPrivilege";
            public const string DEBUG_NAME = "SeDebugPrivilege";
            public const string ENABLE_DELEGATION_NAME = "SeEnableDelegationPrivilege";
            public const string IMPERSONATE_NAME = "SeImpersonatePrivilege";
            public const string INCREAQUOTA_NAME = "SeIncreaseQuotaPrivilege";
            public const string INC_BAPRIORITY_NAME = "SeIncreaseBasePriorityPrivilege";
            public const string LOAD_DRIVER_NAME = "SeLoadDriverPrivilege";
            public const string LOCK_MEMORY_NAME = "SeLockMemoryPrivilege";
            public const string MACHINE_ACCOUNT_NAME = "SeMachineAccountPrivilege";
            public const string MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";
            public const string PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";
            public const string REMOTE_SHUTDOWN_NAME = "SeRemoteShutdownPrivilege";
            public const string RESTORE_NAME = "SeRestorePrivilege";
            public const string SECURITY_NAME = "SeSecurityPrivilege";
            public const string SHUTDOWN_NAME = "SeShutdownPrivilege";
            public const string SYSTEMTIME_NAME = "SeSystemtimePrivilege";
            public const string SYSTEM_ENVIRONMENT_NAME = "SeSystemEnvironmentPrivilege";
            public const string SYSTEM_PROFILE_NAME = "SeSystemProfilePrivilege";
            public const string TAKE_OWNERSHIP_NAME = "SeTakeOwnershipPrivilege";
            public const string TCB_NAME = "SeTcbPrivilege";
            public const string UNDOCK_NAME = "SeUndockPrivilege";
            public const string UNSOLICITED_INPUT_NAME = "SeUnsolicitedInputPrivilege";
        }
    }
}