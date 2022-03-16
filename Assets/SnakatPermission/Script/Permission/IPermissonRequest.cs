using System;

namespace Snakat
{
    public interface IPermissionRequest
    {
        // IPermissionRequest Rationale (Rationale<string[]> rationale);

        IPermissionRequest OnGranted (Action granted);

        IPermissionRequest OnDenied (Action<int[]> denied);

        void Start ();
    }

    public interface IPermissionRequestInvoke
    {
        int[] Permissions { get; }

        void InvokeOnGranted ();

        void InvokeOnDenied (int[] denied);
    }
}
