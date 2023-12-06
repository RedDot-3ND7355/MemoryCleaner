using System;
using System.Runtime.InteropServices;

namespace MemoryCleaner
{
	// Token: 0x0200000D RID: 13
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct TokPriv1Luid
	{
		// Token: 0x04000039 RID: 57
		public int Count;

		// Token: 0x0400003A RID: 58
		public long Luid;

		// Token: 0x0400003B RID: 59
		public int Attr;
	}
}
