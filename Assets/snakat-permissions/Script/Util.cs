using System;
using System.Runtime.InteropServices;

namespace Snakat
{
    internal static class Util
    {
        internal static int[] CreateArray (IntPtr source, int length)
        {
            var dest = new int[length];
            Marshal.Copy (source, dest, 0, length);
            return dest;
        }

        internal static bool ContainAll (int[] permissions, int[] allPermissions)
        {
            for (var i = 0; i < permissions.Length; i++) {
                if (!Contain (permissions[i], allPermissions)) {
                    return false;
                }
            }
            return true;
        }

        private static bool Contain (int permission, int[] allPermissions)
        {
            for (var i = 0; i < allPermissions.Length; i++) {
                if (permission == allPermissions[i]) {
                    return true;
                }
            }
            return false;
        }
    }
}
