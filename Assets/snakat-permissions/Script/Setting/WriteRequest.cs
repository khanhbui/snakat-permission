using System;

namespace Snakat
{
    internal class WriteRequest : IWriteRequest, IWriteRequestInvoke
    {
        private Action _Granted;
        private Action _Denied;


        #region IWriteRequest
        public IWriteRequest OnGranted (Action granted)
        {
            _Granted = granted;
            return this;
        }

        public IWriteRequest OnDenied (Action denied)
        {
            _Denied = denied;
            return this;
        }

        public void Start ()
        {
            PlatformImpl.RequestWriteSystemSetting (this);
        }
        #endregion


        #region IWriteRequestInvoke
        public void InvokeOnGranted ()
        {
            if (_Granted != null) {
                _Granted.Invoke ();
            }
        }

        public void InvokeOnDenied ()
        {
            if (_Denied != null) {
                _Denied.Invoke ();
            }
        }
        #endregion
    }
}
