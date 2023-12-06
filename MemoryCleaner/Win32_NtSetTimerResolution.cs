using System;
using System.Runtime.InteropServices;

namespace MemoryCleaner
{
	// Token: 0x0200000C RID: 12
	internal sealed class Win32_NtSetTimerResolution
	{
		// Token: 0x0600006F RID: 111
		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern int NtSetTimerResolution(uint DesiredResolution, bool SetResolution, out uint CurrentResolution);

		// Token: 0x06000070 RID: 112
		[DllImport("ntdll.dll", SetLastError = true)]
		private static extern int NtQueryTimerResolution(out uint MinimumResolution, out uint MaximumResolution, out uint ActualResolution);

		// Token: 0x06000071 RID: 113 RVA: 0x00002F50 File Offset: 0x00001150
		public void GetCurrentTimerResolution()
		{
			Win32_NtSetTimerResolution.NtQueryTimerResolution(out this.MininumResolution, out this.MaximumResolution, out this.DefaultResolution);
			//App.Appsettings.DefaultResolution = this.DefaultResolution / 10000f;
			//App.Appsettings.DefaultResolutionMS = (this.DefaultResolution / 10000f).ToString() + "ms";
			//App.Appsettings.MaximumTimerResolution = this.MaximumResolution / 10000f;
			//App.Appsettings.MaximumTimerResolutionMS = (this.MaximumResolution / 10000f).ToString() + "ms";
			//App.Appsettings.MinimumTimerResolution = this.MininumResolution / 10000f;
			//App.Appsettings.MinimumTimerResolutionMS = (this.MininumResolution / 10000f).ToString() + "ms";
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000303C File Offset: 0x0000123C
		public void SetMaxTimerResolution(uint WantedResolution)
		{
			uint actual = 0U;
			Win32_NtSetTimerResolution.NtSetTimerResolution(WantedResolution, true, out actual);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003058 File Offset: 0x00001258
		public void ReleaseMaxTimerResolution(uint WantedResolution)
		{
			uint actual = 0U;
			Win32_NtSetTimerResolution.NtSetTimerResolution(WantedResolution, false, out actual);
		}

		// Token: 0x04000036 RID: 54
		private uint DefaultResolution;

		// Token: 0x04000037 RID: 55
		private uint MininumResolution;

		// Token: 0x04000038 RID: 56
		private uint MaximumResolution;
	}
}
