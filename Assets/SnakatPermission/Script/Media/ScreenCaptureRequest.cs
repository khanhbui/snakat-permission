using System;
using UnityEngine;

namespace Snakat
{
    internal class ScreenCaptureRequest : IScreenCaptureRequest, IScreenCaptureRequestInvoke
    {
#if UNITY_ANDROID
        private Action<AndroidJavaObject> _Granted;
#else
        private Action _Granted;
#endif
        private Action _Denied;


        #region IMediaRequest
#if UNITY_ANDROID
        public IScreenCaptureRequest OnGranted (Action<AndroidJavaObject> granted)
#else
        public IScreenCaptureRequest OnGranted (Action granted)

#endif
        {
            _Granted = granted;
            return this;
        }

        public IScreenCaptureRequest OnDenied (Action denied)
        {
            _Denied = denied;
            return this;
        }

        public void Start ()
        {
            PlatformImpl.RequestScreenCapture (this);
        }
        #endregion


        #region IMediaRequestInvoke
#if UNITY_ANDROID
        public void InvokeOnGranted (AndroidJavaObject intent)
        {
            if (_Granted != null) {
                _Granted.Invoke (intent);
            }
        }
#else
        public void InvokeOnGranted ()
        {
            if (_Granted != null) {
                _Granted.Invoke ();
            }
        }
#endif

        public void InvokeOnDenied ()
        {
            if (_Denied != null) {
                _Denied.Invoke ();
            }
        }
        #endregion
    }
}
