using System;

namespace Snakat
{
    public interface INotificationRequest
    {
        INotificationRequest OnGranted (Action granted);

        INotificationRequest OnDenied (Action denied);

        void Start ();
    }

    internal interface INotificationRequestInvoke
    {
        void InvokeOnGranted ();

        void InvokeOnDenied ();
    }
}
