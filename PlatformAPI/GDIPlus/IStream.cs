using System;
using System.Runtime.InteropServices;

namespace PlatformAPI.Runtime.InteropServices.ComTypes
{
    [ComImport, Guid("0000000c-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStream
    {
        void Read([Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbRead);
        void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);
        void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);
        void SetSize(long libNewSize);
        void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);
        void Commit(int grfCommitFlags);
        void Revert();
        void LockRegion(long libOffset, long cb, int dwLockType);
        void UnlockRegion(long libOffset, long cb, int dwLockType);
        void Stat(out STATSTG pstatstg, int grfStatFlag);
        void Clone(out IStream ppstm);
    }
}
