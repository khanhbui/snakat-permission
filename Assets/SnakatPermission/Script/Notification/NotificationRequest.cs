using System;

namespace Snakat
{
    internal class NotificationRequest : INotificationRequest, INotificationRequestInvoke
    {
        private Action _Granted;
        private Action _Denied;

        #region INotificationRequest
        public INotificationRequest OnGranted (Action granted)
        {
            _Granted = granted;
            return this;
        }

        public INotificationRequest OnDenied (Action denied)
        {
            _Denied = denied;
            return this;
        }

        public virtual void Start ()
        {
            PlatformImpl.RequestNotification (this);
        }
        #endregion


        #region INotificationRequestInvoke
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
