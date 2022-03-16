using System;

namespace Snakat
{
    internal class SettingRequest : ISettingRequest, ISettingRequestInvoke
    {
        private Action _Closed;

        #region ISettingRequest
        public ISettingRequest OnClosed (Action closed)
        {
            _Closed = closed;

            return this;
        }

        public void Start ()
        {
            PlatformImpl.OpenSetting (this);
        }
        #endregion


        #region ISettingRequestInvoke
        public void InvokeOnClosed ()
        {
            if (_Closed != null) {
                _Closed.Invoke ();
            }
        }
        #endregion
    }
}
