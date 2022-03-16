using System;

namespace Snakat
{
    public interface ISettingRequest
    {
        ISettingRequest OnClosed (Action closed);

        void Start ();
    }

    internal interface ISettingRequestInvoke
    {
        void InvokeOnClosed ();
    }
}
