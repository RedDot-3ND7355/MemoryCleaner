using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;

namespace MemoryCleaner
{
	// Token: 0x02000012 RID: 18
	internal sealed class Win32_NtSetSystemInformation
	{
		// Token: 0x060000B3 RID: 179
		[DllImport("ntdll.dll")]
		internal static extern uint NtSetSystemInformation(int infoClass, IntPtr info, int length);

		// Token: 0x060000B4 RID: 180 RVA: 0x00004828 File Offset: 0x00002A28
		public void ClearStandbyCache()
		{
			if (Win32_PrivilegeElevation.SetIncreasePrivileges(new string[] { "SeProfileSingleProcessPrivilege" }))
			{
				try
				{
					int systemInfoLength = Marshal.SizeOf<int>(4);
					GCHandle gcHandle = GCHandle.Alloc(4, GCHandleType.Pinned);
					if (Win32_NtSetSystemInformation.NtSetSystemInformation(80, gcHandle.AddrOfPinnedObject(), systemInfoLength) != 0U)
					{
						throw new Exception("NtSetSystemInformation: ", new Win32Exception(Marshal.GetLastWin32Error()));
					}
					gcHandle.Free();
					//App.Appsettings.NumberOfTime = (int.Parse(App.Appsettings.NumberOfTime) + 1).ToString();
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		// Token: 0x0400007D RID: 125
		private const int MemoryPurgeStandbyList = 4;

		// Token: 0x02000032 RID: 50
		internal enum SYSTEM_INFORMATION_CLASS
		{
			// Token: 0x040000F4 RID: 244
			SystemBasicInformation,
			// Token: 0x040000F5 RID: 245
			SystemProcessorInformation,
			// Token: 0x040000F6 RID: 246
			SystemPerformanceInformation,
			// Token: 0x040000F7 RID: 247
			SystemTimeOfDayInformation,
			// Token: 0x040000F8 RID: 248
			SystemPathInformation,
			// Token: 0x040000F9 RID: 249
			SystemProcessInformation,
			// Token: 0x040000FA RID: 250
			SystemCallCountInformation,
			// Token: 0x040000FB RID: 251
			SystemDeviceInformation,
			// Token: 0x040000FC RID: 252
			SystemProcessorPerformanceInformation,
			// Token: 0x040000FD RID: 253
			SystemFlagsInformation,
			// Token: 0x040000FE RID: 254
			SystemCallTimeInformation,
			// Token: 0x040000FF RID: 255
			SystemModuleInformation,
			// Token: 0x04000100 RID: 256
			SystemLocksInformation,
			// Token: 0x04000101 RID: 257
			SystemStackTraceInformation,
			// Token: 0x04000102 RID: 258
			SystemPagedPoolInformation,
			// Token: 0x04000103 RID: 259
			SystemNonPagedPoolInformation,
			// Token: 0x04000104 RID: 260
			SystemHandleInformation,
			// Token: 0x04000105 RID: 261
			SystemObjectInformation,
			// Token: 0x04000106 RID: 262
			SystemPageFileInformation,
			// Token: 0x04000107 RID: 263
			SystemVdmInstemulInformation,
			// Token: 0x04000108 RID: 264
			SystemVdmBopInformation,
			// Token: 0x04000109 RID: 265
			SystemFileCacheInformation,
			// Token: 0x0400010A RID: 266
			SystemPoolTagInformation,
			// Token: 0x0400010B RID: 267
			SystemInterruptInformation,
			// Token: 0x0400010C RID: 268
			SystemDpcBehaviorInformation,
			// Token: 0x0400010D RID: 269
			SystemFullMemoryInformation,
			// Token: 0x0400010E RID: 270
			SystemLoadGdiDriverInformation,
			// Token: 0x0400010F RID: 271
			SystemUnloadGdiDriverInformation,
			// Token: 0x04000110 RID: 272
			SystemTimeAdjustmentInformation,
			// Token: 0x04000111 RID: 273
			SystemSummaryMemoryInformation,
			// Token: 0x04000112 RID: 274
			SystemMirrorMemoryInformation,
			// Token: 0x04000113 RID: 275
			SystemPerformanceTraceInformation,
			// Token: 0x04000114 RID: 276
			SystemCrashDumpInformation,
			// Token: 0x04000115 RID: 277
			SystemExceptionInformation,
			// Token: 0x04000116 RID: 278
			SystemCrashDumpStateInformation,
			// Token: 0x04000117 RID: 279
			SystemKernelDebuggerInformation,
			// Token: 0x04000118 RID: 280
			SystemContextSwitchInformation,
			// Token: 0x04000119 RID: 281
			SystemRegistryQuotaInformation,
			// Token: 0x0400011A RID: 282
			SystemExtendServiceTableInformation,
			// Token: 0x0400011B RID: 283
			SystemPrioritySeperation,
			// Token: 0x0400011C RID: 284
			SystemVerifierAddDriverInformation,
			// Token: 0x0400011D RID: 285
			SystemVerifierRemoveDriverInformation,
			// Token: 0x0400011E RID: 286
			SystemProcessorIdleInformation,
			// Token: 0x0400011F RID: 287
			SystemLegacyDriverInformation,
			// Token: 0x04000120 RID: 288
			SystemCurrentTimeZoneInformation,
			// Token: 0x04000121 RID: 289
			SystemLookasideInformation,
			// Token: 0x04000122 RID: 290
			SystemTimeSlipNotification,
			// Token: 0x04000123 RID: 291
			SystemSessionCreate,
			// Token: 0x04000124 RID: 292
			SystemSessionDetach,
			// Token: 0x04000125 RID: 293
			SystemSessionInformation,
			// Token: 0x04000126 RID: 294
			SystemRangeStartInformation,
			// Token: 0x04000127 RID: 295
			SystemVerifierInformation,
			// Token: 0x04000128 RID: 296
			SystemVerifierThunkExtend,
			// Token: 0x04000129 RID: 297
			SystemSessionProcessInformation,
			// Token: 0x0400012A RID: 298
			SystemLoadGdiDriverInSystemSpace,
			// Token: 0x0400012B RID: 299
			SystemNumaProcessorMap,
			// Token: 0x0400012C RID: 300
			SystemPrefetcherInformation,
			// Token: 0x0400012D RID: 301
			SystemExtendedProcessInformation,
			// Token: 0x0400012E RID: 302
			SystemRecommendedSharedDataAlignment,
			// Token: 0x0400012F RID: 303
			SystemComPlusPackage,
			// Token: 0x04000130 RID: 304
			SystemNumaAvailableMemory,
			// Token: 0x04000131 RID: 305
			SystemProcessorPowerInformation,
			// Token: 0x04000132 RID: 306
			SystemEmulationBasicInformation,
			// Token: 0x04000133 RID: 307
			SystemEmulationProcessorInformation,
			// Token: 0x04000134 RID: 308
			SystemExtendedHandleInformation,
			// Token: 0x04000135 RID: 309
			SystemLostDelayedWriteInformation,
			// Token: 0x04000136 RID: 310
			SystemBigPoolInformation,
			// Token: 0x04000137 RID: 311
			SystemSessionPoolTagInformation,
			// Token: 0x04000138 RID: 312
			SystemSessionMappedViewInformation,
			// Token: 0x04000139 RID: 313
			SystemHotpatchInformation,
			// Token: 0x0400013A RID: 314
			SystemObjectSecurityMode,
			// Token: 0x0400013B RID: 315
			SystemWatchdogTimerHandler,
			// Token: 0x0400013C RID: 316
			SystemWatchdogTimerInformation,
			// Token: 0x0400013D RID: 317
			SystemLogicalProcessorInformation,
			// Token: 0x0400013E RID: 318
			SystemWow64SharedInformationObsolete,
			// Token: 0x0400013F RID: 319
			SystemRegisterFirmwareTableInformationHandler,
			// Token: 0x04000140 RID: 320
			SystemFirmwareTableInformation,
			// Token: 0x04000141 RID: 321
			SystemModuleInformationEx,
			// Token: 0x04000142 RID: 322
			SystemVerifierTriageInformation,
			// Token: 0x04000143 RID: 323
			SystemSuperfetchInformation,
			// Token: 0x04000144 RID: 324
			SystemMemoryListInformation,
			// Token: 0x04000145 RID: 325
			SystemFileCacheInformationEx,
			// Token: 0x04000146 RID: 326
			SystemThreadPriorityClientIdInformation,
			// Token: 0x04000147 RID: 327
			SystemProcessorIdleCycleTimeInformation,
			// Token: 0x04000148 RID: 328
			SystemVerifierCancellationInformation,
			// Token: 0x04000149 RID: 329
			SystemProcessorPowerInformationEx,
			// Token: 0x0400014A RID: 330
			SystemRefTraceInformation,
			// Token: 0x0400014B RID: 331
			SystemSpecialPoolInformation,
			// Token: 0x0400014C RID: 332
			SystemProcessIdInformation,
			// Token: 0x0400014D RID: 333
			SystemErrorPortInformation,
			// Token: 0x0400014E RID: 334
			SystemBootEnvironmentInformation,
			// Token: 0x0400014F RID: 335
			SystemHypervisorInformation,
			// Token: 0x04000150 RID: 336
			SystemVerifierInformationEx,
			// Token: 0x04000151 RID: 337
			SystemTimeZoneInformation,
			// Token: 0x04000152 RID: 338
			SystemImageFileExecutionOptionsInformation,
			// Token: 0x04000153 RID: 339
			SystemCoverageInformation,
			// Token: 0x04000154 RID: 340
			SystemPrefetchPatchInformation,
			// Token: 0x04000155 RID: 341
			SystemVerifierFaultsInformation,
			// Token: 0x04000156 RID: 342
			SystemSystemPartitionInformation,
			// Token: 0x04000157 RID: 343
			SystemSystemDiskInformation,
			// Token: 0x04000158 RID: 344
			SystemProcessorPerformanceDistribution,
			// Token: 0x04000159 RID: 345
			SystemNumaProximityNodeInformation,
			// Token: 0x0400015A RID: 346
			SystemDynamicTimeZoneInformation,
			// Token: 0x0400015B RID: 347
			SystemCodeIntegrityInformation,
			// Token: 0x0400015C RID: 348
			SystemProcessorMicrocodeUpdateInformation,
			// Token: 0x0400015D RID: 349
			SystemProcessorBrandString,
			// Token: 0x0400015E RID: 350
			SystemVirtualAddressInformation,
			// Token: 0x0400015F RID: 351
			SystemLogicalProcessorAndGroupInformation,
			// Token: 0x04000160 RID: 352
			SystemProcessorCycleTimeInformation,
			// Token: 0x04000161 RID: 353
			SystemStoreInformation,
			// Token: 0x04000162 RID: 354
			SystemRegistryAppendString,
			// Token: 0x04000163 RID: 355
			SystemAitSamplingValue,
			// Token: 0x04000164 RID: 356
			SystemVhdBootInformation,
			// Token: 0x04000165 RID: 357
			SystemCpuQuotaInformation,
			// Token: 0x04000166 RID: 358
			SystemNativeBasicInformation,
			// Token: 0x04000167 RID: 359
			SystemErrorPortTimeouts,
			// Token: 0x04000168 RID: 360
			SystemLowPriorityIoInformation,
			// Token: 0x04000169 RID: 361
			SystemBootEntropyInformation,
			// Token: 0x0400016A RID: 362
			SystemVerifierCountersInformation,
			// Token: 0x0400016B RID: 363
			SystemPagedPoolInformationEx,
			// Token: 0x0400016C RID: 364
			SystemSystemPtesInformationEx,
			// Token: 0x0400016D RID: 365
			SystemNodeDistanceInformation,
			// Token: 0x0400016E RID: 366
			SystemAcpiAuditInformation,
			// Token: 0x0400016F RID: 367
			SystemBasicPerformanceInformation,
			// Token: 0x04000170 RID: 368
			SystemQueryPerformanceCounterInformation,
			// Token: 0x04000171 RID: 369
			SystemSessionBigPoolInformation,
			// Token: 0x04000172 RID: 370
			SystemBootGraphicsInformation,
			// Token: 0x04000173 RID: 371
			SystemScrubPhysicalMemoryInformation,
			// Token: 0x04000174 RID: 372
			SystemBadPageInformation,
			// Token: 0x04000175 RID: 373
			SystemProcessorProfileControlArea,
			// Token: 0x04000176 RID: 374
			SystemCombinePhysicalMemoryInformation,
			// Token: 0x04000177 RID: 375
			SystemEntropyInterruptTimingInformation,
			// Token: 0x04000178 RID: 376
			SystemConsoleInformation,
			// Token: 0x04000179 RID: 377
			SystemPlatformBinaryInformation,
			// Token: 0x0400017A RID: 378
			SystemThrottleNotificationInformation,
			// Token: 0x0400017B RID: 379
			SystemHypervisorProcessorCountInformation,
			// Token: 0x0400017C RID: 380
			SystemDeviceDataInformation,
			// Token: 0x0400017D RID: 381
			SystemDeviceDataEnumerationInformation,
			// Token: 0x0400017E RID: 382
			SystemMemoryTopologyInformation,
			// Token: 0x0400017F RID: 383
			SystemMemoryChannelInformation,
			// Token: 0x04000180 RID: 384
			SystemBootLogoInformation,
			// Token: 0x04000181 RID: 385
			SystemProcessorPerformanceInformationEx,
			// Token: 0x04000182 RID: 386
			SystemSpare0,
			// Token: 0x04000183 RID: 387
			SystemSecureBootPolicyInformation,
			// Token: 0x04000184 RID: 388
			SystemPageFileInformationEx,
			// Token: 0x04000185 RID: 389
			SystemSecureBootInformation,
			// Token: 0x04000186 RID: 390
			SystemEntropyInterruptTimingRawInformation,
			// Token: 0x04000187 RID: 391
			SystemPortableWorkspaceEfiLauncherInformation,
			// Token: 0x04000188 RID: 392
			SystemFullProcessInformation,
			// Token: 0x04000189 RID: 393
			MaxSystemInfoClass
		}

		// Token: 0x02000033 RID: 51
		private enum SYSTEM_MEMORY_LIST_COMMAND
		{
			// Token: 0x0400018B RID: 395
			MemoryCaptureAccessedBits,
			// Token: 0x0400018C RID: 396
			MemoryCaptureAndResetAccessedBits,
			// Token: 0x0400018D RID: 397
			MemoryEmptyWorkingSets,
			// Token: 0x0400018E RID: 398
			MemoryFlushModifiedList,
			// Token: 0x0400018F RID: 399
			MemoryPurgeStandbyList,
			// Token: 0x04000190 RID: 400
			MemoryPurgeLowPriorityStandbyList,
			// Token: 0x04000191 RID: 401
			MemoryCommandMax
		}
	}
}
