using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace CSharpDemo
{
    public class Common
    {
        public static string PtrToString(IntPtr p)
        {
            // TODO: deal with character set issues.  Will PtrToStringAnsi always
            // "Do The Right Thing"?
            if (p == IntPtr.Zero)
                return null;
            return Marshal.PtrToStringAnsi(p);
        }


        public static string[] PtrToStringArray(IntPtr stringArray)
        {
            if (stringArray == IntPtr.Zero)
                return new string[] { };


            int argc = CountStrings(stringArray);
            return PtrToStringArray(argc, stringArray);
        }

        private static int CountStrings(IntPtr stringArray)
        {
            int count = 0;
            while (Marshal.ReadIntPtr(stringArray, count * IntPtr.Size) != IntPtr.Zero)
                ++count;
            return count;
        }


        public static string[] PtrToStringArray(int count, IntPtr stringArray)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException("count", "< 0");
            if (stringArray == IntPtr.Zero)
                return new string[count];


            string[] members = new string[count];
            for (int i = 0; i < count; ++i)
            {
                IntPtr s = Marshal.ReadIntPtr(stringArray, i * IntPtr.Size);
                members[i] = PtrToString(s);
            }


            return members;
        }
    }
}
