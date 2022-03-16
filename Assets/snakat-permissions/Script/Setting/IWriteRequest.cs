using System;

namespace Snakat
{
    public interface IWriteRequest
    {
        IWriteRequest OnGranted (Action granted);

        IWriteRequest OnDenied (Action denied);

        void Start ();
    }

    internal interface IWriteRequestInvoke
    {
        void InvokeOnGranted ();

        void InvokeOnDenied ();
    }
}
