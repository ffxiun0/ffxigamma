/*
 * Copyright (c) 2023 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using Microsoft.Win32.SafeHandles;
using System;
using System.Drawing;

namespace ffxigamma {
    public abstract class SafeDeviceContextHandle : SafeHandleZeroOrMinusOneIsInvalid {
        public SafeDeviceContextHandle() : base(true) { }
    }

    public class SafeCreateDCHandle : SafeDeviceContextHandle {
        protected override bool ReleaseHandle() => NativeMethods.DeleteDC(handle);
    }

    public class SafeGraphicsDCHandle : SafeDeviceContextHandle {
        private readonly Graphics owner;

        public SafeGraphicsDCHandle(IntPtr handle, Graphics owner) {
            this.handle = handle;
            this.owner = owner;
        }

        protected override bool ReleaseHandle() {
            owner.ReleaseHdc(handle);
            return true;
        }
    }

    public static class SafeDeviceContextHandleExtention {
        public static SafeGraphicsDCHandle GetHdcSafe(this Graphics g)
            => new SafeGraphicsDCHandle(g.GetHdc(), g);
    }

    public class SafeIconHandle : SafeHandleZeroOrMinusOneIsInvalid {
        public SafeIconHandle(IntPtr handle) : base(true) {
            this.handle = handle;
        }

        protected override bool ReleaseHandle() => NativeMethods.DestroyIcon(handle);
    }

    public class SafeItemIdList : SafeHandleZeroOrMinusOneIsInvalid {
        public SafeItemIdList() : base(true) { }

        protected override bool ReleaseHandle() {
            NativeMethods.ILFree(handle);
            return true;
        }
    }
}
