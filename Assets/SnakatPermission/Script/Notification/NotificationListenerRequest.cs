namespace Snakat
{
    internal class NotificationListenerRequest : NotificationRequest
    {
        public override void Start ()
        {
            PlatformImpl.RequestNotificationListener (this);
        }
    }
}
