using System;
using UnityEngine;

namespace Snakat
{
    public interface IScreenCaptureRequest
    {
#if UNITY_ANDROID
        IScreenCaptureRequest OnGranted (Action<AndroidJavaObject> granted);
#else
        IScreenCaptureRequest OnGranted (Action granted);
#endif

        IScreenCaptureRequest OnDenied (Action denied);

        void Start ();
    }

    public interface IScreenCaptureRequestInvoke
    {
#if UNITY_ANDROID
        void InvokeOnGranted (AndroidJavaObject granted);
#else
        void InvokeOnGranted ();
#endif

        void InvokeOnDenied ();
    }
}
