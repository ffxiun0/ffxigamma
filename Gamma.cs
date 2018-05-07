/*
 * Copyright (c) 2015 ffxiun0
 * https://opensource.org/licenses/MIT
 */
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ffxigamma {
    class Gamma {
        private double gamma;
        private byte[] table;

        public Gamma(double gamma) {
            this.gamma = gamma;
            this.table = MakeLookupTable(gamma);
        }

        public double Value {
            get {
                return gamma;
            }
        }

        private static byte[] MakeLookupTable(double gamma) {
            var table = new byte[256];
            for (int i = 0; i < 256; i++)
                table[i] = Compute((byte)i, gamma);
            return table;
        }

        private static byte Compute(byte b, double gamma) {
            return (byte)Math.Floor(Math.Pow(b / 255.0, gamma) * 255.0 + 0.5);
        }

        public byte Lookup(byte b) {
            return table[b];
        }

        public void ApplyTo(Bitmap bmp) {
            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            var bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
            try {
                var ptr = bmpData.Scan0;
                var size = Math.Abs(bmpData.Stride) * bmp.Height;
                var bytes = new byte[size];

                Marshal.Copy(ptr, bytes, 0, size);
                Apply(bmpData, bytes);
                Marshal.Copy(bytes, 0, ptr, size);
            }
            finally {
                bmp.UnlockBits(bmpData);
            }
        }

        private void Apply(BitmapData bmpData, byte[] bytes) {
            switch (bmpData.PixelFormat) {
                case PixelFormat.Format24bppRgb:
                    Apply24bit(bytes, bmpData.Width, bmpData.Height, bmpData.Stride);
                    break;
                case PixelFormat.Format32bppRgb:
                case PixelFormat.Format32bppArgb:
                    Apply32bit(bytes, bmpData.Width, bmpData.Height, bmpData.Stride);
                    break;
                default:
                    throw new NotSupportedException(bmpData.PixelFormat + " is not supported.");
            }
        }

        private void Apply24bit(byte[] bytes, int width, int height, int stride) {
            for (int y = 0; y < height; y++) {
                int offset = y * stride;
                for (int x = 0; x < width; x++) {
                    bytes[offset + 0] = table[bytes[offset + 0]];
                    bytes[offset + 1] = table[bytes[offset + 1]];
                    bytes[offset + 2] = table[bytes[offset + 2]];
                    offset += 3;
                }
            }
        }

        private void Apply32bit(byte[] bytes, int width, int height, int stride) {
            for (int y = 0; y < height; y++) {
                int offset = y * stride;
                for (int x = 0; x < width; x++) {
                    bytes[offset + 0] = table[bytes[offset + 0]];
                    bytes[offset + 1] = table[bytes[offset + 1]];
                    bytes[offset + 2] = table[bytes[offset + 2]];
                    offset += 4;
                }
            }
        }
    }
}
