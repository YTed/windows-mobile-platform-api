//
// Copyright (c) Microsoft Corporation.  All rights reserved.
//
//
// Use of this sample source code is subject to the terms of the Microsoft
// license agreement under which you licensed this sample source code. If
// you did not accept the terms of the license agreement, you are not
// authorized to use this sample source code. For the terms of the license,
// please see the license agreement between you and Microsoft or, if applicable,
// see the LICENSE.RTF on your install media or the root of your tools installation.
// THE SAMPLE SOURCE CODE IS PROVIDED "AS IS", WITH NO WARRANTIES OR INDEMNITIES.
//
using System;

namespace PlatformAPI.GPS
{
    /// <summary>
    /// Summary description for Utils.
    /// </summary>
    public class Utils
    {
        public Utils()
        {
        }

        public static IntPtr LocalAlloc(uint byteCount)
        {
            IntPtr ptr = Memory.LocalAlloc(LMEM_ZEROINIT, byteCount);
            if (ptr == IntPtr.Zero)
            {
                throw new OutOfMemoryException();
            }

            return ptr;
        }

        public static void LocalFree(IntPtr hMem)
        {
            IntPtr ptr = Memory.LocalFree(hMem);
            if (ptr != IntPtr.Zero)
            {
                throw new ArgumentException();
            }
        }

        private const uint LMEM_ZEROINIT = 0x40;
    }

}
