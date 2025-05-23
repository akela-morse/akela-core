﻿using System;
using System.Runtime.CompilerServices;

namespace Akela.Tools
{
    public static class BitExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetBitMask(this int nthBit)
        {
            return 1 << nthBit;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBitSet(this int n, int nthBit)
        {
            return (n & GetBitMask(nthBit)) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SetBit(this int n, int nthBit)
        {
            return n | GetBitMask(nthBit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ClearBit(this int n, int nthBit)
        {
            return n & ~GetBitMask(nthBit);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToggleBit(this int n, int nthBit)
        {
            return n ^ GetBitMask(nthBit);
        }

        public static bool ApplyBit(this ref int n, int nthBit, bool set)
        {
            if (set)
                n = n.SetBit(nthBit);
            else
                n = n.ClearBit(nthBit);

            return set;
        }

        public static bool[] ToBoolArray(this byte n)
        {
            return MakeBoolArray(n, 8);
        }

        public static bool[] ToBoolArray(this sbyte n)
        {
            return MakeBoolArray(n, 8);
        }

        public static bool[] ToBoolArray(this short n)
        {
            return MakeBoolArray(n, 16);
        }

        public static bool[] ToBoolArray(this ushort n)
        {
            return MakeBoolArray(n, 16);
        }

        public static bool[] ToBoolArray(this int n)
        {
            return n.MakeBoolArray(32);
        }

        public static bool[] ToBoolArray(this uint n)
        {
            return ((int)n).MakeBoolArray(32);
        }

        public static T FromBoolArray<T>(this bool[] arr) where T : unmanaged
        {
            var num = 0;

            for (var i = 0; i < arr.Length; ++i)
            {
                if (arr[i])
                    num = num.SetBit(i);
            }

            return (T)Convert.ChangeType(num, typeof(T));
        }

        #region Private Methods
        private static bool[] MakeBoolArray(this int n, int bits)
        {
            var arr = new bool[bits];

            for (var i = 0; i < bits; ++i)
            {
                if (n.IsBitSet(i))
                    arr[i] = true;
            }

            return arr;
        }
        #endregion
    }
}