using System;

namespace Snakat
{
    internal class PermissionRequest : IPermissionRequest, IPermissionRequestInvoke
    {
        private readonly int[] _Permissions;

        private Action _Granted;
        private Action<int[]> _Denied;


        #region IPermissionRequest
        public PermissionRequest (int[] permissions)
        {
            _Permissions = permissions;
        }

        public IPermissionRequest OnGranted (Action granted)
        {
            _Granted = granted;
            return this;
        }

        public IPermissionRequest OnDenied (Action<int[]> denied)
        {
            _Denied = denied;
            return this;
        }

        public void Start ()
        {
            PlatformImpl.RequestPermissions (this);
        }
        #endregion


        #region IPermissionRequestInvoke
        public int[] Permissions {
            get {
                return _Permissions;
            }
        }

        public void InvokeOnGranted ()
        {
            if (_Granted != null) {
                _Granted.Invoke ();
            }
        }

        public void InvokeOnDenied (int[] denied)
        {
            if (_Denied != null) {
                _Denied.Invoke (denied);
            }
        }
        #endregion
    }
}
