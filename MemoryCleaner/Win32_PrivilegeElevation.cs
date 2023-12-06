using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace MemoryCleaner
{
	// Token: 0x0200000E RID: 14
	internal class Win32_PrivilegeElevation
	{
		// Token: 0x06000075 RID: 117
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern bool LookupPrivilegeValue(string host, string name, ref long pluid);

		// Token: 0x06000076 RID: 118
		[DllImport("advapi32.dll", SetLastError = true)]
		internal static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TokPriv1Luid newst, int len, IntPtr prev, IntPtr relen);

		// Token: 0x06000077 RID: 119 RVA: 0x0000307C File Offset: 0x0000127C
		private static bool SetIncreasePrivilege(string privilegeName)
		{
			bool flag;
			using (WindowsIdentity current = WindowsIdentity.GetCurrent(TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges))
			{
				TokPriv1Luid newst;
				newst.Count = 1;
				newst.Luid = 0L;
				newst.Attr = 2;
				if (!Win32_PrivilegeElevation.LookupPrivilegeValue(null, privilegeName, ref newst.Luid))
				{
					throw new Exception("Error in LookupPrivilegeValue: ", new Win32Exception(Marshal.GetLastWin32Error()));
				}
				int num = (Win32_PrivilegeElevation.AdjustTokenPrivileges(current.Token, false, ref newst, 0, IntPtr.Zero, IntPtr.Zero) ? 1 : 0);
				if (num == 0)
				{
					throw new Exception("Error in AdjustTokenPrivileges: ", new Win32Exception(Marshal.GetLastWin32Error()));
				}
				flag = num != 0;
			}
			return flag;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003128 File Offset: 0x00001328
		public static bool SetIncreasePrivileges(params string[] privilegeNames)
		{
			for (int i = 0; i < privilegeNames.Length; i++)
			{
				Win32_PrivilegeElevation.SetIncreasePrivilege(privilegeNames[i]);
			}
			return true;
		}

		// Token: 0x0400003C RID: 60
		private const int SE_PRIVILEGE_ENABLED = 2;

		// Token: 0x0200002B RID: 43
		public class SE
		{
			// Token: 0x040000B6 RID: 182
			public const string ASSIGNPRIMARYTOKEN_NAME = "SeAssignPrimaryTokenPrivilege";

			// Token: 0x040000B7 RID: 183
			public const string AUDIT_NAME = "SeAuditPrivilege";

			// Token: 0x040000B8 RID: 184
			public const string BACKUP_NAME = "SeBackupPrivilege";

			// Token: 0x040000B9 RID: 185
			public const string CHANGE_NOTIFY_NAME = "SeChangeNotifyPrivilege";

			// Token: 0x040000BA RID: 186
			public const string CREATE_GLOBAL_NAME = "SeCreateGlobalPrivilege";

			// Token: 0x040000BB RID: 187
			public const string CREATE_PAGEFILE_NAME = "SeCreatePagefilePrivilege";

			// Token: 0x040000BC RID: 188
			public const string CREATE_PERMANENT_NAME = "SeCreatePermanentPrivilege";

			// Token: 0x040000BD RID: 189
			public const string CREATE_TOKEN_NAME = "SeCreateTokenPrivilege";

			// Token: 0x040000BE RID: 190
			public const string DEBUG_NAME = "SeDebugPrivilege";

			// Token: 0x040000BF RID: 191
			public const string ENABLE_DELEGATION_NAME = "SeEnableDelegationPrivilege";

			// Token: 0x040000C0 RID: 192
			public const string IMPERSONATE_NAME = "SeImpersonatePrivilege";

			// Token: 0x040000C1 RID: 193
			public const string INCREAQUOTA_NAME = "SeIncreaseQuotaPrivilege";

			// Token: 0x040000C2 RID: 194
			public const string INC_BAPRIORITY_NAME = "SeIncreaseBasePriorityPrivilege";

			// Token: 0x040000C3 RID: 195
			public const string LOAD_DRIVER_NAME = "SeLoadDriverPrivilege";

			// Token: 0x040000C4 RID: 196
			public const string LOCK_MEMORY_NAME = "SeLockMemoryPrivilege";

			// Token: 0x040000C5 RID: 197
			public const string MACHINE_ACCOUNT_NAME = "SeMachineAccountPrivilege";

			// Token: 0x040000C6 RID: 198
			public const string MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";

			// Token: 0x040000C7 RID: 199
			public const string PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";

			// Token: 0x040000C8 RID: 200
			public const string REMOTE_SHUTDOWN_NAME = "SeRemoteShutdownPrivilege";

			// Token: 0x040000C9 RID: 201
			public const string RESTORE_NAME = "SeRestorePrivilege";

			// Token: 0x040000CA RID: 202
			public const string SECURITY_NAME = "SeSecurityPrivilege";

			// Token: 0x040000CB RID: 203
			public const string SHUTDOWN_NAME = "SeShutdownPrivilege";

			// Token: 0x040000CC RID: 204
			public const string SYSTEMTIME_NAME = "SeSystemtimePrivilege";

			// Token: 0x040000CD RID: 205
			public const string SYSTEM_ENVIRONMENT_NAME = "SeSystemEnvironmentPrivilege";

			// Token: 0x040000CE RID: 206
			public const string SYSTEM_PROFILE_NAME = "SeSystemProfilePrivilege";

			// Token: 0x040000CF RID: 207
			public const string TAKE_OWNERSHIP_NAME = "SeTakeOwnershipPrivilege";

			// Token: 0x040000D0 RID: 208
			public const string TCB_NAME = "SeTcbPrivilege";

			// Token: 0x040000D1 RID: 209
			public const string UNDOCK_NAME = "SeUndockPrivilege";

			// Token: 0x040000D2 RID: 210
			public const string UNSOLICITED_INPUT_NAME = "SeUnsolicitedInputPrivilege";
		}
	}
}
