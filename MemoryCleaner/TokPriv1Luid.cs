using System;
using System.Runtime.InteropServices;

namespace MemoryCleaner
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct TokPriv1Luid
    {
        public int Count;
        public long Luid;
        public int Attr;
    }
}
